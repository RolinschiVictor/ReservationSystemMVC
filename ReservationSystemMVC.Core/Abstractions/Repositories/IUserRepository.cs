using System;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Core.Abstractions.Repositories;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void AddUser(User user);
}