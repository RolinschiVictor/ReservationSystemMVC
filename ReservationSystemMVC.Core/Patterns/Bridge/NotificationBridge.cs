using System;

namespace ReservationSystemMVC.Core.Patterns.Bridge;

// Implementor: Sistemul de expediere
public interface IMessageSender
{
    void SendMessage(string title, string body);
}

// Concrete Implementor 1
public class EmailSender : IMessageSender
{
    public void SendMessage(string title, string body)
    {
        Console.WriteLine($"[Email Renderer] Rendering and sending: {title} | {body}");
    }
}

// Concrete Implementor 2
public class PushNotificationSender : IMessageSender
{
    public void SendMessage(string title, string body)
    {
        Console.WriteLine($"[Push Renderer] Sending to device: {title} | {body}");
    }
}

// Abstraction: Tipul notificării (Confirmare Rezervare, Facturare, etc)
public abstract class NotificationAction
{
    protected IMessageSender _sender;

    protected NotificationAction(IMessageSender sender)
    {
        _sender = sender;
    }

    public abstract void Notify(string content);
}

// Refined Abstraction 1
public class BookingConfirmationNotification : NotificationAction
{
    public BookingConfirmationNotification(IMessageSender sender) : base(sender) { }

    public override void Notify(string content)
    {
        _sender.SendMessage("Booking Confirmed", $"Your booking details: {content}");
    }
}

// Refined Abstraction 2
public class PaymentReminderNotification : NotificationAction
{
    public PaymentReminderNotification(IMessageSender sender) : base(sender) { }

    public override void Notify(string content)
    {
        _sender.SendMessage("Payment Reminder", $"Please complete the payment for: {content}");
    }
}
