using System;
using Microsoft.AspNetCore.Mvc;
using ReservationSystemMVC.Core.Services;
using ReservationSystemMVC.Core.Domain.Enums;
using Stripe.Checkout;

namespace ReservationSystemMVC.Controllers;

public class BookingController : Controller
{
    private readonly BookingService _bookingService;
    
    // Cheie Secretă STripe - Pentru producție trebuie citită din appsettings.json!
    private readonly string _stripeSecretKey = "sk_test_51YOUR_TEST_KEY_HERE";

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
        Stripe.StripeConfiguration.ApiKey = _stripeSecretKey;
    }

    [HttpGet]
    public IActionResult Create(Guid resourceId)
    {
        ViewBag.ResourceId = resourceId;
        return View();
    }

    [HttpPost]
    public IActionResult Create(Guid resourceId, DateTime startDate, DateTime endDate, string userEmail)
    {
        try
        {
            // 1. Crează rezervarea cu status "Pending"
            var booking = _bookingService.CreateBooking(resourceId, startDate, endDate, userEmail);
            
            // Calculăm un preț fictiv în acest moment (dacă ai preț pe BookableResource folosește-l)
            long priceInCents = 15000; // ex: $150.00
            
            var domain = $"{Request.Scheme}://{Request.Host}";

            // 2. Creează Sesiunea de Checkout Stripe
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new System.Collections.Generic.List<string> { "card" },
                LineItems = new System.Collections.Generic.List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = priceInCents,
                            Currency = "eur", // sau moneda dorita
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Rezervare Cazare",
                                Description = $"Rezervare de la {startDate:dd-MM-yyyy} până la {endDate:dd-MM-yyyy}",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                // Redirecționări către acțiunile noastre pentru success/cancel cu ID-ul rezervării atașat
                SuccessUrl = domain + $"/Booking/PaymentSuccess?bookingId={booking.Id}",
                CancelUrl = domain + $"/Booking/PaymentCancel?bookingId={booking.Id}",
                CustomerEmail = userEmail
            };

            var service = new SessionService();
            Session session = service.Create(options);

            // 3. Redirecționează vizitatorul către pagina complet securizată de plată Stripe
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            ViewBag.ResourceId = resourceId; 
            return View();
        }
    }

    // Acțiune chemată de Stripe când plata a reușit
    [HttpGet]
    public IActionResult PaymentSuccess(Guid bookingId)
    {
        // Actualizăm statusul la vizita cu succes
        _bookingService.UpdateBookingStatus(bookingId, BookingStatus.Confirmed);
        
        TempData["SuccessMessage"] = "Plaja s-a procesat cu succes. Rezervarea dvs. este confirmată!";
        // Redirecționăm către o pagină la alegere, ex Index din Home (schimbă după cum dorești)
        return RedirectToAction("Index", "Home"); 
    }

    // Acțiune chemată dacă utilizatorul anulează din checkout sau plata eșuează
    [HttpGet]
    public IActionResult PaymentCancel(Guid bookingId)
    {
        // Anulăm rezervarea sau o lăsăm pending ca omul sa poata reincepe
        _bookingService.UpdateBookingStatus(bookingId, BookingStatus.Cancelled);
        
        TempData["ErrorMessage"] = "Plata a fost anulată. Rezervarea nu a fost realizată.";
        return RedirectToAction("Index", "Home");
    }
}