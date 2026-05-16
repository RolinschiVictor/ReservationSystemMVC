using System;
using System.Text.Json;

namespace ReservationSystemMVC.Core.Patterns.Memento;

// The Memento class stores the internal state of the Draft
public class BookingDraftMemento
{
    public string StateJson { get; }

    public BookingDraftMemento(string stateJson)
    {
        StateJson = stateJson;
    }
}

// The Originator class that creates and restores from Mementos
public class BookingDraftOriginator
{
    public Guid ResourceId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int GuestsCount { get; set; }
    public string? SpecialRequests { get; set; }

    public BookingDraftMemento SaveToMemento()
    {
        var json = JsonSerializer.Serialize(this);
        return new BookingDraftMemento(json);
    }

    public void RestoreFromMemento(BookingDraftMemento memento)
    {
        var restored = JsonSerializer.Deserialize<BookingDraftOriginator>(memento.StateJson);
        if (restored != null)
        {
            this.ResourceId = restored.ResourceId;
            this.FullName = restored.FullName;
            this.Email = restored.Email;
            this.PhoneNumber = restored.PhoneNumber;
            this.StartDate = restored.StartDate;
            this.EndDate = restored.EndDate;
            this.GuestsCount = restored.GuestsCount;
            this.SpecialRequests = restored.SpecialRequests;
        }
    }
}