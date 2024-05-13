using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.Views.TouristViews;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.Application.UseCases
{
    public class TourRequestService
    {
        private ITourRequestRepository _tourRequestRepository;

        public TourRequestService(ITourRequestRepository tourRequestRepository)
        {
            _tourRequestRepository = tourRequestRepository;
        }

        public static TourRequestService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourRequestService>();
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public List<TourRequest> GetByYear(int year)
        {
            return _tourRequestRepository.GetByYear(year);
        }
        public TourRequest GetById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }

        public List<TourRequest> GetByUserId(int userId)
        {
            return _tourRequestRepository.GetByUserId(userId);
        }

        /// <summary>
        /// Adds a tour to the repository
        /// </summary>
        /// <param name="tour"></param>
        public void Add(TourRequest tourRequest)
        {
            _tourRequestRepository.Add(tourRequest);
        }

        /// <summary>
        /// Gets the next id for the tour
        /// </summary>
        /// <returns></returns>
        public int NextId()
        {
            return _tourRequestRepository.NextId(); 
        }

        public void Update(TourRequest tourRequest)
        {
            _tourRequestRepository.Update(tourRequest);
        }

        public void Delete(int id)
        {
            _tourRequestRepository.Delete(id);
        }


    }
}
