using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Core.Domain.ValueObjects;

namespace ReservationSystemMVC.Core.Domain.Entities
{
    public class Apartment : BookableResource
    {
        public int Rooms { get; private set; }
        public decimal PricePerDay { get => BasePricePerDay; set => BasePricePerDay = value; }

        // Professional properties
        public Location? Location { get; set; }
        public double Rating { get; set; }
        public new List<string> Images { get { return base.Images.ToList(); } set { base.Images = value.ToArray(); } }
        public ApartmentFeature Features { get; set; } = ApartmentFeature.None;
        public int Beds { get; set; }
        public int Bathrooms { get; set; }
        public string Area { get; set; } = "";
        public int MaxGuests { get; set; }
        public ApartmentRules? Rules { get; set; }
        public List<Review> Reviews { get; set; } = [];

        public Apartment(string name, int rooms, decimal pricePerDay) : base(name)
        {
            if (rooms <= 0) throw new System.ArgumentException("Rooms invalid");
            if (pricePerDay <= 0) throw new System.ArgumentException("Price invalid");

            Rooms = rooms;
            BasePricePerDay = pricePerDay;
        }

        public override string Describe() => $"Apartment: {Name}, rooms={Rooms}, {PricePerDay}€/day";

        /// <summary>
        /// PROTOTYPE PATTERN – deep copy with new Id.
        /// Useful for creating apartment variations in different cities.
        /// </summary>
        public override BookableResource Clone()
        {
            var copy = new Apartment(Name, Rooms, PricePerDay)
            {
                Description = Description,
                Location = Location,
                Rating = Rating,
                Images = [.. Images],
                Features = Features,
                Beds = Beds,
                Bathrooms = Bathrooms,
                Area = Area,
                MaxGuests = MaxGuests,
                Rules = Rules,
                Reviews = [.. Reviews]
            };
            return copy;
        }
    }
}
