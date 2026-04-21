using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Services;
using ReservationSystemMVC.Infrastructure;
using ReservationSystemMVC.Infrastructure.Data;
using ReservationSystemMVC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// Singleton – same in-memory list shared across all requests (seed data persists)
builder.Services.AddSingleton<IResourceRepository, InMemoryResourceRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<BookingService>();
// SINGLETON PATTERN – register the single instance from the Singleton class itself
builder.Services.AddSingleton(ReservationPricingService.Instance);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    AuthDataSeeder.Seed(dbContext);
}

// Seed realistic data using Factory Method pattern
var repo = app.Services.GetRequiredService<IResourceRepository>();
DataSeeder.Seed(repo);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();



