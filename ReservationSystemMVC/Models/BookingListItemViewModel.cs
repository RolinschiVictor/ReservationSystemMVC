using System;
using ReservationSystemMVC.Core.Domain.Enums;

namespace ReservationSystemMVC.Models;

public class BookingListItemViewModel
{
    public Guid Id { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int GuestsCount { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
