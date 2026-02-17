using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Factories;
using ReservationSystemMVC.Core.Abstractions;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IResourceRepository, InMemoryResourceRepository>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetRequiredService<IResourceRepository>();

    repo.Add(new ReservationSystemMVC.Core.Domain.Entities.HotelRoom("HotelRoom #101", 2, 60m));
    repo.Add(new ReservationSystemMVC.Core.Domain.Entities.Apartment("Apartment Center", 3, 80m));
    repo.Add(new ReservationSystemMVC.Core.Domain.Entities.EventVenue("Event Hall A", 100, 150m));
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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


