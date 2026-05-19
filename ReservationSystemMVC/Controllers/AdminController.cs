using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Builders;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Factories.FactoryMethod;
using ReservationSystemMVC.Core.Patterns.Proxy;
using ReservationSystemMVC.Models.Admin;

namespace ReservationSystemMVC.Controllers;

// 6 & 8. Role-based Authorization: Only Admin can access
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IResourceRepository _resourceRepository;

    public AdminController(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public IActionResult Dashboard()
    {
        var resources = _resourceRepository.GetAll();
        return View(resources);
    }

    [HttpGet]
    public IActionResult CreateHotel()
    {
        return View(new CreateHotelViewModel());
    }

    [HttpPost]
    public IActionResult CreateHotel(CreateHotelViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // REAL USAGE OF BUILDER PATTERN
        var builder = new HotelRoomBuilder();
        HotelRoom newHotel = builder
            .SetName(model.Name)
            .SetBeds(model.Beds)
            .SetPricePerDay(model.PricePerDay)
            .SetDescription(model.Description)
            .SetLocation(model.City, model.Address, model.Country)
            .SetRating(model.Rating)
            .Build();

        _resourceRepository.Add(newHotel);

        return RedirectToAction(nameof(Dashboard));
    }

    [HttpGet]
    public IActionResult CreateResource()
    {
        return View(new CreateResourceViewModel());
    }

    [HttpPost]
    public IActionResult CreateResource(CreateResourceViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        ResourceFactoryMethod factory = model.ResourceType switch
        {
            "HotelRoom" => new HotelRoomFactory(),
            "Apartment" => new ApartmentFactory(),
            "EventVenue" => new EventVenueFactory(),
            _ => throw new ArgumentOutOfRangeException(nameof(model.ResourceType), model.ResourceType, "Unknown resource type")
        };

        var resource = factory.CreateResource(model.Name, model.SizeOrCapacity, model.PricePerDay);
        _resourceRepository.Add(resource);

        return RedirectToAction(nameof(Dashboard));
    }

    [HttpPost]
    public IActionResult CloneResource(Guid id)
    {
        var resource = _resourceRepository.GetById(id);
        if (resource != null)
        {
            // REAL USAGE OF PROTOTYPE PATTERN
            var clonedResource = resource.Clone();
            clonedResource.Name = clonedResource.Name + " (Copy)";
            _resourceRepository.Add(clonedResource);
        }
        return RedirectToAction(nameof(Dashboard));
    }

    [HttpGet]
    public IActionResult ResourceDocument(Guid id)
    {
        var resource = _resourceRepository.GetById(id);
        if (resource == null)
        {
            return NotFound();
        }

        var userRole = User.IsInRole("Admin") ? "Admin" : "User";
        IResourceDocument document = new ProxyResourceDocument($"Resource-{resource.Id}-document.txt", userRole);

        var content = document.GetContent();
        var bytes = System.Text.Encoding.UTF8.GetBytes(content);
        return File(bytes, "text/plain; charset=utf-8", $"resource-{resource.Id}-internal.txt");
    }
}