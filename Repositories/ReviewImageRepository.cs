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

public class ReviewImageRepository : IRepository<ReviewImage>, IReviewImageRepository
{
    private const string FilePath = "../../../Resources/Data/review_images.csv";
    private readonly Serializer<ReviewImage> _serializer;

    private List<ReviewImage> _reviewImages;

    public ReviewImageRepository()
    {
        _serializer = new Serializer<ReviewImage>();
        _reviewImages = _serializer.FromCSV(FilePath);
    }

    public void Add(ReviewImage image)
    {
        image.Id = NextId();
        _reviewImages.Add(image);
        _serializer.ToCSV(FilePath, _reviewImages);
    }

    public List<ReviewImage> GetAll()
    {
        return _reviewImages;
    }

    public int NextId()
    {
        _reviewImages = _serializer.FromCSV(FilePath);
        if (_reviewImages.Count < 1)
        {
            return 1;
        }
        return _reviewImages.Max(c => c.Id) + 1;
    }

    public List<ReviewImage> GetByAccommodationId(int accommodationId)
    {
        return _reviewImages.Where(a => a.AccommodationId == accommodationId).ToList();
    }

    public void Update(ReviewImage accommodation)
    {
        var index = _reviewImages.FindIndex(a => a.Id == accommodation.Id);
        _reviewImages[index] = accommodation;
        _serializer.ToCSV(FilePath, _reviewImages);
    }

    public void RemoveByAccommodationId(int accommodationId)
    {
        _reviewImages.RemoveAll(a => a.AccommodationId == accommodationId);
        _serializer.ToCSV(FilePath, _reviewImages);
    }

    public List<ReviewImage> GetImagesByAccommodationId(int id)
    {
        return _reviewImages.Where(a => a.AccommodationId == id).ToList();
    }

    public void DeleteByAccommodationId(int id)
    {
        _reviewImages.RemoveAll(a => a.AccommodationId == id);
        _serializer.ToCSV(FilePath, _reviewImages);
    }

    public void AddAll(List<ReviewImage> images)
    {
        foreach (ReviewImage image in images)
        {
            image.Id = NextId();
        }

        _reviewImages.AddRange(images);
        _serializer.ToCSV(FilePath, _reviewImages);
    }

    public void Delete(int id)
    {
        _reviewImages.RemoveAll(a => a.Id == id);
        _serializer.ToCSV(FilePath, _reviewImages);
    }

    public ReviewImage GetById(int id)
    {
        return _reviewImages.FirstOrDefault(a => a.Id == id);
    }

    public List<ReviewImage> GetByReviewId(int id)
    {
        return _reviewImages.Where(a => a.ReviewId == id).ToList();
    }

    public void DeleteByReviewId(int id)
    {
        _reviewImages.RemoveAll(a => a.ReviewId == id);
        _serializer.ToCSV(FilePath, _reviewImages);
    }
}
