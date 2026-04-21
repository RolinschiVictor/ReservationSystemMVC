using System;
using System.ComponentModel.DataAnnotations.Schema;
using ReservationSystemMVC.Core.Domain.Enums;

namespace ReservationSystemMVC.Core.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    // Legătura către resursa rezervată (Hotel/Apartament/Eveniment)
    public Guid ResourceId { get; set; } 

    [NotMapped]
    public Guid ItemId
    {
        get => ResourceId;
        set => ResourceId = value;
    }

    public Guid? UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    
    // Simplificăm utilizatorul cu un email pentru funcționalitatea curentă
    public string UserEmail { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int GuestsCount { get; set; }
    public string? SpecialRequests { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
}