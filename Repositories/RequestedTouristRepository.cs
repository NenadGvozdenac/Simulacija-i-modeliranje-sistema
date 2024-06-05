using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.View.PathfinderViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class RequestedTouristRepository : IRequestedTouristRepository
    {
        private const string FilePath = "../../../Resources/Data/requested_tourists.csv";

        private readonly Serializer<RequestedTourist> _serializer;

        private List<RequestedTourist> _tourists;

        public RequestedTouristRepository()
        {
            _serializer = new Serializer<RequestedTourist>();
            _tourists = _serializer.FromCSV(FilePath);
        }

        public List<RequestedTourist> GetAll()
        {
            return _tourists;
        }

        public RequestedTourist GetById(int id)
        {
            return _tourists.FirstOrDefault(a => a.Id == id);
        }

        public RequestedTourist GetByName(string name)
        {
            _tourists = _serializer.FromCSV(FilePath);
            return _tourists.FirstOrDefault(t => t.Name == name);
        }

        public void Add(RequestedTourist tourist)
        {
            _tourists.Add(tourist);
            _serializer.ToCSV(FilePath, _tourists);
        }

        public int NextId()
        {
            _tourists = _serializer.FromCSV(FilePath);
            if (_tourists.Count < 1)
            {
                return 1;
            }
            return _tourists.Max(c => c.Id) + 1;
        }
        public void Update(RequestedTourist tourist)
        {
            var existingTourist = _tourists.FirstOrDefault(t => t.Id == tourist.Id);
            if (existingTourist != null)
            {
                existingTourist.Name = tourist.Name;
                existingTourist.Surname = tourist.Surname;
                existingTourist.Age = tourist.Age;
                existingTourist.RequestId = tourist.RequestId;

                _serializer.ToCSV(FilePath, _tourists);
            }
        }

        public void Delete(int id)
        {
            var existingTourist = _tourists.FirstOrDefault(t => t.Id == id);
            if (existingTourist != null)
            {
                _tourists.Remove(existingTourist);
                _serializer.ToCSV(FilePath, _tourists);
            }
        }
    }
}
