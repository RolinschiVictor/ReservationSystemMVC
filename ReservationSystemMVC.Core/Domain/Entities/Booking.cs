using System;
using ReservationSystemMVC.Core.Domain.Enums;

namespace ReservationSystemMVC.Core.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    // Legătura către resursa rezervată (Hotel/Apartament/Eveniment)
    public Guid ResourceId { get; set; } 
    
    // Simplificăm utilizatorul cu un email pentru funcționalitatea curentă
    public string UserEmail { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
}