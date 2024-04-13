using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReviewRepository
    {
        List<AccommodationReview> _accommodationReviews { get; set; }

        void Add(AccommodationReview accommodationReview);
        void Delete(int id);
        List<AccommodationReview> GetAll();
        double GetAverageRating(int ownerId);
        List<AccommodationReview> GetByAccommodationId(int accommodationId);
        AccommodationReview GetById(int id);
        List<AccommodationReview> GetByOwnerId(int userId);
        List<AccommodationReview> GetReviewsByAccommodations(List<Accommodation> ownerAccommodations);
        void Update(AccommodationReview accommodationReview);
    }
}