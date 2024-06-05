using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repositories
{
    public class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tour_requests.csv";

        private readonly Serializer<TourRequest> _serializer;

        private List<TourRequest> _tourRequests;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }
        public List<TourRequest> GetAll()
        {
            return _tourRequests;
        }

        public TourRequest GetById(int id)
        {
            return _tourRequests.FirstOrDefault(a => a.Id == id);
        }

        public List<TourRequest> GetByYear(int year)
        {
            return _tourRequests.Where(a => a.BeginDate.Year == year).ToList();
        }
        public List<TourRequest> GetByUserId(int userId)
        {
            return _tourRequests.Where(a => a.UserId == userId).ToList();
        }

        /// <summary>
        /// Adds a tour to the repository
        /// </summary>
        /// <param name="tour"></param>
        public void Add(TourRequest tourRequest)
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

        public void Update(TourRequest tourRequest)
        {
            var existingTourRequest = _tourRequests.FirstOrDefault(t => t.Id == tourRequest.Id);
            if (existingTourRequest != null)
            {
                existingTourRequest.Tourists = tourRequest.Tourists;
                existingTourRequest.Location = tourRequest.Location;
                existingTourRequest.EndDate = tourRequest.EndDate;
                existingTourRequest.BeginDate = tourRequest.BeginDate;
                existingTourRequest.Description = tourRequest.Description;
                existingTourRequest.Language = tourRequest.Language;
                existingTourRequest.UserId = tourRequest.UserId;
                existingTourRequest.Status = tourRequest.Status;
                existingTourRequest.ComplexRequestId = tourRequest.ComplexRequestId;
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
