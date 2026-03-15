using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Core.Domain.ValueObjects;

namespace ReservationSystemMVC.Core.Domain.Builders;

/// BUILDER PATTERN 
/// Constructs a complex HotelRoom step-by-step with a fluent API.

public class HotelRoomBuilder
{
    private string _name = "Hotel";
    private int _beds = 2;
    private decimal _pricePerDay = 100m;
    private string _description = "";
    private Location? _location;
    private double _rating;
    private readonly List<string> _images = [];
    private readonly List<RoomType> _roomTypes = [];
    private RoomFeature _roomFeatures = RoomFeature.None;
    private HotelAmenity _amenities = HotelAmenity.None;
    private HotelStyle _style = HotelStyle.None;
    private readonly List<string> _languages = [];
    private readonly List<Review> _reviews = [];

    public HotelRoomBuilder SetName(string name) { _name = name; return this; }
    public HotelRoomBuilder SetBeds(int beds) { _beds = beds; return this; }
    public HotelRoomBuilder SetPricePerDay(decimal price) { _pricePerDay = price; return this; }
    public HotelRoomBuilder SetDescription(string desc) { _description = desc; return this; }
    public HotelRoomBuilder SetLocation(string city, string address, string country) { _location = new Location(city, address, country); return this; }
    public HotelRoomBuilder SetRating(double rating) { _rating = rating; return this; }
    public HotelRoomBuilder AddImage(string url) { _images.Add(url); return this; }
    public HotelRoomBuilder AddRoomType(string name, int capacity, decimal price, string size, params string[] features) { _roomTypes.Add(new RoomType(name, capacity, price, size, features)); return this; }
    public HotelRoomBuilder SetRoomFeatures(RoomFeature features) { _roomFeatures = features; return this; }
    public HotelRoomBuilder SetAmenities(HotelAmenity amenities) { _amenities = amenities; return this; }
    public HotelRoomBuilder SetStyle(HotelStyle style) { _style = style; return this; }
    public HotelRoomBuilder AddLanguage(string lang) { _languages.Add(lang); return this; }
    public HotelRoomBuilder AddLanguages(params string[] langs) { _languages.AddRange(langs); return this; }
    public HotelRoomBuilder AddReview(string user, double rating, string comment, DateOnly date) { _reviews.Add(new Review(user, rating, comment, date)); return this; }

    /// Assembles all parts and returns the final HotelRoom.

    public HotelRoom Build()
    {
        var hotel = new HotelRoom(_name, _beds, _pricePerDay)
        {
            Description = _description,
            Location = _location,
            Rating = _rating,
            Images = [.. _images],
            RoomTypes = [.. _roomTypes],
            RoomFeatures = _roomFeatures,
            Amenities = _amenities,
            Style = _style,
            LanguagesSpoken = [.. _languages],
            Reviews = [.. _reviews]
        };
        return hotel;
    }
}
