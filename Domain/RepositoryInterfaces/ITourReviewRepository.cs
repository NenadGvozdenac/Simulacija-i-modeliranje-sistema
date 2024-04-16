using BookingApp.Model.MutualModels;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        void Add(TourReview tourReview);
        void Delete(int id);
        List<TourReview> GetAll();
        TourReview GetById(int id);
        List<TourReview> GetByTourId(int tourId);
        void Update(TourReview tourReview);
        public int NextId();
    }
}