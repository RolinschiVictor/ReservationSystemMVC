using System;

namespace ReservationSystemMVC.Core.Patterns.Adapter;

public class PayPalApi
{
    public bool SendPayment(decimal value)
    {
        Console.WriteLine($"[PayPal] Se procesează plata de {value}.");
        return true;
    }
}

public class StripeApi
{
    public bool MakeCharge(int amountInCents, string currency)
    {
        Console.WriteLine($"[Stripe] Se încasează {amountInCents} cenți în {currency}.");
        return true;
    }
}