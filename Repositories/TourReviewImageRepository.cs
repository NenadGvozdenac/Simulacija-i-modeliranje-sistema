using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model.MutualModels;


namespace BookingApp.Repositories
{
    public class TourReviewImageRepository : ITourReviewImageRepository
    {
        private const string FilePath = "../../../Resources/Data/tour_review_images.csv";
        private readonly Serializer<TourReviewImage> _serializer;

        private List<TourReviewImage> _reviewImages;

        public TourReviewImageRepository()
        {
            _serializer = new Serializer<TourReviewImage>();
            _reviewImages = _serializer.FromCSV(FilePath);
        }

        public void Add(TourReviewImage image)
        {
            image.Id = NextId();
            _reviewImages.Add(image);
            _serializer.ToCSV(FilePath, _reviewImages);
        }

        public List<TourReviewImage> GetAll()
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

        public void Update(TourReviewImage tour)
        {
            var index = _reviewImages.FindIndex(t => t.Id == tour.Id);
            _reviewImages[index] = tour;
            _serializer.ToCSV(FilePath, _reviewImages);
        }

        public List<TourReviewImage> GetByTourId(int tourId)
        {
            return _reviewImages.Where(t => t.TourId == tourId).ToList();
        }

        public void RemoveByTourId(int tourId)
        {
            _reviewImages.RemoveAll(t => t.TourId == tourId);
            _serializer.ToCSV(FilePath, _reviewImages);
        }

        public List<TourReviewImage> GetImagesByTourId(int tourId)
        {
            return _reviewImages.Where(t => t.TourId == tourId).ToList();
        }

        public void DeleteByTourId(int tourId)
        {
            _reviewImages.RemoveAll(t => t.TourId == tourId);
            _serializer.ToCSV(FilePath, _reviewImages);
        }

        public void Delete(int id)
        {
            _reviewImages.RemoveAll(t => t.Id == id);
            _serializer.ToCSV(FilePath, _reviewImages);
        }

        public TourReviewImage GetById(int id)
        {
            return _reviewImages.FirstOrDefault(t => t.Id == id);
        }

        public List<TourReviewImage> GetByReviewId(int id)
        {
            return _reviewImages.Where(t => t.ReviewId == id).ToList();
        }
        public void DeleteByReviewId(int id)
        {
            _reviewImages.RemoveAll(t => t.ReviewId == id);
            _serializer.ToCSV(FilePath, _reviewImages);
        }
    }
}
