using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Infrastructure.Data;

public static class AuthDataSeeder
{
    public static void Seed(ApplicationDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        if (!dbContext.Users.Any(u => u.Email == "admin@test.com"))
        {
            dbContext.Users.Add(new User
            {
                FullName = "Admin User",
                Email = "admin@test.com",
                PasswordHash = "admin123",
                Role = "Admin"
            });
        }

        if (!dbContext.Users.Any(u => u.Email == "user@test.com"))
        {
            dbContext.Users.Add(new User
            {
                FullName = "Demo User",
                Email = "user@test.com",
                PasswordHash = "user123",
                Role = "User"
            });
        }

        dbContext.SaveChanges();
    }
}
