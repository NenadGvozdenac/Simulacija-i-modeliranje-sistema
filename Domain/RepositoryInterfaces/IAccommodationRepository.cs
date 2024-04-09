using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        void Add(Accommodation accommodation);
        void Delete(int id);
        List<Accommodation> GetAccommodationsByOwnerId(int id);
        List<Accommodation> GetAll();
        Accommodation GetById(int id);
        bool IsAccommodationDeletable(int id);
        int NextId();
        void Update(Accommodation accommodation);
    }
}