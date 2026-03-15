using ReservationSystemMVC.Core.Abstractions.Services;
using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Core.Services.Pricing
{
    public class EventPricingPolicy : IPricingPolicy
    {
        public decimal CalculatePrice(IBookableResource resource, int days)
        {
            if (resource is EventVenue ev)
            {
                return ev.BasePricePerDay * days;
            }

            return 0m;
        }
    }
}
