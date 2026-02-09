namespace ReservationSystemMVC.Core.Domain.Entities
{
    public class EventVenue : BookableResource
    {
        public int Capacity { get; private set; }
        public decimal BasePricePerDay { get; private set; }

        public EventVenue(string name, int capacity, decimal basePricePerDay) : base(name)
        {
            if (capacity <= 0) throw new System.ArgumentException("Capacity invalid");
            if (basePricePerDay <= 0) throw new System.ArgumentException("Price invalid");

            Capacity = capacity;
            BasePricePerDay = basePricePerDay;
        }

        public override string Describe() => $"EventVenue: {Name}, capacity={Capacity}, {BasePricePerDay}/day";
    }
}
