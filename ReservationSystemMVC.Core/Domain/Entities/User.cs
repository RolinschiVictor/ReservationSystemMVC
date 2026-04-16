using System;

namespace ReservationSystemMVC.Core.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // In a real app, hash this!
    public string Role { get; set; } = "User"; // Default role
}