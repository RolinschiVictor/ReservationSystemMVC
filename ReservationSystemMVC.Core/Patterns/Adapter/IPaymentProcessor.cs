using System;
using System.Threading.Tasks;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Core.Patterns.Adapter;

public interface IPaymentProcessor
{
    string ProviderName { get; }
    Task<PaymentSessionResponse> CreatePaymentSessionAsync(Booking booking, string resourceName, decimal totalPrice, string baseUrl);
}