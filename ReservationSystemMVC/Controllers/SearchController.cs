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

    public SearchController(IResourceRepository resourceRepository, ReservationPricingService pricingService)
    {
        _resourceRepository = resourceRepository;
        _pricingService = pricingService;
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
            vm.Message = "Selecteaz? tipul, data de început ?i durata, apoi apas? Caut?.";
            return View(vm);
        }

        var filtered = (vm.Category.Value) switch
        {
            ResourceCategory.Hotel => all.OfType<HotelRoom>().Cast<BookableResource>(),
            ResourceCategory.Apartment => all.OfType<Apartment>().Cast<BookableResource>(),
            ResourceCategory.Event => all.OfType<EventVenue>().Cast<BookableResource>(),
            _ => Array.Empty<BookableResource>().AsEnumerable()
        };

        vm.Results = filtered;

        // ====================================================
        // ABSTRACT FACTORY pattern in action:
        // For each result, calculate total price using
        // ReservationPricingService ? IReservationFactory ? IPricingPolicy
        // ====================================================
        var prices = new Dictionary<Guid, decimal>();
        foreach (var resource in filtered)
        {
            prices[resource.Id] = _pricingService.CalculateTotalPrice(resource, days);
        }
        vm.TotalPrices = prices;

        var endDate = vm.StartDate.Value.AddDays(days);
        vm.Message = $"Rezultate pentru {vm.Category} • {vm.StartDate:dd.MM.yyyy} ? {endDate:dd.MM.yyyy} ({vm.Days} zile)";
        return View(vm);
    }
}
