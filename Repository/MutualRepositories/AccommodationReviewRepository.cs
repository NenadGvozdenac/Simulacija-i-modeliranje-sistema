using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.MutualRepositories
{
    public class AccommodationReviewRepository : IRepository<AccommodationReview>
    {
        private const string FilePath = "../../../Resources/Data/accommodation_review.csv";

        private readonly Serializer<AccommodationReview> _serializer;

        public List<AccommodationReview> _accommodationReviews { get; set; }

        public AccommodationReviewRepository()
        {
            _serializer = new Serializer<AccommodationReview>();
            _accommodationReviews = _serializer.FromCSV(FilePath);
        }

        public static AccommodationReviewRepository GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<AccommodationReviewRepository>();
        }

        public void Add(AccommodationReview accommodationReview)
        {
            accommodationReview.Id = NextId();
            _accommodationReviews.Add(accommodationReview);
            _serializer.ToCSV(FilePath, _accommodationReviews);
        }

        public List<AccommodationReview> GetAll()
        {
            return _accommodationReviews;
        }

        private int NextId()
        {
            _accommodationReviews = _serializer.FromCSV(FilePath);
            if (_accommodationReviews.Count < 1)
            {
                return 1;
            }
            return _accommodationReviews.Max(c => c.Id) + 1;
        }

        public void Update(AccommodationReview accommodationReview)
        {
            AccommodationReview oldAccommodationReseview = _accommodationReviews.FirstOrDefault(accommodation => accommodation.Id == accommodationReview.Id);

            if (oldAccommodationReseview == null)
            {
                return;
            }

            oldAccommodationReseview.AccommodationId = accommodationReview.AccommodationId;
            oldAccommodationReseview.ReservationId = accommodationReview.ReservationId;
            oldAccommodationReseview.UserId = accommodationReview.UserId;
            oldAccommodationReseview.Cleanliness = accommodationReview.Cleanliness;
            oldAccommodationReseview.OwnersCourtesy = accommodationReview.OwnersCourtesy;
            oldAccommodationReseview.Feedback = accommodationReview.Feedback;

            _serializer.ToCSV(FilePath, _accommodationReviews);
        }

        public void Delete(int id)
        {
            AccommodationReview accommodationReview = _accommodationReviews.FirstOrDefault(accommodation => accommodation.Id == id);

            if (accommodationReview == null)
            {
                return;
            }

            _accommodationReviews.Remove(accommodationReview);
            _serializer.ToCSV(FilePath, _accommodationReviews);
        }

        public List<AccommodationReview> GetReviewsByAccommodations(List<Accommodation> ownerAccommodations)
        {
            List<AccommodationReview> reviews = new List<AccommodationReview>();

            foreach (Accommodation accommodation in ownerAccommodations)
            {
                reviews.AddRange(_accommodationReviews.Where(reviews => reviews.AccommodationId == accommodation.Id).ToList());
            }

            return reviews;
        }

        public AccommodationReview GetById(int id)
        {
            return _accommodationReviews.FirstOrDefault(a => a.Id == id);
        }
    }
}
