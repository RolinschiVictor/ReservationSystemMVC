using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Services.Pricing;
using ReservationSystemMVC.Core.Abstractions.Services;

namespace ReservationSystemMVC.Core.Domain.Factories.AbstractFactory
{
    /// <summary>
    /// Concrete Abstract Factory – creates an apartment resource + apartment pricing.
    /// </summary>
    public class ApartmentReservationFactory : IReservationFactory
    {
        public IBookableResource CreateResource()
        {
            return new Apartment("Apartment Standard", 2, 80m);
        }

        public IPricingPolicy CreatePricingPolicy()
        {
            return new ApartmentPricingPolicy();
        }
    }
}
