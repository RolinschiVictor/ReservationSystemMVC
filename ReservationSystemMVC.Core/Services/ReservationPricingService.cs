using ReservationSystemMVC.Core.Abstractions.Services;
using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Factories.AbstractFactory;

namespace ReservationSystemMVC.Core.Services
{
    /// <summary>
    /// Uses the Abstract Factory pattern to pick the right IReservationFactory
    /// based on resource type, then delegates pricing to the factory's IPricingPolicy.
    /// </summary>
    public class ReservationPricingService
    {
        /// <summary>
        /// Returns the correct Abstract Factory for a given resource type.
        /// </summary>
        public IReservationFactory GetFactory(BookableResource resource)
        {
            return resource switch
            {
                HotelRoom   => new HotelReservationFactory(),
                Apartment   => new ApartmentReservationFactory(),
                EventVenue  => new EventReservationFactory(),
                _ => throw new ArgumentException($"No factory for {resource.GetType().Name}")
            };
        }

        /// <summary>
        /// Calculates the total price for a resource using the Abstract Factory pattern:
        ///   1. Pick the right factory for the resource type
        ///   2. Factory creates the matching IPricingPolicy
        ///   3. Policy calculates the price
        /// </summary>
        public decimal CalculateTotalPrice(BookableResource resource, int days)
        {
            IReservationFactory factory = GetFactory(resource);
            IPricingPolicy policy = factory.CreatePricingPolicy();
            return policy.CalculatePrice(resource, days);
        }
    }
}
