using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationRepository
    {
        void Add(AccommodationReservation accommodationres);
        void CheckDate(AccommodationReservation reservation);
        void Delete(int id);
        List<DateTime> FindTakenDates(int id);
        List<AccommodationReservation> GetAll();
        List<AccommodationReservation> GetByAccommodationId(int accommodationId);
        AccommodationReservation GetById(int id);
        List<AccommodationReservation> GetReservationsByAccommodations(List<Accommodation> accommodations);
        int NextId();
        void Update(AccommodationReservation accommodationres);
    }
}