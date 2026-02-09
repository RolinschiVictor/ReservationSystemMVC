using System;
using System.Collections.Generic;
using System.Linq;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Infrastructure.Repositories
{
    public class InMemoryResourceRepository : IResourceRepository
    {
        private readonly List<BookableResource> _resources = new();

        public void Add(BookableResource resource) => _resources.Add(resource);

        public BookableResource? GetById(Guid id)
            => _resources.FirstOrDefault(r => r.Id == id);

        public IEnumerable<BookableResource> GetAll() => _resources;
    }
}
