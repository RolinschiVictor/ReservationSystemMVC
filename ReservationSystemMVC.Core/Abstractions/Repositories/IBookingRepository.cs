using System;
using System.Collections.Generic;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Core.Abstractions.Repositories;

public interface IBookingRepository
{
    IEnumerable<Booking> GetBookingsByResource(Guid resourceId);
    void AddBooking(Booking booking);
    IEnumerable<Booking> GetAll();
}