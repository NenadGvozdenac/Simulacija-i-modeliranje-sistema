using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        void Add(Tour tour);
        void Delete(int id);
        List<Tour> GetAll();
        Tour GetById(int id);
        List<Tour> GetByOwnerId(int ownerId);
        Tour GetByName(string name);
        int NextId();
        void Update(Tour tour);
    }
}