using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class ComplexTourRequestsRepository : IComplexTourRequestsRepository
    {
        private const string FilePath = "../../../Resources/Data/complex_tour_requests.csv";

        private readonly Serializer<ComplexTourRequest> _serializer;

        private List<ComplexTourRequest> _tourRequests;

        public ComplexTourRequestsRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }
        public List<ComplexTourRequest> GetAll()
        {
            return _tourRequests;
        }

        public ComplexTourRequest GetById(int id)
        {
            return _tourRequests.FirstOrDefault(a => a.Id == id);
        }

        public List<ComplexTourRequest> GetByUserId(int userId)
        {
            return _tourRequests.Where(a => a.UserId == userId).ToList();
        }

        /// <summary>
        /// Adds a tour to the repository
        /// </summary>
        /// <param name="tour"></param>
        public void Add(ComplexTourRequest tourRequest)
        {
            _tourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
        }

        /// <summary>
        /// Gets the next id for the tour
        /// </summary>
        /// <returns></returns>
        public int NextId()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            if (_tourRequests.Count < 1)
            {
                return 1;
            }
            return _tourRequests.Max(c => c.Id) + 1;
        }

        public void Update(ComplexTourRequest tourRequest)
        {
            var existingTourRequest = _tourRequests.FirstOrDefault(t => t.Id == tourRequest.Id);
            if (existingTourRequest != null)
            {
                existingTourRequest.UserId = tourRequest.UserId;
                existingTourRequest.Status = tourRequest.Status;
                existingTourRequest.TourRequestIds = tourRequest.TourRequestIds;
                _serializer.ToCSV(FilePath, _tourRequests);
            }
        }

        public void Delete(int id)
        {
            var existingTourRequest = _tourRequests.FirstOrDefault(t => t.Id == id);
            if (existingTourRequest != null)
            {
                _tourRequests.Remove(existingTourRequest);
                _serializer.ToCSV(FilePath, _tourRequests);
            }
        }
    }
}
