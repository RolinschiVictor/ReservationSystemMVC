using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Services.Pricing;
using ReservationSystemMVC.Core.Abstractions.Services;

namespace ReservationSystemMVC.Core.Domain.Factories
{
    public class HotelReservationFactory : IReservationFactory
    {
        public IBookableResource CreateResource()
        {
            return new HotelRoom("Hotel Deluxe", 2, 100m);
        }

        public IPricingPolicy CreatePricingPolicy()
        {
            return new DefaultPricingPolicy();
        }
    }
}
