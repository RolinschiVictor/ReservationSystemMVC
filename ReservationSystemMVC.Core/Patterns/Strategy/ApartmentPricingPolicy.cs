using ReservationSystemMVC.Core.Abstractions.Services;
using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Core.Services.Pricing
{
    /// <summary>
    /// Pricing policy for apartments – price per day × number of days.
    /// </summary>
    public class ApartmentPricingPolicy : IPricingPolicy
    {
        public decimal CalculatePrice(IBookableResource resource, int days)
        {
            if (resource is Apartment apt)
            {
                return apt.PricePerDay * days;
            }

            return 0m;
        }
    }
}
