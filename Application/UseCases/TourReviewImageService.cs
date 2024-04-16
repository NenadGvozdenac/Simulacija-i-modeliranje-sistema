using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model.MutualModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourReviewImageService
    {
        private ITourReviewImageRepository _tourReviewImageRepository;

        public TourReviewImageService(ITourReviewImageRepository tourReviewImageRepository)
        {
            _tourReviewImageRepository = tourReviewImageRepository;
        }

        public static TourReviewImageService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourReviewImageService>();
        }
        public void Add(TourReviewImage image)
        {
            _tourReviewImageRepository.Add(image);
        }

        public List<TourReviewImage> GetAll()
        {
            return _tourReviewImageRepository.GetAll();
        }

        public int NextId()
        {
            return _tourReviewImageRepository.NextId();
        }

        public void Update(TourReviewImage tour)
        {
            _tourReviewImageRepository.Update(tour);
        }

        public List<TourReviewImage> GetByTourId(int tourId)
        {
            return _tourReviewImageRepository.GetByTourId(tourId);
        }

        public void RemoveByTourId(int tourId)
        {
            _tourReviewImageRepository.RemoveByTourId(tourId);
        }

        public List<TourReviewImage> GetImagesByTourId(int tourId)
        {
            return _tourReviewImageRepository.GetImagesByTourId(tourId);
        }

        public void DeleteByTourId(int tourId)
        {
            _tourReviewImageRepository.DeleteByTourId(tourId);
        }

        public void Delete(int id)
        {
            _tourReviewImageRepository.Delete(id);
        }

        public TourReviewImage GetById(int id)
        {
            return _tourReviewImageRepository.GetById(id);
        }

        public List<TourReviewImage> GetByReviewId(int id)
        {
            return _tourReviewImageRepository.GetByReviewId(id);
        }
        public void DeleteByReviewId(int id)
        {
            _tourReviewImageRepository.DeleteByReviewId(id);
        }
    }
}
