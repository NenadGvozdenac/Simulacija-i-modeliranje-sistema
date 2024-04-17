using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IAccommodationRenovationRepository
{
    void Add(AccommodationRenovation accommodationRenovation);
    void Delete(int id);
    void Update(AccommodationRenovation accommodationRenovation);
    List<AccommodationRenovation> GetAll();
    AccommodationRenovation GetById(int id);
    List<AccommodationRenovation> GetByAccommodationId(int accommodationId);
}