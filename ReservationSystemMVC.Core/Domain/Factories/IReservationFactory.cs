using ReservationSystemMVC.Core.Domain.Abstractions;
using ReservationSystemMVC.Core.Abstractions.Services;

namespace ReservationSystemMVC.Core.Domain.Factories
{
    public interface IReservationFactory
    {
        IBookableResource CreateResource();
        IPricingPolicy CreatePricingPolicy();
    }
}
