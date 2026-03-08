using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Abstractions;

namespace ReservationSystemMVC.Core.Domain.Factories.FactoryMethod
{
    /// <summary>
    /// Concrete Factory Method – creates an <see cref="Apartment"/>.
    /// </summary>
    public class ApartmentFactory : ResourceFactoryMethod
    {
        public override BookableResource CreateResource(string name, int rooms, decimal pricePerDay)
        {
            return new Apartment(name, rooms, pricePerDay);
        }
    }
}
