using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.MutualRepositories
{
    public class LocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }

        public List<Location> GetAll()
        {
            return _locations;
        }

        public Location GetById(int id)
        {
            return _locations.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Location location)
        {
            _locations.Add(location);
            _serializer.ToCSV(FilePath, _locations);
        }

        public void Update(Location location)
        {
            var existingLocation = _locations.FirstOrDefault(a => a.Id == location.Id);
            if (existingLocation != null)
            {
                existingLocation.Id = location.Id;
                existingLocation.City = location.City;
                existingLocation.Country = location.Country;
                _serializer.ToCSV(FilePath, _locations);
            }
        }

        public void Delete(int id)
        {
            var existingAccommodation = _locations.FirstOrDefault(a => a.Id == id);
            if (existingAccommodation != null)
            {
                _locations.Remove(existingAccommodation);
                _serializer.ToCSV(FilePath, _locations);
            }
        }
    }
}
