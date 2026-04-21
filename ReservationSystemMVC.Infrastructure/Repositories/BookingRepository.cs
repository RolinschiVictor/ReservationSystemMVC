using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Infrastructure.Data;

namespace ReservationSystemMVC.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Booking> GetBookingsByResource(Guid resourceId)
    {
        // Returnăm doar rezervările care nu sunt anulate
        return _dbContext.Bookings
            .Where(b => b.ResourceId == resourceId && b.Status != BookingStatus.Cancelled)
            .AsNoTracking()
            .ToList();
    }

    public IEnumerable<Booking> GetBookingsByUser(Guid userId)
    {
        return _dbContext.Bookings
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.CreatedAtUtc)
            .AsNoTracking()
            .ToList();
    }

    public Booking? GetById(Guid bookingId)
    {
        return _dbContext.Bookings.FirstOrDefault(b => b.Id == bookingId);
    }

    public void AddBooking(Booking booking)
    {
        _dbContext.Bookings.Add(booking);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Booking> GetAll()
    {
        return _dbContext.Bookings.AsNoTracking().ToList();
    }

    public void UpdateBookingStatus(Guid bookingId, BookingStatus newStatus)
    {
        var booking = GetById(bookingId);
        if (booking == null)
        {
            return;
        }

        booking.Status = newStatus;
        _dbContext.SaveChanges();
    }
}