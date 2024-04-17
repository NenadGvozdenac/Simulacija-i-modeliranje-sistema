using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repositories;

public class ReviewImageRepository : BaseRepository<ReviewImage>, IReviewImageRepository
{
    public ReviewImageRepository() : base("../../../Resources/Data/review_images.csv")
    {
    }

    public List<ReviewImage> GetByAccommodationId(int accommodationId)
    {
        return GetAll().Where(a => a.AccommodationId == accommodationId).ToList();
    }

    public void RemoveByAccommodationId(int accommodationId)
    {
        DeleteRange(GetAll().Where(a => a.AccommodationId == accommodationId).ToList());
    }

    public List<ReviewImage> GetImagesByAccommodationId(int id)
    {
        return GetAll().Where(a => a.AccommodationId == id).ToList();
    }

    public void DeleteByAccommodationId(int id)
    {
        DeleteRange(GetAll().Where(a => a.AccommodationId == id).ToList());
    }

    public void AddAll(List<ReviewImage> images)
    {
        foreach (var image in images)
        {
            Add(image);
        }
    }

    public List<ReviewImage> GetByReviewId(int id)
    {
        return GetAll().Where(a => a.ReviewId == id).ToList();
    }

    public void DeleteByReviewId(int id)
    {
        DeleteRange(GetAll().Where(a => a.ReviewId == id).ToList());
    }
}
