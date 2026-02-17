using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Abstractions;

namespace ReservationSystemMVC.Core.Domain.Factories
{
    public class HotelRoomFactory : ResourceFactory
    {
        public override IBookableResource CreateResource()
        {
            return new HotelRoom("Hotel Room #201", 2, 70m);
        }
    }
}
