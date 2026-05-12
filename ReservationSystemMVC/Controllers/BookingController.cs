using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Services;
using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Infrastructure.Data;
using ReservationSystemMVC.Models;
using Stripe.Checkout;
using Stripe;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using Order = PayPalCheckoutSdk.Orders.Order;
using System.Collections.Generic;

namespace ReservationSystemMVC.Controllers;

public class BookingController : Controller
{
    private readonly BookingService _bookingService;
    private readonly IResourceRepository _resourceRepository;
    private readonly ReservationPricingService _pricingService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public BookingController(
        BookingService bookingService,
        IResourceRepository resourceRepository,
        ReservationPricingService pricingService,
        ApplicationDbContext dbContext,
        IConfiguration configuration)
    {
        _bookingService = bookingService;
        _resourceRepository = resourceRepository;
        _pricingService = pricingService;
        _dbContext = dbContext;
        _configuration = configuration;

        var stripeSecretKey = _configuration["Stripe:SecretKey"];
        if (!string.IsNullOrWhiteSpace(stripeSecretKey))
        {
            StripeConfiguration.ApiKey = stripeSecretKey;
        }
    }

    [Authorize]
    [HttpGet]
    public IActionResult Create(Guid resourceId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var vm = BuildCreateViewModel(resourceId, startDate, endDate);
        return View(vm);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookingCreateViewModel vm)
    {
        if (vm.StartDate.Date < DateTime.Today)
        {
            ModelState.AddModelError(nameof(vm.StartDate), "Data de început nu poate fi în trecut.");
        }

        if (vm.EndDate.Date <= vm.StartDate.Date)
        {
            ModelState.AddModelError(nameof(vm.EndDate), "Data de final trebuie să fie după data de început.");
        }

        var resource = _resourceRepository.GetById(vm.ResourceId);
        if (resource == null)
        {
            ModelState.AddModelError(nameof(vm.ResourceId), "Resursa selectată nu există.");
        }

        if (!ModelState.IsValid)
        {
            vm.AvailableResources = _resourceRepository.GetAll();
            return View(vm);
        }

        try
        {
            var start = vm.StartDate.Date;
            var end = vm.EndDate.Date;
            var days = (end - start).Days;

            if (!_bookingService.IsAvailable(vm.ResourceId, start, end))
            {
                vm.AvailableResources = _resourceRepository.GetAll();
                vm.ErrorMessage = "Resursa nu mai este disponibilă pentru perioada selectată.";
                return View(vm);
            }

            var totalPrice = _pricingService.CalculateTotalPrice(resource!, Math.Max(days, 1));
            var userId = TryGetCurrentUserId();
            var fullName = string.IsNullOrWhiteSpace(vm.FullName) ? GetCurrentUserName() : vm.FullName;
            var email = string.IsNullOrWhiteSpace(vm.Email) ? User.Identity?.Name ?? string.Empty : vm.Email;

            var booking = _bookingService.CreateBooking(
                vm.ResourceId,
                start,
                end,
                userId,
                fullName,
                email,
                vm.PhoneNumber,
                vm.GuestsCount,
                vm.SpecialRequests,
                totalPrice);

            if (vm.PaymentProvider == "PayPal")
            {
                var clientId = _configuration["PayPal:ClientId"] ?? "dummy_client_id";
                var secret = _configuration["PayPal:Secret"] ?? "dummy_secret";
                var env = new SandboxEnvironment(clientId, secret);
                var client = new PayPalHttpClient(env);

                var orderRequest = new OrdersCreateRequest();
                orderRequest.Prefer("return=representation");
                var amountFormatted = totalPrice.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

                orderRequest.RequestBody(new OrderRequest()
                {
                    CheckoutPaymentIntent = "CAPTURE",
                    PurchaseUnits = new List<PurchaseUnitRequest>
                    {
                        new PurchaseUnitRequest
                        {
                            AmountWithBreakdown = new AmountWithBreakdown
                            {
                                CurrencyCode = "EUR",
                                Value = amountFormatted
                            },
                            Description = $"Rezervare {resource!.Name}"
                        }
                    },
                    ApplicationContext = new ApplicationContext
                    {
                        ReturnUrl = $"{Request.Scheme}://{Request.Host}/Booking/PayPalSuccess?bookingId={booking.Id}",
                        CancelUrl = $"{Request.Scheme}://{Request.Host}/Booking/Cancel?bookingId={booking.Id}"
                    }
                });

                var response = client.Execute(orderRequest).Result;
                var result = response.Result<Order>();

                _dbContext.Payments.Add(new Payment
                {
                    BookingId = booking.Id,
                    AmountInCents = (long)Math.Round(totalPrice * 100m),
                    Currency = "eur",
                    PayPalOrderId = result.Id,
                    Provider = "PayPal",
                    Status = "Pending",
                    StripeSessionId = Guid.NewGuid().ToString() // Evităm eroarea de unicitate pe indexul StripeSessionId
                });
                _dbContext.SaveChanges();

                var approveLink = result.Links.FirstOrDefault(l => l.Rel == "approve")?.Href;
                return Redirect(approveLink!);
            }
            else
            {
                var session = CreateStripeSession(booking, resource!.Name);

                _dbContext.Payments.Add(new Payment
                {
                    BookingId = booking.Id,
                    AmountInCents = (long)Math.Round(totalPrice * 100m),
                    Currency = "eur",
                    StripeSessionId = session.Id,
                    Provider = "Stripe",
                    Status = "Pending"
                });
                _dbContext.SaveChanges();

                return Redirect(session.Url!);
            }
        }
        catch (Exception ex)
        {
            vm.AvailableResources = _resourceRepository.GetAll();
            var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "";
            vm.ErrorMessage = $"A apărut o eroare la salvare: {ex.Message}. Detalii: {innerMessage}";
            return View(vm);
        }
    }

    [Authorize]
    [HttpGet]
    public IActionResult Quote(Guid resourceId, DateTime startDate, DateTime endDate)
    {
        if (startDate.Date < DateTime.Today || endDate.Date <= startDate.Date)
        {
            return Json(new { valid = false, message = "Date invalide." });
        }

        var resource = _resourceRepository.GetById(resourceId);
        if (resource == null)
        {
            return Json(new { valid = false, message = "Resursa nu există." });
        }

        var days = Math.Max(1, (endDate.Date - startDate.Date).Days);
        var totalPrice = _pricingService.CalculateTotalPrice(resource, days);
        var availableSlots = _bookingService.GetAvailableSlots(resource, startDate.Date, endDate.Date);

        return Json(new
        {
            valid = true,
            totalPrice,
            availableSlots,
            isAvailable = availableSlots > 0
        });
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateCheckoutSession(Guid bookingId)
    {
        var booking = _bookingService.GetBookingById(bookingId);
        if (booking == null)
        {
            return NotFound();
        }

        var resource = _resourceRepository.GetById(booking.ResourceId);
        if (resource == null)
        {
            return NotFound();
        }

        var session = CreateStripeSession(booking, resource.Name);
        return Json(new { url = session.Url });
    }

    [HttpGet]
    public IActionResult PayPalSuccess(Guid bookingId, string token)
    {
        var clientId = _configuration["PayPal:ClientId"] ?? "dummy_client_id";
        var secret = _configuration["PayPal:Secret"] ?? "dummy_secret";
        var env = new SandboxEnvironment(clientId, secret);
        var client = new PayPalHttpClient(env);

        var request = new OrdersCaptureRequest(token);
        request.RequestBody(new OrderActionRequest());

        try
        {
            var response = client.Execute(request).Result;
            var result = response.Result<Order>();

            if (result.Status == "COMPLETED")
            {
                var payment = _dbContext.Payments.FirstOrDefault(p => p.BookingId == bookingId && p.Provider == "PayPal");
                if (payment != null)
                {
                    payment.Status = "Completed";
                    payment.PaidAtUtc = DateTime.UtcNow;
                    _dbContext.SaveChanges();
                }

                _bookingService.UpdateBookingStatus(bookingId, BookingStatus.Confirmed);
                return RedirectToAction("Success", new { bookingId = bookingId });
            }
        }
        catch (Exception)
        {
            // Dacă capture-ul eșuează (fonduri insuficiente sau anulare) 
        }

        return RedirectToAction("Cancel", new { bookingId = bookingId });
    }

    [HttpGet]
    public IActionResult Success(Guid bookingId)
    {
        var booking = _bookingService.GetBookingById(bookingId);
        return View(booking);
    }

    [HttpGet]
    public IActionResult Cancel(Guid bookingId)
    {
        _bookingService.UpdateBookingStatus(bookingId, BookingStatus.Cancelled);
        var booking = _bookingService.GetBookingById(bookingId);
        return View(booking);
    }

    [Authorize]
    [HttpGet]
    public IActionResult MyBookings()
    {
        var userId = TryGetCurrentUserId();
        if (userId == null)
        {
            return Challenge();
        }

        var resources = _resourceRepository.GetAll().ToDictionary(r => r.Id, r => r.Name);
        var rawBookings = _bookingService.GetBookingsForUser(userId.Value).ToList();
        var bookingIds = rawBookings.Select(b => b.Id).ToList();
        var payments = _dbContext.Payments.Where(p => bookingIds.Contains(p.BookingId)).ToDictionary(p => p.BookingId, p => p.Provider);

        var bookings = rawBookings
            .Select(b => new BookingListItemViewModel
            {
                Id = b.Id,
                ResourceName = resources.TryGetValue(b.ResourceId, out var name) ? name : "Resursă indisponibilă",
                UserName = b.UserFullName,
                UserEmail = b.UserEmail,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                GuestsCount = b.GuestsCount,
                TotalPrice = b.TotalPrice,
                Status = b.Status,
                CreatedAtUtc = b.CreatedAtUtc,
                PaymentProvider = payments.TryGetValue(b.Id, out var prov) ? prov : "Indisponibil"
            })
            .ToList();

        return View(bookings);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult AdminIndex()
    {
        var resources = _resourceRepository.GetAll().ToDictionary(r => r.Id, r => r.Name);
        var bookings = _bookingService
            .GetAllBookings()
            .OrderByDescending(b => b.CreatedAtUtc)
            .Select(b => new BookingListItemViewModel
            {
                Id = b.Id,
                ResourceName = resources.TryGetValue(b.ResourceId, out var name) ? name : "Resursă indisponibilă",
                UserName = b.UserFullName,
                UserEmail = b.UserEmail,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                GuestsCount = b.GuestsCount,
                TotalPrice = b.TotalPrice,
                Status = b.Status,
                CreatedAtUtc = b.CreatedAtUtc
            })
            .ToList();

        return View(bookings);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateStatus(Guid bookingId, BookingStatus status)
    {
        _bookingService.UpdateBookingStatus(bookingId, status);
        return RedirectToAction(nameof(AdminIndex));
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> StripeWebhook()
    {
        var webhookSecret = _configuration["Stripe:WebhookSecret"];
        if (string.IsNullOrWhiteSpace(webhookSecret))
        {
            return BadRequest("Webhook secret not configured.");
        }

        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                webhookSecret);

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;
                if (session?.Metadata?.TryGetValue("bookingId", out var bookingIdValue) == true
                    && Guid.TryParse(bookingIdValue, out var bookingId))
                {
                    _bookingService.UpdateBookingStatus(bookingId, BookingStatus.Confirmed);
                    var payment = _dbContext.Payments.FirstOrDefault(p => p.StripeSessionId == session.Id);
                    if (payment != null)
                    {
                        payment.Status = "Confirmed";
                        payment.StripePaymentIntentId = session.PaymentIntentId ?? string.Empty;
                        payment.PaidAtUtc = DateTime.UtcNow;
                        _dbContext.SaveChanges();
                    }
                }
            }

            if (stripeEvent.Type == "checkout.session.expired" || stripeEvent.Type == "checkout.session.async_payment_failed")
            {
                var session = stripeEvent.Data.Object as Session;
                if (session?.Metadata?.TryGetValue("bookingId", out var bookingIdValue) == true
                    && Guid.TryParse(bookingIdValue, out var bookingId))
                {
                    _bookingService.UpdateBookingStatus(bookingId, BookingStatus.Cancelled);
                    var payment = _dbContext.Payments.FirstOrDefault(p => p.StripeSessionId == session.Id);
                    if (payment != null)
                    {
                        payment.Status = "Cancelled";
                        _dbContext.SaveChanges();
                    }
                }
            }

            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    private BookingCreateViewModel BuildCreateViewModel(Guid resourceId, DateTime? startDate, DateTime? endDate)
    {
        var start = startDate?.Date ?? DateTime.Today;
        var end = endDate?.Date ?? start.AddDays(1);
        var resource = _resourceRepository.GetById(resourceId);
        var days = Math.Max(1, (end - start).Days);

        return new BookingCreateViewModel
        {
            ResourceId = resourceId,
            ResourceName = resource?.Name,
            FullName = GetCurrentUserName(),
            Email = User.Identity?.Name ?? string.Empty,
            StartDate = start,
            EndDate = end,
            TotalPrice = resource is null ? 0 : _pricingService.CalculateTotalPrice(resource, days),
            MinDate = DateTime.Today.ToString("yyyy-MM-dd"),
            AvailableResources = _resourceRepository.GetAll()
        };
    }

    private Session CreateStripeSession(Booking booking, string resourceName)
    {
        var amountInCents = (long)Math.Round(booking.TotalPrice * 100m);
        var domain = $"{Request.Scheme}://{Request.Host}";

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = ["card"],
            LineItems =
            [
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = amountInCents,
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Rezervare {resourceName}",
                            Description = $"{booking.StartDate:dd-MM-yyyy} - {booking.EndDate:dd-MM-yyyy}"
                        }
                    },
                    Quantity = 1
                }
            ],
            Mode = "payment",
            SuccessUrl = domain + $"/Booking/Success?bookingId={booking.Id}",
            CancelUrl = domain + $"/Booking/Cancel?bookingId={booking.Id}",
            CustomerEmail = booking.UserEmail,
            Metadata = new System.Collections.Generic.Dictionary<string, string>
            {
                ["bookingId"] = booking.Id.ToString()
            }
        };

        var service = new SessionService();
        return service.Create(options);
    }

    private Guid? TryGetCurrentUserId()
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(value, out var userId) ? userId : null;
    }

    private string GetCurrentUserName()
    {
        var claimName = User.FindFirstValue("full_name");
        if (!string.IsNullOrWhiteSpace(claimName))
        {
            return claimName;
        }

        var email = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(email))
        {
            return string.Empty;
        }

        var atIndex = email.IndexOf('@');
        return atIndex > 0 ? email[..atIndex] : email;
    }
}