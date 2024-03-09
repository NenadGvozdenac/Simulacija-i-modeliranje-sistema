using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository.MutualRepositories
{
    public class AccommodationImageRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation_images.csv";

        private readonly Serializer<AccommodationImage> _serializer;

        private List<AccommodationImage> _accommodationImages;

        public AccommodationImageRepository()
        {
            _serializer = new Serializer<AccommodationImage>();
            _accommodationImages = _serializer.FromCSV(FilePath);
        }

        public void Add(AccommodationImage accommodation)
        {
            _accommodationImages.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodationImages);
        }

        public void Remove(AccommodationImage accommodation)
        {
            _accommodationImages.Remove(accommodation);
            _serializer.ToCSV(FilePath, _accommodationImages);
        }

        public List<AccommodationImage> GetAll()
        {
            return _accommodationImages;
        }

        public int NextId()
        {
            _accommodationImages = _serializer.FromCSV(FilePath);
            if (_accommodationImages.Count < 1)
            {
                return 1;
            }
            return _accommodationImages.Max(c => c.Id) + 1;
        }

        public List<AccommodationImage> GetByAccommodationId(int accommodationId)
        {
            return _accommodationImages.Where(a => a.AccommodationId == accommodationId).ToList();
        }

        public void Update(AccommodationImage accommodation)
        {
            var index = _accommodationImages.FindIndex(a => a.Id == accommodation.Id);
            _accommodationImages[index] = accommodation;
            _serializer.ToCSV(FilePath, _accommodationImages);
        }

        public void RemoveByAccommodationId(int accommodationId)
        {
            _accommodationImages.RemoveAll(a => a.AccommodationId == accommodationId);
            _serializer.ToCSV(FilePath, _accommodationImages);
        }

        public List<AccommodationImage> GetImagesByAccommodationId(int id)
        {
            return _accommodationImages.Where(a => a.AccommodationId == id).ToList();
        }

        public void DeleteByAccommodationId(int id)
        {
            _accommodationImages.RemoveAll(a => a.AccommodationId == id);
            _serializer.ToCSV(FilePath, _accommodationImages);
        }
    }
}
