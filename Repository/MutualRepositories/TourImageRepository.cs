using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.MutualRepositories
{
    public class TourImageRepository
    {

        private const string FilePath = "../../../Resources/Data/tour_images.csv";

        private readonly Serializer<TourImage> _serializer;

        private List<TourImage> _tourImages;

        public TourImageRepository()
        {
            _serializer = new Serializer<TourImage>();
            _tourImages = _serializer.FromCSV(FilePath);
        }

        public void Add(TourImage tour)
        {
            _tourImages.Add(tour);
            _serializer.ToCSV(FilePath, _tourImages);
        }

        public void Remove(TourImage tour)
        {
            _tourImages.Remove(tour);
            _serializer.ToCSV(FilePath, _tourImages);
        }

        public List<TourImage> GetAll()
        {
            return _tourImages;
        }

        public int NextId()
        {
            _tourImages = _serializer.FromCSV(FilePath);
            if (_tourImages.Count < 1)
            {
                return 1;
            }
            return _tourImages.Max(c => c.Id) + 1;
        }

        public List<TourImage> GetByTourId(int tourId)
        {
            return _tourImages.Where(a => a.Id == tourId).ToList();
        }

        public void Update(TourImage tour)
        {
            var index = _tourImages.FindIndex(a => a.Id == tour.Id);
            _tourImages[index] = tour;
            _serializer.ToCSV(FilePath, _tourImages);
        }

        public void RemoveByTourId(int tourId)
        {
            _tourImages.RemoveAll(a => a.Id == tourId);
            _serializer.ToCSV(FilePath, _tourImages);
        }

        public List<TourImage> GetImagesByTourId(int id)
        {
            return _tourImages.Where(a => a.TourId == id).ToList();
        }

    }
}
