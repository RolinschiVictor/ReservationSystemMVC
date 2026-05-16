using System.ComponentModel.DataAnnotations;

namespace ReservationSystemMVC.Models.Admin;

public class CreateHotelViewModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public int Beds { get; set; } = 2;
    [Required]
    public decimal PricePerDay { get; set; } = 100m;
    public string Description { get; set; } = string.Empty;

    // Address
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public double Rating { get; set; } = 5.0;
}
