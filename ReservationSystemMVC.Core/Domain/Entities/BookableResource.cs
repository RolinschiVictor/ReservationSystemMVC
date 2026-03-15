using ReservationSystemMVC.Core.Domain.Abstractions;
using System;

namespace ReservationSystemMVC.Core.Domain.Entities
{

    /// Base class for all bookable resources.
    /// Implements IPrototype for the PROTOTYPE PATTERN.

    public abstract class BookableResource : IBookableResource, IPrototype<BookableResource>
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public string Name { get; set; }

        protected BookableResource(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name invalid");
            Name = name.Trim();
        }

        public abstract string Describe();

        /// PROTOTYPE PATTERN – creates a deep copy with a new unique Id.

        public abstract BookableResource Clone();
    }
}
