using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Services;
using ReservationSystemMVC.Infrastructure;
using ReservationSystemMVC.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// Singleton – same in-memory list shared across all requests (seed data persists)
builder.Services.AddSingleton<IResourceRepository, InMemoryResourceRepository>();
builder.Services.AddSingleton<ReservationPricingService>();

var app = builder.Build();

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
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();



