using System;
using System.Collections.Generic;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Core.Abstractions.Repositories
{
    public interface IResourceRepository
    {
        void Add(BookableResource resource);
        BookableResource? GetById(Guid id);
        IEnumerable<BookableResource> GetAll();
    }
}
