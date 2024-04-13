using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repositories;

public class AccommodationReservationMovingRepository : BaseRepository<AccommodationReservationMoving>, IAccommodationReservationMovingRepository
{
    public AccommodationReservationMovingRepository() : base("../../../Resources/Data/accommodation_reservation_moving.csv") {}

    public List<AccommodationReservationMoving> GetMovingsByAccommodations(List<Accommodation> ownerAccommodations)
    {
        List<AccommodationReservationMoving> movings = new List<AccommodationReservationMoving>();

        foreach (Accommodation accommodation in ownerAccommodations)
        {
            movings.AddRange(GetAll().Where(moving => moving.AccommodationId == accommodation.Id).ToList());
        }

        return movings;
    }

    public void DeleteAll(Func<AccommodationReservationMoving, bool> value)
    {
        DeleteRange(GetAll().Where(value).ToList());
    }
}
