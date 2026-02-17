using ReservationSystemMVC.Core.Abstractions.Services;
using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Core.Services.Pricing
{
    public class DefaultPricingPolicy : IPricingPolicy
    {
        public decimal CalculatePrice(IBookableResource resource, int days)
        {
            if (resource is HotelRoom hr)
            {
                return hr.PricePerDay * days;
            }

            // fallback: zero
            return 0m;
        }
    }
}
