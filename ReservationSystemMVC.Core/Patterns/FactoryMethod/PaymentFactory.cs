using Microsoft.Extensions.Configuration;
using ReservationSystemMVC.Core.Patterns.Adapter;

namespace ReservationSystemMVC.Core.Patterns.FactoryMethod;

public interface IPaymentFactory
{
    IPaymentProcessor CreateProcessor(string providerName);
}

public class PaymentFactory : IPaymentFactory
{
    private readonly IConfiguration _configuration;

    public PaymentFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IPaymentProcessor CreateProcessor(string providerName)
    {
        if (providerName == "PayPal")
        {
            var clientId = _configuration["PayPal:ClientId"] ?? string.Empty;
            var secret = _configuration["PayPal:Secret"] ?? string.Empty;
            return new PayPalAdapter(clientId, secret);
        }

        return new StripeAdapter();
    }
}