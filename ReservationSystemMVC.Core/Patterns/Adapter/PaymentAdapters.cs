namespace ReservationSystemMVC.Core.Patterns.Adapter;

public class PayPalAdapter : IPaymentProcessor
{
    private readonly PayPalApi _payPalApi = new();

    public bool ProcessPayment(decimal amount, string currency)
    {
        return _payPalApi.SendPayment(amount);
    }
}

public class StripeAdapter : IPaymentProcessor
{
    private readonly StripeApi _stripeApi = new();

    public bool ProcessPayment(decimal amount, string currency)
    {
        int cents = (int)(amount * 100);
        return _stripeApi.MakeCharge(cents, currency);
    }
}