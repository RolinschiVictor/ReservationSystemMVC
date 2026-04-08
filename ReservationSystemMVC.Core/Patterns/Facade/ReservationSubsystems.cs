using System;

namespace ReservationSystemMVC.Core.Patterns.Facade;

public class InventoryService 
{
    public bool CheckAvailability(string hotelName, string roomType, DateOnly date)
    {
        Console.WriteLine($"[Inventory] Verific disponibilitate {roomType} la {hotelName} pe {date}.");
        return true; 
    }
}

public class PricingService 
{
    public decimal CalculateTotal(string roomType, int nights = 1)
    {
        Console.WriteLine($"[Pricing] Calcul preț pentru {roomType} ({nights} nopți).");
        return 150.00m * nights; 
    }
}

public class PaymentService 
{
    public bool ProcessPayment(string cardDetails, decimal amount)
    {
        Console.WriteLine($"[Payment] Procesare plată de {amount:C}...");
        return true; 
    }
}

public class NotificationService 
{
    public void SendEmailConfirmation(string clientEmail)
    {
        Console.WriteLine($"[Notification] Trimis confirmare la '{clientEmail}'.");
    }
}