using System.ComponentModel.DataAnnotations;

namespace ReservationSystemMVC.Models.Admin;

public class CreateResourceViewModel
{
    [Required]
    public string ResourceType { get; set; } = "HotelRoom";

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue)]
    public int SizeOrCapacity { get; set; } = 1;

    [Required]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal PricePerDay { get; set; } = 100m;
}
