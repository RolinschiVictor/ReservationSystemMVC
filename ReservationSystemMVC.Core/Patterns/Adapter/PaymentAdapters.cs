// ...existing code...
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Core.Patterns.Adapter;

public class PayPalAdapter : IPaymentProcessor
{
    private readonly string _clientId;
    private readonly string _secret;
    public string ProviderName => "PayPal";

    public PayPalAdapter(string clientId, string secret)
    {
        _clientId = clientId;
        _secret = secret;
    }

    public async Task<PaymentSessionResponse> CreatePaymentSessionAsync(Booking booking, string resourceName, decimal totalPrice, string baseUrl)
    {
        var env = new SandboxEnvironment(_clientId, _secret);
        var client = new PayPalHttpClient(env);
        var orderRequest = new OrdersCreateRequest();
        orderRequest.Prefer("return=representation");
        var amountFormatted = totalPrice.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

        var returnUrl = $"{(baseUrl.EndsWith('/') ? baseUrl.TrimEnd('/') : baseUrl)}/Booking/PayPalSuccess?bookingId={booking.Id}";
        var cancelUrl = $"{(baseUrl.EndsWith('/') ? baseUrl.TrimEnd('/') : baseUrl)}/Booking/Cancel?bookingId={booking.Id}";

        orderRequest.RequestBody(new OrderRequest()
        {
            CheckoutPaymentIntent = "CAPTURE",
            PurchaseUnits = new List<PurchaseUnitRequest>
            {
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown { CurrencyCode = "EUR", Value = amountFormatted },
                    Description = $"Rezervare {resourceName}"
                }
            },
            ApplicationContext = new ApplicationContext
            {
                ReturnUrl = returnUrl,
                CancelUrl = cancelUrl
            }
        });

        var response = await client.Execute(orderRequest);
        var result = response.Result<Order>();
        var approveLink = result.Links.FirstOrDefault(l => l.Rel == "approve")?.Href;

        return new PaymentSessionResponse
        {
            SessionId = result.Id,
            RedirectUrl = approveLink ?? string.Empty
        };
    }
}

public class StripeAdapter : IPaymentProcessor
{
    public string ProviderName => "Stripe";
    public async Task<PaymentSessionResponse> CreatePaymentSessionAsync(Booking booking, string resourceName, decimal totalPrice, string baseUrl)
    {
        var returnUrl = $"{(baseUrl.EndsWith('/') ? baseUrl.TrimEnd('/') : baseUrl)}/Booking/Success?bookingId={booking.Id}";
        var cancelUrl = $"{(baseUrl.EndsWith('/') ? baseUrl.TrimEnd('/') : baseUrl)}/Booking/Cancel?bookingId={booking.Id}";

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)Math.Round(totalPrice * 100m),
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions { Name = $"Rezervare {resourceName}" }
                    },
                    Quantity = 1,
                }
            },
            Mode = "payment",
            SuccessUrl = returnUrl,
            CancelUrl = cancelUrl,
            Metadata = new Dictionary<string, string> { { "bookingId", booking.Id.ToString() } }
        };
        var service = new SessionService();
        var session = await service.CreateAsync(options);
        return new PaymentSessionResponse { SessionId = session.Id, RedirectUrl = session.Url };
    }
}
// ...existing code...