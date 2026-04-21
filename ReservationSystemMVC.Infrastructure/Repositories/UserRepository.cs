using System.Linq;
using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Infrastructure.Data;

namespace ReservationSystemMVC.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
    }

    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public void PromoteUserToAdmin(string email)
    {
        var user = GetUserByEmail(email);
        if (user != null)
        {
            user.Role = "Admin";
            _dbContext.SaveChanges();
        }
    }
}
