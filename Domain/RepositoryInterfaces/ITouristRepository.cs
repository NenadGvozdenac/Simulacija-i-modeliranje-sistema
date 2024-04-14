using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITouristRepository
    {
        void Add(Tourist tourist);
        void Delete(int id);
        List<Tourist> GetAll();
        Tourist GetById(int id);
        Tourist GetByName(string name);
        int NextId();
        void Update(Tourist tourist);
    }
}