using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourStartTimeRepository
    {
        void Add(TourStartTime time);
        void Delete(int id);
        void DeleteByTourId(int id);
        List<TourStartTime> GetAll();
        TourStartTime GetById(int Id);
        List<TourStartTime> GetByTourId(int tourId);
        TourStartTime GetByTourStartTimeAndId(DateTime tourTime, int TourId);
        List<TourStartTime> GetTimeByTourId(int id);
        int NextId();
        void RemoveByTourId(int tourId);
        void RemoveByTourStartTimeAndId(DateTime tourTime, int TourId);
        void Update(TourStartTime time);
    }
}