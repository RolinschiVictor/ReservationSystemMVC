using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Core.Domain.ValueObjects;

namespace ReservationSystemMVC.Core.Domain.Builders;

/// BUILDER PATTERN – constructs a complex EventVenue step-by-step.

public class EventVenueBuilder
{
    private string _name = "Event";
    private int _capacity = 100;
    private decimal _pricePerDay = 500m;
    private string _description = "";
    private EventCategory _category = EventCategory.Other;
    private Location? _location;
    private DateOnly? _eventDate;
    private TimeOnly? _startTime;
    private TimeOnly? _endTime;
    private readonly List<string> _images = [];
    private string _organizer = "";
    private string _duration = "";
    private string? _ageRestriction;
    private EventFacility _facilities = EventFacility.None;
    private readonly List<Review> _reviews = [];

    public EventVenueBuilder SetName(string name) { _name = name; return this; }
    public EventVenueBuilder SetCapacity(int capacity) { _capacity = capacity; return this; }
    public EventVenueBuilder SetPricePerDay(decimal price) { _pricePerDay = price; return this; }
    public EventVenueBuilder SetDescription(string desc) { _description = desc; return this; }
    public EventVenueBuilder SetCategory(EventCategory cat) { _category = cat; return this; }
    public EventVenueBuilder SetLocation(string city, string address, string country) { _location = new Location(city, address, country); return this; }
    public EventVenueBuilder SetEventDate(DateOnly date) { _eventDate = date; return this; }
    public EventVenueBuilder SetTimes(TimeOnly start, TimeOnly end) { _startTime = start; _endTime = end; return this; }
    public EventVenueBuilder AddImage(string url) { _images.Add(url); return this; }
    public EventVenueBuilder SetOrganizer(string org) { _organizer = org; return this; }
    public EventVenueBuilder SetDuration(string dur) { _duration = dur; return this; }
    public EventVenueBuilder SetAgeRestriction(string? age) { _ageRestriction = age; return this; }
    public EventVenueBuilder SetFacilities(EventFacility fac) { _facilities = fac; return this; }
    public EventVenueBuilder AddReview(string user, double rating, string comment, DateOnly date) { _reviews.Add(new Review(user, rating, comment, date)); return this; }

    public EventVenue Build()
    {
        var ev = new EventVenue(_name, _capacity, _pricePerDay)
        {
            Description = _description,
            Category = _category,
            Location = _location,
            EventDate = _eventDate,
            StartTime = _startTime,
            EndTime = _endTime,
            Images = [.. _images],
            Organizer = _organizer,
            Duration = _duration,
            AgeRestriction = _ageRestriction,
            Facilities = _facilities,
            Reviews = [.. _reviews]
        };
        return ev;
    }
}
