using System;
using System.Threading.Tasks;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Patterns.FactoryMethod;
using ReservationSystemMVC.Core.Patterns.Adapter;

namespace ReservationSystemMVC.Core.Patterns.Facade;

public class HotelReservationFacade
{
    private readonly IPaymentFactory _paymentFactory;

    public HotelReservationFacade(IPaymentFactory paymentFactory)
    {
        _paymentFactory = paymentFactory;
    }

    public async Task<PaymentSessionResponse> ProcessBookingPaymentAsync(Booking booking, BookableResource resource, string paymentProvider, string baseUrl)
    {
        Console.WriteLine($"--- Începere proces rezervare (prin Facade) pentru {booking.UserEmail} ---");

        // Factory & Adapter sub Facade ascund complexitatea fa?a de Controller
        var paymentProcessor = _paymentFactory.CreateProcessor(paymentProvider);
        
        var sessionResponse = await paymentProcessor.CreatePaymentSessionAsync(
            booking, 
            resource.Name, 
            booking.TotalPrice, 
            baseUrl);

        Console.WriteLine("--- Rezervare ini?iata cu succes prin Facade! ---");
        return sessionResponse;
    }
}
