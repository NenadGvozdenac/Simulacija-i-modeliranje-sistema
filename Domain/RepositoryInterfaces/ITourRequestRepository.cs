using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRequestRepository
    {
        void Add(TourRequest tourRequest);
        void Delete(int id);
        List<TourRequest> GetAll();
        TourRequest GetById(int id);
        List<TourRequest> GetByUserId(int userId);
        int NextId();
        void Update(TourRequest tourRequest);
    }
}