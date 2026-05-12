using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Core.Domain.ValueObjects;

namespace ReservationSystemMVC.Core.Domain.Entities
{
    public class HotelRoom : BookableResource
    {
        public int Beds { get; private set; }
        public decimal PricePerDay { get => BasePricePerDay; set => BasePricePerDay = value; }

        // Professional properties
        public Location? Location { get; set; }
        public double Rating { get; set; }
        public new List<string> Images { get { return base.Images.ToList(); } set { base.Images = value.ToArray(); } }
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
            BasePricePerDay = pricePerDay;
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
