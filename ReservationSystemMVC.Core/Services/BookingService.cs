using System;
using System.Collections.Generic;
using System.Linq;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Enums;

namespace ReservationSystemMVC.Core.Services;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IResourceRepository _resourceRepository;

    public BookingService(IBookingRepository bookingRepository, IResourceRepository resourceRepository)
    {
        _bookingRepository = bookingRepository;
        _resourceRepository = resourceRepository;
    }

    private static int GetReservationLimit(BookableResource resource) => resource switch
    {
        Apartment => 1,
        HotelRoom => 20,
        EventVenue ev => Math.Max(1, ev.Capacity),
        _ => 1
    };

    public int GetOverlappingActiveBookingsCount(Guid resourceId, DateTime start, DateTime end)
    {
        var existingBookings = _bookingRepository.GetBookingsByResource(resourceId);
        return existingBookings.Count(b => start < b.EndDate && end > b.StartDate && b.Status != BookingStatus.Cancelled);
    }

    public int GetAvailableSlots(BookableResource resource, DateTime start, DateTime end)
    {
        var limit = GetReservationLimit(resource);
        var overlappingCount = GetOverlappingActiveBookingsCount(resource.Id, start, end);
        return Math.Max(0, limit - overlappingCount);
    }

    public bool IsAvailable(Guid resourceId, DateTime start, DateTime end)
    {
        var resource = _resourceRepository.GetById(resourceId)
            ?? throw new InvalidOperationException("Resursa selectată nu există.");

        return GetAvailableSlots(resource, start, end) > 0;
    }

    public BookableResource GetResourceById(Guid resourceId)
    {
        return _resourceRepository.GetById(resourceId)
            ?? throw new InvalidOperationException("Resursa selectată nu există.");
    }

    public Booking CreateBooking(
        Guid resourceId,
        DateTime start,
        DateTime end,
        Guid? userId,
        string userFullName,
        string userEmail,
        string phoneNumber,
        int guestsCount,
        string? specialRequests,
        decimal totalPrice)
    {
        var startDate = start.Date;
        var endDate = end.Date;

        if (startDate < DateTime.Today)
            throw new ArgumentException("Data de început nu poate fi în trecut.");

        if (startDate >= endDate)
            throw new ArgumentException("Data de sfârșit trebuie să fie după data de început.");

        if (guestsCount <= 0)
            throw new ArgumentException("Numărul de persoane trebuie să fie mai mare ca 0.");

        var resource = _resourceRepository.GetById(resourceId)
            ?? throw new InvalidOperationException("Resursa selectată nu există.");

        if (GetAvailableSlots(resource, startDate, endDate) <= 0)
            throw new InvalidOperationException("Resursa selectată este deja ocupată pentru perioada aleasă. Alege alte date.");

        var booking = new Booking
        {
            ResourceId = resourceId,
            UserId = userId,
            UserFullName = userFullName,
            UserEmail = userEmail,
            PhoneNumber = phoneNumber,
            GuestsCount = guestsCount,
            SpecialRequests = specialRequests,
            StartDate = startDate,
            EndDate = endDate,
            TotalPrice = totalPrice,
            Status = BookingStatus.Pending
        };

        _bookingRepository.AddBooking(booking);

        return booking;
    }

    // Nouă metodă pentru actualizarea statusului după plata Stripe
    public void UpdateBookingStatus(Guid bookingId, BookingStatus newStatus)
    {
        _bookingRepository.UpdateBookingStatus(bookingId, newStatus);
    }

    public Booking? GetBookingById(Guid bookingId)
    {
        return _bookingRepository.GetById(bookingId);
    }

    public IEnumerable<Booking> GetBookingsForUser(Guid userId)
    {
        return _bookingRepository.GetBookingsByUser(userId);
    }

    public IEnumerable<Booking> GetAllBookings()
    {
        return _bookingRepository.GetAll();
    }
}