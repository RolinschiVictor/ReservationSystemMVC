using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Core.Domain.Factories.FactoryMethod
{
    /// <summary>
    /// Factory Method pattern – defines the abstract creation method.
    /// Subclasses override <see cref="CreateResource"/> to produce concrete products.
    /// The caller doesn't know (or care) which concrete class is instantiated.
    /// </summary>
    public abstract class ResourceFactoryMethod
    {
        /// <summary>
        /// Factory Method – subclasses decide the concrete type.
        /// </summary>
        public abstract BookableResource CreateResource(string name, int sizeOrCapacity, decimal pricePerDay);
    }
}
