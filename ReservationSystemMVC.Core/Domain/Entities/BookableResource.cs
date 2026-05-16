using ReservationSystemMVC.Core.Domain.Abstractions;
using System;

namespace ReservationSystemMVC.Core.Domain.Entities
{

    /// Base class for all bookable resources.
    /// Implements IPrototype for the PROTOTYPE PATTERN.

    public abstract class BookableResource : IBookableResource, IPrototype<BookableResource>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal BasePricePerDay { get; set; }
        public string[] Images { get; set; } = Array.Empty<string>();

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
