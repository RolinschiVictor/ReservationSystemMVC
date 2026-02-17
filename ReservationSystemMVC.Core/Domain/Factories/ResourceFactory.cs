using ReservationSystemMVC.Core.Domain.Abstractions;

namespace ReservationSystemMVC.Core.Domain.Factories
{
    public abstract class ResourceFactory
    {
        // FACTORY METHOD
        public abstract IBookableResource CreateResource();
    }
}
