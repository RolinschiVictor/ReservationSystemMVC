using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Services;
using ReservationSystemMVC.Infrastructure;
using ReservationSystemMVC.Infrastructure.Data;
using ReservationSystemMVC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Configure Stripe API Key
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"] ?? "sk_test_1234567890abcdef";

builder.Services.AddControllersWithViews();
// Singleton � same in-memory list shared across all requests (seed data persists)
builder.Services.AddSingleton<IResourceRepository, InMemoryResourceRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<BookingService>();
// OBSERVER PATTERN - register the notifier so it can be injected into BookingService
builder.Services.AddSingleton<ReservationSystemMVC.Core.Patterns.Observer.IObservable, ReservationSystemMVC.Core.Patterns.Observer.ResourceAvailabilityNotifier>();
builder.Services.AddScoped<ReservationSystemMVC.Core.Patterns.FactoryMethod.IPaymentFactory, ReservationSystemMVC.Core.Patterns.FactoryMethod.PaymentFactory>();
// SINGLETON PATTERN � register the single instance from the Singleton class itself
builder.Services.AddSingleton(ReservationPricingService.Instance);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();

// SESSION - required for Draft (Memento/Command Pattern)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    AuthDataSeeder.Seed(dbContext);
}

// Seed realistic data using Factory Method pattern
var repo = app.Services.GetRequiredService<IResourceRepository>();
DataSeeder.Seed(repo);

// Attach a simple BookingLoggerObserver to the notifier so the app reacts to booking events
var notifier = app.Services.GetRequiredService<ReservationSystemMVC.Core.Patterns.Observer.IObservable>();
if (notifier is ReservationSystemMVC.Core.Patterns.Observer.ResourceAvailabilityNotifier ran)
{
    ran.Attach(new ReservationSystemMVC.Core.Patterns.Observer.BookingLoggerObserver());
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();




