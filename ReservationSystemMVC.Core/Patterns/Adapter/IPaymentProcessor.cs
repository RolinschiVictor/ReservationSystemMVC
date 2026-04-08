namespace ReservationSystemMVC.Core.Patterns.Adapter;

public interface IPaymentProcessor
{
    bool ProcessPayment(decimal amount, string currency);
}