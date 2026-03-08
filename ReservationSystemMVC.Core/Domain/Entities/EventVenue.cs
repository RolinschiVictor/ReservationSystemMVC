using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Core.Domain.ValueObjects;

namespace ReservationSystemMVC.Core.Domain.Entities
{
    public class EventVenue : BookableResource
    {
        public int Capacity { get; private set; }
        public decimal BasePricePerDay { get; private set; }

        // Professional properties
        public string Description { get; set; } = "";
        public EventCategory Category { get; set; } = EventCategory.Other;
        public Location? Location { get; set; }
        public DateOnly? EventDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public List<string> Images { get; set; } = [];
        public string Organizer { get; set; } = "";
        public string Duration { get; set; } = "";
        public string? AgeRestriction { get; set; }
        public EventFacility Facilities { get; set; } = EventFacility.None;
        public List<Review> Reviews { get; set; } = [];

        public EventVenue(string name, int capacity, decimal basePricePerDay) : base(name)
        {
            if (capacity <= 0) throw new System.ArgumentException("Capacity invalid");
            if (basePricePerDay <= 0) throw new System.ArgumentException("Price invalid");

            Capacity = capacity;
            BasePricePerDay = basePricePerDay;
        }

        public override string Describe() => $"EventVenue: {Name}, capacity={Capacity}, {BasePricePerDay}€/day";
    }
}
