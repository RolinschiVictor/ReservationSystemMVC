using System;
using System.Linq;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Enums;

namespace ReservationSystemMVC.Core.Services;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public bool IsAvailable(Guid resourceId, DateTime start, DateTime end)
    {
        var existingBookings = _bookingRepository.GetBookingsByResource(resourceId);
        // Excludem rezervările anulate din verificarea suprapunerilor
        bool hasOverlap = existingBookings.Any(b => start < b.EndDate && end > b.StartDate && b.Status != BookingStatus.Cancelled);
        return !hasOverlap;
    }

    public Booking CreateBooking(Guid resourceId, DateTime start, DateTime end, string userEmail)
    {
        if (start.Date < DateTime.Now.Date)
            throw new ArgumentException("Data de început nu poate fi în trecut.");
            
        if (start >= end)
            throw new ArgumentException("Data de destrămare (Sfârșit) trebuie să fie după data de început.");

        if (!IsAvailable(resourceId, start, end))
            throw new InvalidOperationException("Perioada selectată este indisponibilă. Vă rugăm să selectați alte date.");

        var booking = new Booking
        {
            ResourceId = resourceId,
            UserEmail = userEmail,
            StartDate = start,
            EndDate = end,
            Status = BookingStatus.Pending // Rămâne pe Pending până e plătită
        };

        _bookingRepository.AddBooking(booking);

        return booking;
    }

    // Nouă metodă pentru actualizarea statusului după plata Stripe
    public void UpdateBookingStatus(Guid bookingId, BookingStatus newStatus)
    {
        var allBookings = _bookingRepository.GetAll();
        var booking = allBookings.FirstOrDefault(b => b.Id == bookingId);
        
        if (booking != null)
        {
            booking.Status = newStatus;
        }
    }
}