using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
    {
        var user = _userRepository.GetUserByEmail(email);

        // 5. Fix Login logic with Claims
        if (user != null && user.PasswordHash == password) // Simple plain-text check for demo
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("full_name", user.FullName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // 7. Redirect after login
            if (user.Role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        ViewBag.ErrorMessage = "Invalid email or password.";
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string fullName, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            ViewBag.ErrorMessage = "Numele este obligatoriu.";
            return View();
        }

        if (_userRepository.GetUserByEmail(email) != null)
        {
            ViewBag.ErrorMessage = "Email already in use.";
            return View();
        }

        // 2. Fix Register logic (Force Role = "User")
        var newUser = new User
        {
            FullName = fullName,
            Email = email,
            PasswordHash = password, // Remember to hash in real apps
            Role = "User" // NEVER trust UI for the role
        };

        _userRepository.AddUser(newUser);

        ViewBag.SuccessMessage = "Registration successful! You can now login.";
        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}