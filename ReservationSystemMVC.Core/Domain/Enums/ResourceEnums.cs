namespace ReservationSystemMVC.Core.Domain.Enums;

[Flags]
public enum RoomFeature
{
    None = 0,
    WiFi = 1 << 0,
    AirConditioning = 1 << 1,
    TV = 1 << 2,
    PrivateBathroom = 1 << 3,
    Balcony = 1 << 4,
    MiniBar = 1 << 5,
    Desk = 1 << 6,
    CoffeeMachine = 1 << 7
}

[Flags]
public enum HotelAmenity
{
    None = 0,
    FreeWiFi = 1 << 0,
    Parking = 1 << 1,
    Restaurant = 1 << 2,
    Bar = 1 << 3,
    Spa = 1 << 4,
    SwimmingPool = 1 << 5,
    Gym = 1 << 6,
    AirportShuttle = 1 << 7,
    RoomService = 1 << 8,
    Reception24h = 1 << 9
}

[Flags]
public enum HotelStyle
{
    None = 0,
    Luxury = 1 << 0,
    Budget = 1 << 1,
    Boutique = 1 << 2,
    FamilyFriendly = 1 << 3,
    Business = 1 << 4,
    Romantic = 1 << 5
}

[Flags]
public enum ApartmentFeature
{
    None = 0,
    Kitchen = 1 << 0,
    WashingMachine = 1 << 1,
    Balcony = 1 << 2,
    WiFi = 1 << 3,
    AirConditioning = 1 << 4,
    TV = 1 << 5,
    Parking = 1 << 6,
    Elevator = 1 << 7
}

public enum EventCategory
{
    Concert,
    Festival,
    Conference,
    Exhibition,
    Workshop,
    SportEvent,
    Theater,
    Other
}

[Flags]
public enum EventFacility
{
    None = 0,
    Parking = 1 << 0,
    Food = 1 << 1,
    Bar = 1 << 2,
    VIPArea = 1 << 3,
    SeatsAvailable = 1 << 4
}
