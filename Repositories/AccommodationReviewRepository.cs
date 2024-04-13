using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class AccommodationReviewRepository : BaseRepository<AccommodationReview>, IAccommodationReviewRepository
{
    public AccommodationReviewRepository() : base("../../../Resources/Data/accommodation_reviews.csv") { }

    public List<AccommodationReview> GetReviewsByAccommodations(List<Accommodation> ownerAccommodations)
    {
        List<AccommodationReview> reviews = new List<AccommodationReview>();

        foreach (Accommodation accommodation in ownerAccommodations)
        {
            reviews.AddRange(GetAll().Where(reviews => reviews.AccommodationId == accommodation.Id).ToList());
        }

        return reviews;
    }

    public double GetAverageRating(int ownerId)
    {
        List<AccommodationReview> reviews = GetAll().Where(review => review.Accommodation.OwnerId == ownerId).ToList();

        if (reviews.Count == 0)
        {
            return 0;
        }

        double averageRating = reviews.Average(review => review.Cleanliness + review.OwnersCourtesy) / 2;

        return averageRating;
    }

    public List<AccommodationReview> GetByAccommodationId(int accommodationId)
    {
        return GetAll().Where(review => review.AccommodationId == accommodationId).ToList();
    }

    public List<AccommodationReview> GetByOwnerId(int userId)
    {
        return GetAll().Where(review => review.Accommodation.OwnerId == userId).ToList();
    }
}