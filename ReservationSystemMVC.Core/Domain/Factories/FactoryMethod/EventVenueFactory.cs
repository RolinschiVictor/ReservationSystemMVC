using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Core.Domain.Factories.FactoryMethod
{
    /// <summary>
    /// Concrete Factory Method – creates an <see cref="EventVenue"/>.
    /// </summary>
    public class EventVenueFactory : ResourceFactoryMethod
    {
        public override BookableResource CreateResource(string name, int capacity, decimal basePricePerDay)
        {
            return new EventVenue(name, capacity, basePricePerDay);
        }
    }
}
