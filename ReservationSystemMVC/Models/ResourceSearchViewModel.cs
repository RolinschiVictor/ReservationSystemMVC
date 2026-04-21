using System;
using System.Collections.Generic;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Models;

public class ResourceSearchViewModel
{
    public ResourceCategory? Category { get; set; }

    // yyyy-MM-dd from <input type="date">
    public DateOnly? StartDate { get; set; }

    public int Days { get; set; } = 1;

    public IEnumerable<BookableResource> Results { get; set; } = Array.Empty<BookableResource>();

    /// <summary>
    /// Total price per resource (calculated via Abstract Factory → IPricingPolicy).
    /// Key = resource Id, Value = total price for the selected number of days.
    /// </summary>
    public Dictionary<Guid, decimal> TotalPrices { get; set; } = [];

    /// <summary>
    /// Remaining slots for selected period.
    /// Key = resource Id, Value = remaining available reservation slots.
    /// </summary>
    public Dictionary<Guid, int> AvailableSlots { get; set; } = [];

    public string? Message { get; set; }
}
