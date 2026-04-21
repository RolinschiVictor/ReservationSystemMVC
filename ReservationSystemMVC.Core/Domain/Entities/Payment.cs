using System;

namespace ReservationSystemMVC.Core.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookingId { get; set; }
    public long AmountInCents { get; set; }
    public string Currency { get; set; } = "eur";
    public string StripeSessionId { get; set; } = string.Empty;
    public string StripePaymentIntentId { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? PaidAtUtc { get; set; }
}
