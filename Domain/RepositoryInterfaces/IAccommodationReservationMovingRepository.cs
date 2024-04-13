using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IAccommodationReservationMovingRepository
{
    void Add(AccommodationReservationMoving accommodationReservationMoving);
    void Delete(int id);
    void DeleteAll(Func<AccommodationReservationMoving, bool> value);
    List<AccommodationReservationMoving> GetAll();
    AccommodationReservationMoving GetById(int id);
    List<AccommodationReservationMoving> GetMovingsByAccommodations(List<Accommodation> ownerAccommodations);
    void Update(AccommodationReservationMoving accommodationReservationMoving);
}