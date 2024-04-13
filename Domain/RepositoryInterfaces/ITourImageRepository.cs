using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourImageRepository
    {
        void Add(TourImage tour);
        List<TourImage> GetAll();
        List<TourImage> GetByTourId(int tourId);
        List<TourImage> GetImagesByTourId(int id);
        int NextId();
        void Remove(TourImage tour);
        void RemoveByTourId(int tourId);
        void Update(TourImage tour);
    }
}