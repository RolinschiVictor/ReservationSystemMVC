namespace ReservationSystemMVC.Core.Domain.ValueObjects;

public record Location(string City, string Address, string Country);

public record Review(string UserName, double Rating, string Comment, DateOnly Date);

public record RoomType(string RoomName, int Capacity, decimal Price, string RoomSize, string[] Features);

public record ApartmentRules(
    TimeOnly CheckIn,
    TimeOnly CheckOut,
    bool PetsAllowed,
    bool SmokingAllowed,
    bool PartiesAllowed
);
