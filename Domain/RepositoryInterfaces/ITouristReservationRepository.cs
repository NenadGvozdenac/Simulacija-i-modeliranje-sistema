using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITouristReservationRepository
    {
        void Add(TouristReservation reservation);
        void Delete(int id);
        List<TouristReservation> GetAll();
        TouristReservation GetById(int id);
        List<TouristReservation> GetByTimeId(int id);
        TouristReservation GetByTouristId(int id);
        List<TouristReservation> GetByTourStartTimeAndId(DateTime tourTime, int TourId);
        int NextId();
        void Update(TouristReservation reservation);
    }
}