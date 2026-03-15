using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Core.Domain.ValueObjects;

namespace ReservationSystemMVC.Core.Domain.Builders;

/// BUILDER PATTERN – constructs a complex Apartment step-by-step.

public class ApartmentBuilder
{
    private string _name = "Apartment";
    private int _rooms = 2;
    private decimal _pricePerDay = 80m;
    private string _description = "";
    private Location? _location;
    private double _rating;
    private readonly List<string> _images = [];
    private ApartmentFeature _features = ApartmentFeature.None;
    private int _beds;
    private int _bathrooms;
    private string _area = "";
    private int _maxGuests;
    private ApartmentRules? _rules;
    private readonly List<Review> _reviews = [];

    public ApartmentBuilder SetName(string name) { _name = name; return this; }
    public ApartmentBuilder SetRooms(int rooms) { _rooms = rooms; return this; }
    public ApartmentBuilder SetPricePerDay(decimal price) { _pricePerDay = price; return this; }
    public ApartmentBuilder SetDescription(string desc) { _description = desc; return this; }
    public ApartmentBuilder SetLocation(string city, string address, string country) { _location = new Location(city, address, country); return this; }
    public ApartmentBuilder SetRating(double rating) { _rating = rating; return this; }
    public ApartmentBuilder AddImage(string url) { _images.Add(url); return this; }
    public ApartmentBuilder SetFeatures(ApartmentFeature features) { _features = features; return this; }
    public ApartmentBuilder SetBeds(int beds) { _beds = beds; return this; }
    public ApartmentBuilder SetBathrooms(int bathrooms) { _bathrooms = bathrooms; return this; }
    public ApartmentBuilder SetArea(string area) { _area = area; return this; }
    public ApartmentBuilder SetMaxGuests(int max) { _maxGuests = max; return this; }
    public ApartmentBuilder SetRules(TimeOnly checkIn, TimeOnly checkOut, bool pets, bool smoking, bool parties) { _rules = new ApartmentRules(checkIn, checkOut, pets, smoking, parties); return this; }
    public ApartmentBuilder AddReview(string user, double rating, string comment, DateOnly date) { _reviews.Add(new Review(user, rating, comment, date)); return this; }

    public Apartment Build()
    {
        var apt = new Apartment(_name, _rooms, _pricePerDay)
        {
            Description = _description,
            Location = _location,
            Rating = _rating,
            Images = [.. _images],
            Features = _features,
            Beds = _beds,
            Bathrooms = _bathrooms,
            Area = _area,
            MaxGuests = _maxGuests,
            Rules = _rules,
            Reviews = [.. _reviews]
        };
        return apt;
    }
}
