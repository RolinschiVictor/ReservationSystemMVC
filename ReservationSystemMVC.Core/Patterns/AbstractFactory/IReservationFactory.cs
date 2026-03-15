using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Abstractions.Services;

namespace ReservationSystemMVC.Core.Domain.Factories.AbstractFactory
{
    /// <summary>
    /// Abstract Factory pattern – declares a family of related products:
    /// a bookable resource and its associated pricing policy.
    /// </summary>
    public interface IReservationFactory
    {
        IBookableResource CreateResource();
        IPricingPolicy CreatePricingPolicy();
    }
}
