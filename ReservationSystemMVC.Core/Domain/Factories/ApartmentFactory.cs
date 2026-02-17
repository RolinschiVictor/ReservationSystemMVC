using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Abstractions;

namespace ReservationSystemMVC.Core.Domain.Factories
{
    public class ApartmentFactory : ResourceFactory
    {
        public override IBookableResource CreateResource()
        {
            return new Apartment("Apartment Premium", 3, 90m);
        }
    }
}
