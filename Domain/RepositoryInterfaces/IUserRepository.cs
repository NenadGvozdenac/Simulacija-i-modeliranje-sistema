using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(int ownerId);
        List<User> GetAll();
        User GetById(int id);
        User GetByUsername(string username);
        int NextId();
        void Update(User user);
    }
}