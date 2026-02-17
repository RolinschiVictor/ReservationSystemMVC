using ReservationSystemMVC.Core.Domain.Abstractions;

namespace ReservationSystemMVC.Core.Abstractions.Services
{
    public interface IPricingPolicy
    {
        /// <summary>
        /// Calculate total price for a resource for a given number of days.
        /// </summary>
        decimal CalculatePrice(IBookableResource resource, int days);
    }
}
