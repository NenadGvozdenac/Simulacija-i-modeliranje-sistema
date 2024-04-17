using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class AccommodationRenovationRepository : BaseRepository<AccommodationRenovation>, IAccommodationRenovationRepository
{
    public AccommodationRenovationRepository() : base("../../../Resources/Data/accommodation_renovations.csv") { }

    public List<AccommodationRenovation> GetByAccommodationId(int accommodationId)
    {
        return GetAll().Where(x => x.AccommodationId == accommodationId).ToList();
    }
}
