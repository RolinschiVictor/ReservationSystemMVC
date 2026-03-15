using ReservationSystemMVC.Core.Abstractions.Services;
using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Factories.AbstractFactory;

namespace ReservationSystemMVC.Core.Services
{
    /// SINGLETON PATTERN
    /// Only ONE instance of this service exists in the entire application.

    public sealed class ReservationPricingService
    {
        // ── Singleton infrastructure ──
        private static readonly Lazy<ReservationPricingService> _instance = new(() => new ReservationPricingService());


        /// The single, globally accessible instance.

        public static ReservationPricingService Instance => _instance.Value;

        /// Private constructor – nobody can create a second instance.

        private ReservationPricingService() { }

        // ── Business logic ──
        /// Returns the correct Abstract Factory for a given resource type.
   
        public IReservationFactory GetFactory(BookableResource resource)
        {
            return resource switch
            {
                HotelRoom  => new HotelReservationFactory(),
                Apartment  => new ApartmentReservationFactory(),
                EventVenue => new EventReservationFactory(),
                _ => throw new ArgumentException($"No factory for {resource.GetType().Name}")
            };
        }

        /// Calculates the total price for a resource using the Abstract Factory pattern

        public decimal CalculateTotalPrice(BookableResource resource, int days)
        {
            IReservationFactory factory = GetFactory(resource);
            IPricingPolicy policy = factory.CreatePricingPolicy();
            return policy.CalculatePrice(resource, days);
        }
    }
}
