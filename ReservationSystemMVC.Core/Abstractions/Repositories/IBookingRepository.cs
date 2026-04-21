using System;
using System.Collections.Generic;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Enums;

namespace ReservationSystemMVC.Core.Abstractions.Repositories;

public interface IBookingRepository
{
    IEnumerable<Booking> GetBookingsByResource(Guid resourceId);
    IEnumerable<Booking> GetBookingsByUser(Guid userId);
    Booking? GetById(Guid bookingId);
    void AddBooking(Booking booking);
    IEnumerable<Booking> GetAll();
    void UpdateBookingStatus(Guid bookingId, BookingStatus newStatus);
}