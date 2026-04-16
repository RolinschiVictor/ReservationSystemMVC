using System;
using System.Collections.Generic;
using System.Linq;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Enums;

namespace ReservationSystemMVC.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    // Utilizăm o listă In-Memory conform instrucțiunilor
    private static readonly List<Booking> _bookings = new();

    public IEnumerable<Booking> GetBookingsByResource(Guid resourceId)
    {
        // Returnăm doar rezervările care nu sunt anulate
        return _bookings.Where(b => b.ResourceId == resourceId && b.Status != BookingStatus.Cancelled);
    }

    public void AddBooking(Booking booking)
    {
        _bookings.Add(booking);
    }

    public IEnumerable<Booking> GetAll()
    {
        return _bookings;
    }
}