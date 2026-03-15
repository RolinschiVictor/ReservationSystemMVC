using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Abstractions;

namespace ReservationSystemMVC.Core.Domain.Factories.FactoryMethod
{
    /// <summary>
    /// Concrete Factory Method – creates a <see cref="HotelRoom"/>.
    /// The caller just says "create a resource" without knowing it's a HotelRoom.
    /// </summary>
    public class HotelRoomFactory : ResourceFactoryMethod
    {
        public override BookableResource CreateResource(string name, int beds, decimal pricePerDay)
        {
            return new HotelRoom(name, beds, pricePerDay);
        }
    }
}
