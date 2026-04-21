using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Models;

public class BookingCreateViewModel
{
    [Required]
    public Guid ResourceId { get; set; }

    public IEnumerable<BookableResource> AvailableResources { get; set; } = Array.Empty<BookableResource>();

    [Required(ErrorMessage = "Numele este obligatoriu.")]
    [Display(Name = "Nume și prenume")]
    [StringLength(200)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Numărul de telefon este obligatoriu.")]
    [Display(Name = "Număr de telefon")]
    [Phone]
    [StringLength(32)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Data check-in")]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Data check-out")]
    public DateTime EndDate { get; set; }

    [Range(1, 2000, ErrorMessage = "Număr persoane invalid.")]
    [Display(Name = "Număr persoane")]
    public int GuestsCount { get; set; } = 1;

    [Display(Name = "Cerințe speciale")]
    [StringLength(1000)]
    public string? SpecialRequests { get; set; }

    public decimal TotalPrice { get; set; }
    public string MinDate { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");
    public string? ResourceName { get; set; }
    public string? ErrorMessage { get; set; }
}
