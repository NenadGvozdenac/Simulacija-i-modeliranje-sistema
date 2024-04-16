using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repositories;
using BookingApp.WPF.Views.GuestViews;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourReservationService
    {

        private ITouristReservationRepository _tourReservationRepository;

        public TourReservationService(ITouristReservationRepository tourReservationRepository)
        {
            _tourReservationRepository = tourReservationRepository;
        }

        public static TourReservationService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourReservationService>();
        }

        public List<TouristReservation> GetAll()
        {
            return _tourReservationRepository.GetAll();
        }

        public TouristReservation GetById(int id)
        {
            return _tourReservationRepository.GetById(id);
        }

        public TouristReservation GetByTouristId(int id)
        {
            return _tourReservationRepository.GetByTouristId(id);
        }

        public List<TouristReservation> GetByTourStartTimeAndId(DateTime tourTime, int TourId)
        {
            return _tourReservationRepository.GetByTourStartTimeAndId(tourTime, TourId);
        }

        public List<TouristReservation> GetByTimeId(int id)
        {
            return _tourReservationRepository.GetByTimeId(id);
        }

        public void Add(TouristReservation reservation)
        {
            _tourReservationRepository.Add(reservation);
        }

        public int NextId()
        {
           return  _tourReservationRepository.NextId();
            
        }
        public void Update(TouristReservation reservation)
        {
            _tourReservationRepository.Update(reservation);
        }

        public void Delete(int id)
        {
            _tourReservationRepository.Delete(id);
        }


    }
}
