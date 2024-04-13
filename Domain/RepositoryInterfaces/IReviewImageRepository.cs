using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IReviewImageRepository
    {
        void Add(ReviewImage image);
        void AddAll(List<ReviewImage> images);
        void Delete(int id);
        void DeleteByAccommodationId(int id);
        void DeleteByReviewId(int id);
        List<ReviewImage> GetAll();
        List<ReviewImage> GetByAccommodationId(int accommodationId);
        ReviewImage GetById(int id);
        List<ReviewImage> GetByReviewId(int id);
        List<ReviewImage> GetImagesByAccommodationId(int id);
        int NextId();
        void RemoveByAccommodationId(int accommodationId);
        void Update(ReviewImage accommodation);
    }
}