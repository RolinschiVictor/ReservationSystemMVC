using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Infrastructure.Repositories;

namespace ReservationSystemMVC.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public UserRepository()
    {
        // 3. Create an Admin user manually (Seed Data)
        _users.Add(new User
        {
            Email = "admin@test.com",
            PasswordHash = "admin123", // DO NOT do this in production (Use BCrypt/Argon2)
            Role = "Admin"
        });
        
        // Seed a standard user to test with
        _users.Add(new User
        {
            Email = "user@test.com",
            PasswordHash = "user123", 
            Role = "User"
        });
    }

    public User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }
}

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 4. Configure Authentication
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

        services.AddAuthorization();

        services.AddSingleton<IUserRepository, UserRepository>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();

        // Ensure these are called IN THIS ORDER, between UseRouting and UseEndpoints/MapControllers
        app.UseAuthentication(); 
        app.UseAuthorization();
    }
}