using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Core.Domain.ValueObjects;

namespace ReservationSystemMVC.Core.Domain.Entities
{
    public class HotelRoom : BookableResource
    {
        public int Beds { get; private set; }
        public decimal PricePerDay { get; private set; }

        // Professional properties
        public string Description { get; set; } = "";
        public Location? Location { get; set; }
        public double Rating { get; set; }
        public List<string> Images { get; set; } = [];
        public List<RoomType> RoomTypes { get; set; } = [];
        public RoomFeature RoomFeatures { get; set; } = RoomFeature.None;
        public HotelAmenity Amenities { get; set; } = HotelAmenity.None;
        public HotelStyle Style { get; set; } = HotelStyle.None;
        public List<string> LanguagesSpoken { get; set; } = [];
        public List<Review> Reviews { get; set; } = [];

        public HotelRoom(string name, int beds, decimal pricePerDay) : base(name)
        {
            if (beds <= 0) throw new System.ArgumentException("Beds invalid");
            if (pricePerDay <= 0) throw new System.ArgumentException("Price invalid");

            Beds = beds;
            PricePerDay = pricePerDay;
        }

        public override string Describe() => $"HotelRoom: {Name}, beds={Beds}, {PricePerDay}/day";

        /// <summary>
        /// PROTOTYPE PATTERN – deep copy with new Id.
        /// Useful for creating hotel variations (e.g., same hotel, different city).
        /// </summary>
        public override BookableResource Clone()
        {
            var copy = new HotelRoom(Name, Beds, PricePerDay)
            {
                Description = Description,
                Location = Location,
                Rating = Rating,
                Images = [.. Images],
                RoomTypes = [.. RoomTypes],
                RoomFeatures = RoomFeatures,
                Amenities = Amenities,
                Style = Style,
                LanguagesSpoken = [.. LanguagesSpoken],
                Reviews = [.. Reviews]
            };
            return copy;
        }
    }
}
