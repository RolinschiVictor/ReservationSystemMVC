using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Models;

public sealed record ResourceListItem(
    Guid Id,
    ResourceCategory Category,
    string Name,
    string Description,
    decimal PricePerDay,
    int CapacityOrSize,
    string? City,
    string? Country,
    string? ImageUrl,
    BookableResource Domain
);
