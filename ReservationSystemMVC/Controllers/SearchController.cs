using Microsoft.AspNetCore.Mvc;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Services;
using ReservationSystemMVC.Models;

namespace ReservationSystemMVC.Controllers;

public class SearchController : Controller
{
    private readonly IResourceRepository _resourceRepository;
    private readonly ReservationPricingService _pricingService;
    private readonly BookingService _bookingService;

    public SearchController(
        IResourceRepository resourceRepository,
        ReservationPricingService pricingService,
        BookingService bookingService)
    {
        _resourceRepository = resourceRepository;
        _pricingService = pricingService;
        _bookingService = bookingService;
    }

    [HttpGet]
    public IActionResult Index([FromQuery] ResourceSearchViewModel vm)
    {
        var days = vm.Days <= 0 ? 1 : vm.Days;
        vm.Days = days;

        var all = _resourceRepository.GetAll();

        if (vm.Category is null || vm.StartDate is null)
        {
            vm.Results = Array.Empty<BookableResource>();
            vm.Message = "Selectează tipul, data de început și durata, apoi apasă Caută.";
            return View(vm);
        }

        var startDate = vm.StartDate.Value.ToDateTime(TimeOnly.MinValue);
        if (startDate.Date < DateTime.Today)
        {
            vm.Results = Array.Empty<BookableResource>();
            vm.Message = "Nu poți căuta rezervări în trecut. Alege o dată de azi înainte.";
            return View(vm);
        }

        var filtered = (vm.Category.Value) switch
        {
            ResourceCategory.Hotel => all.OfType<HotelRoom>().Cast<BookableResource>(),
            ResourceCategory.Apartment => all.OfType<Apartment>().Cast<BookableResource>(),
            ResourceCategory.Event => all.OfType<EventVenue>().Cast<BookableResource>(),
            _ => Array.Empty<BookableResource>().AsEnumerable()
        };

        var endDate = startDate.AddDays(days);
        var prices = new Dictionary<Guid, decimal>();
        var availableSlots = new Dictionary<Guid, int>();
        var availableResources = new List<BookableResource>();

        foreach (var resource in filtered)
        {
            var slots = _bookingService.GetAvailableSlots(resource, startDate, endDate);
            if (slots <= 0)
            {
                continue;
            }

            availableResources.Add(resource);
            availableSlots[resource.Id] = slots;
            prices[resource.Id] = _pricingService.CalculateTotalPrice(resource, days);
        }

        vm.Results = availableResources;
        vm.TotalPrices = prices;
        vm.AvailableSlots = availableSlots;

        if (availableResources.Count == 0)
        {
            vm.Message = $"Nu există disponibilitate pentru {vm.Category} în perioada {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}.";
            return View(vm);
        }

        vm.Message = $"Rezultate pentru {vm.Category} — {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy} ({vm.Days} zile)";
        return View(vm);
    }
}
