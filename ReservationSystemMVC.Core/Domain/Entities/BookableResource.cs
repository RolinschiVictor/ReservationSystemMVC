using ReservationSystemMVC.Core.Domain.Abstractions;
using System;

namespace ReservationSystemMVC.Core.Domain.Entities
{
    public abstract class BookableResource : IBookableResource
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; protected set; }

        protected BookableResource(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name invalid");
            Name = name.Trim();
        }

        public abstract string Describe();
    }
}
