using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IAccommodationImageRepository
{
    void Add(AccommodationImage accommodation);
    void AddAll(List<AccommodationImage> images);
    void Delete(int id);
    void DeleteByAccommodationId(int id);
    List<AccommodationImage> GetAll();
    List<AccommodationImage> GetByAccommodationId(int accommodationId);
    AccommodationImage GetById(int id);
    List<AccommodationImage> GetImagesByAccommodationId(int id);
    int NextId();
    void Update(AccommodationImage accommodation);
}