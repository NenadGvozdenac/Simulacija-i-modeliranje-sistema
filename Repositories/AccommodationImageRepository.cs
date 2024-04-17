using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repositories;

public class AccommodationImageRepository : BaseRepository<AccommodationImage>, IAccommodationImageRepository
{
    public AccommodationImageRepository() : base("../../../Resources/Data/accommodation_images.csv") {}

    public List<AccommodationImage> GetByAccommodationId(int accommodationId)
    {
        return GetAll().Where(a => a.AccommodationId == accommodationId).ToList();
    }

    public List<AccommodationImage> GetImagesByAccommodationId(int id)
    {
        return GetAll().Where(a => a.AccommodationId == id).ToList();
    }

    public void DeleteByAccommodationId(int id)
    {
        DeleteRange(GetAll().Where(a => a.AccommodationId == id).ToList());
    }

    public void AddAll(List<AccommodationImage> images)
    {
        foreach (AccommodationImage image in images)
        {
            image.Id = NextId();
            Add(image);
        }
    }
}
