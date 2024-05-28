using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.Application.UseCases
{
    public class ComplexTourRequestService
    {
        private IComplexTourRequestsRepository _tourRequestRepository;

        public ComplexTourRequestService(IComplexTourRequestsRepository tourRequestRepository)
        {
            _tourRequestRepository = tourRequestRepository;
        }

        public static ComplexTourRequestService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<ComplexTourRequestService>();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public ComplexTourRequest GetById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }

        public List<ComplexTourRequest> GetByUserId(int userId)
        {
            return _tourRequestRepository.GetByUserId(userId);
        }

        /// <summary>
        /// Adds a tour to the repository
        /// </summary>
        /// <param name="tour"></param>
        public void Add(ComplexTourRequest tourRequest)
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

        public void Update(ComplexTourRequest tourRequest)
        {
            _tourRequestRepository.Update(tourRequest);
        }

        public void Delete(int id)
        {
            _tourRequestRepository.Delete(id);
        }

    }
}
