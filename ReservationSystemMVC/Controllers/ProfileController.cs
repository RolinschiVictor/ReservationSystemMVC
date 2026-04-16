using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReservationSystemMVC.Controllers;

// Logged in users only (both User and Admin can access, or just User if restricted)
[Authorize]
public class ProfileController : Controller
{
    public IActionResult Index()
    {
        // User.Identity.Name holds the Email because we mapped ClaimTypes.Name to Email
        ViewBag.Email = User.Identity?.Name; 
        return View();
    }
}