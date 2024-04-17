using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BookingApp.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository() : base("../../../Resources/Data/users.csv")
    {
    }

    public User GetByUsername(string username)
    {
        return GetAll().FirstOrDefault(u => u.Username == username);
    }
}
