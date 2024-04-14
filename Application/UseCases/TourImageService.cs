using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourImageService
    {
        private ITourImageRepository _tourImageRepository;


        public TourImageService(ITourImageRepository imageRepository)
        {
            _tourImageRepository = imageRepository;
        }

        public static TourImageService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourImageService>();
        }

        public void Add(TourImage tour)
        {
            _tourImageRepository.Add(tour);
        }

        public void Remove(TourImage tour)
        {
            _tourImageRepository.Remove(tour);
        }

        public List<TourImage> GetAll()
        {
            return _tourImageRepository.GetAll();
        }

        public int NextId()
        {
            return _tourImageRepository.NextId();
        }

        public List<TourImage> GetByTourId(int tourId)
        {
            return _tourImageRepository.GetByTourId(tourId);
        }

        public void Update(TourImage tour)
        {
            _tourImageRepository.Equals(tour);
        }

        public void RemoveByTourId(int tourId)
        {
            _tourImageRepository.RemoveByTourId(tourId);
        }

        public List<TourImage> GetImagesByTourId(int id)
        {
            return _tourImageRepository.GetImagesByTourId(id);
        }

    }
}
