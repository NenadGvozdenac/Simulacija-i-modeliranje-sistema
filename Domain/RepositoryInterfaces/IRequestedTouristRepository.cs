using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRequestedTouristRepository
    {
        void Add(RequestedTourist tourist);
        void Delete(int id);
        List<RequestedTourist> GetAll();
        RequestedTourist GetById(int id);
        RequestedTourist GetByName(string name);
        int NextId();
        void Update(RequestedTourist tourist);
    }
}