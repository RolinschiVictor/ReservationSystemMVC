using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Services.Pricing;
using ReservationSystemMVC.Core.Abstractions.Services;

namespace ReservationSystemMVC.Core.Domain.Factories
{
    public class EventReservationFactory : IReservationFactory
    {
        public IBookableResource CreateResource()
        {
            return new EventVenue("Event Hall Premium", 150, 200m);
        }

        public IPricingPolicy CreatePricingPolicy()
        {
            return new EventPricingPolicy();
        }
    }
}
