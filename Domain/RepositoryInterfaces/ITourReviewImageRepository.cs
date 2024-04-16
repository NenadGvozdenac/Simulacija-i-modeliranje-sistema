using BookingApp.Model.MutualModels;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReviewImageRepository
    {
        void Add(TourReviewImage image);
        void Delete(int id);
        void DeleteByReviewId(int id);
        void DeleteByTourId(int tourId);
        List<TourReviewImage> GetAll();
        TourReviewImage GetById(int id);
        List<TourReviewImage> GetByReviewId(int id);
        List<TourReviewImage> GetByTourId(int tourId);
        List<TourReviewImage> GetImagesByTourId(int tourId);
        int NextId();
        void RemoveByTourId(int tourId);
        void Update(TourReviewImage tour);
    }
}