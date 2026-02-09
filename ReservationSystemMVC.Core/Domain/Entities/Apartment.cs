namespace ReservationSystemMVC.Core.Domain.Entities
{
    public class Apartment : BookableResource
    {
        public int Rooms { get; private set; }
        public decimal PricePerDay { get; private set; }

        public Apartment(string name, int rooms, decimal pricePerDay) : base(name)
        {
            if (rooms <= 0) throw new System.ArgumentException("Rooms invalid");
            if (pricePerDay <= 0) throw new System.ArgumentException("Price invalid");

            Rooms = rooms;
            PricePerDay = pricePerDay;
        }

        public override string Describe() => $"Apartment: {Name}, rooms={Rooms}, {PricePerDay}/day";
    }
}
