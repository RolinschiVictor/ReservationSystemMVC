namespace ReservationSystemMVC.Core.Domain.Entities
{
    public class HotelRoom : BookableResource
    {
        public int Beds { get; private set; }
        public decimal PricePerDay { get; private set; }

        public HotelRoom(string name, int beds, decimal pricePerDay) : base(name)
        {
            if (beds <= 0) throw new System.ArgumentException("Beds invalid");
            if (pricePerDay <= 0) throw new System.ArgumentException("Price invalid");

            Beds = beds;
            PricePerDay = pricePerDay;
        }

        public override string Describe() => $"HotelRoom: {Name}, beds={Beds}, {PricePerDay}/day";
    }
}
