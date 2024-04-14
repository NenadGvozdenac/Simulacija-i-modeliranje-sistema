using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourStartTimeService
    {
        private ITourStartTimeRepository _tourStartTimeRepository;


        public TourStartTimeService(ITourStartTimeRepository timeRepository)
        {
            _tourStartTimeRepository = timeRepository;
        }

        public static TourStartTimeService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourStartTimeService>();
        }

        public void Add(TourStartTime time)
        {
            _tourStartTimeRepository.Add(time);
        }

        public void Update(TourStartTime time)
        {
            _tourStartTimeRepository.Update(time);
        }



        public void Delete(int id)
        {
            _tourStartTimeRepository?.Delete(id);   
        }

        public List<TourStartTime> GetAll()
        {
           return _tourStartTimeRepository.GetAll();
        }


        public int NextId()
        {
            return _tourStartTimeRepository.NextId();
        }

        public List<TourStartTime> GetByTourId(int tourId)
        {
            return _tourStartTimeRepository.GetByTourId(tourId);
        }

        public TourStartTime GetById(int Id)
        {
            return _tourStartTimeRepository.GetById(Id);
        }


        public TourStartTime GetByTourStartTimeAndId(DateTime tourTime, int TourId)
        {
            return _tourStartTimeRepository.GetByTourStartTimeAndId(tourTime, TourId);
        }

        public void RemoveByTourStartTimeAndId(DateTime tourTime, int TourId)
        {
            _tourStartTimeRepository.RemoveByTourStartTimeAndId(tourTime, TourId);
        }

        public void RemoveByTourId(int tourId)
        {
            _tourStartTimeRepository.RemoveByTourId(tourId);
        }

        public List<TourStartTime> GetTimeByTourId(int id)
        {
            return _tourStartTimeRepository.GetByTourId(id);
        }

        public void DeleteByTourId(int id)
        {
            _tourStartTimeRepository.DeleteByTourId(id) ;
        }
    }
}
