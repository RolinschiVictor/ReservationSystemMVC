using System;

namespace ReservationSystemMVC.Core.Patterns.Facade;

public class HotelReservationFacade
{
    private readonly InventoryService _inventory = new();
    private readonly PricingService _pricing = new();
    private readonly PaymentService _payment = new();
    private readonly NotificationService _notification = new();

    public bool BookRoom(string hotelName, string roomType, DateOnly date, string clientEmail, string cardDetails)
    {
        Console.WriteLine($"--- Începere proces rezervare pentru {clientEmail} ---");

        if (!_inventory.CheckAvailability(hotelName, roomType, date))
        {
            Console.WriteLine("Rezervare eșuată: Cameră indisponibilă.");
            return false;
        }

        decimal totalAmount = _pricing.CalculateTotal(roomType, 1);

        if (!_payment.ProcessPayment(cardDetails, totalAmount))
        {
            Console.WriteLine("Rezervare eșuată: Plată respinsă.");
            return false;
        }

        _notification.SendEmailConfirmation(clientEmail);

        Console.WriteLine("--- Rezervare finalizată cu succes! ---");
        return true;
    }
}