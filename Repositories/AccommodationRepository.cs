using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repositories;

public class AccommodationRepository : BaseRepository<Accommodation>, IAccommodationRepository
{
    public AccommodationRepository() : base("../../../Resources/Data/accommodations.csv") {}

    public List<Accommodation> GetAccommodationsByOwnerId(int id)
    {
        return GetAll().Where(a => a.OwnerId == id).ToList();
    }
}
