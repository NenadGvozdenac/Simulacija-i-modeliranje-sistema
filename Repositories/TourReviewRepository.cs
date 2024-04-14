using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Model.MutualModels;


namespace BookingApp.Repositories
{
    public class TourReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/tour_review.csv";

        private readonly Serializer<TourReview> _serializer;

        private List<TourReview> _tourReviews;

        public TourReviewRepository()
        {
            _serializer = new Serializer<TourReview>();
            _tourReviews = _serializer.FromCSV(FilePath);
        }

        public void Add(TourReview tourReview)
        {
            tourReview.Id = NextId();
            _tourReviews.Add(tourReview);
            _serializer.ToCSV(FilePath, _tourReviews);
        }

        public List<TourReview> GetAll()
        {
            return _tourReviews;
        }

        private int NextId()
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            if (_tourReviews.Count < 1)
            {
                return 1;
            }
            return _tourReviews.Max(c => c.Id) + 1;
        }

        public void Update(TourReview tourReview)
        {
            TourReview oldTourReview = _tourReviews.FirstOrDefault(tour => tour.Id == tourReview.Id);

            if (oldTourReview == null)
            {
                return;
            }

            oldTourReview.TourId = tourReview.TourId;
            oldTourReview.ReservationId = tourReview.ReservationId;
            oldTourReview.UserId = tourReview.UserId;
            oldTourReview.GuideKnowledge = tourReview.GuideKnowledge;
            oldTourReview.GuideLanguage = tourReview.GuideLanguage;
            oldTourReview.Feedback = tourReview.Feedback;

            _serializer.ToCSV(FilePath, _tourReviews);
        }

        public void Delete(int id)
        {
            TourReview tourReview = _tourReviews.FirstOrDefault(tour => tour.Id == id);

            if (tourReview == null)
            {
                return;
            }

            _tourReviews.Remove(tourReview);
            _serializer.ToCSV(FilePath, _tourReviews);
        }

        public TourReview GetById(int id)
        {
            return _tourReviews.FirstOrDefault(tour => tour.Id == id);
        }

        public List<TourReview> GetByTourId(int tourId)
        {
            return _tourReviews.Where(review => review.TourId == tourId).ToList();
        }
    }
}
