using BookingApp.Domain.Miscellaneous;
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
    public class TourReviewService
    {
        private ITourReviewRepository _tourReviewRepository;

        public TourReviewService(ITourReviewRepository tourReviewRepository)
        {
            _tourReviewRepository = tourReviewRepository;
        }

        public static TourReviewService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourReviewService>();
        }

        public void Add(TourReview tourReview)
        {
            _tourReviewRepository.Add(tourReview);
        }

        public List<TourReview> GetAll()
        {
            return _tourReviewRepository.GetAll();
        }

        public int NextId()
        {
            return _tourReviewRepository.NextId();
        }

        public void Update(TourReview tourReview)
        {
            _tourReviewRepository.Update(tourReview);
        }

        public void Delete(int id)
        {
            _tourReviewRepository.Delete(id);
        }

        public TourReview GetById(int id)
        {
            return _tourReviewRepository.GetById(id);
        }

        public List<TourReview> GetByTourId(int tourId)
        {
            return _tourReviewRepository.GetByTourId(tourId);
        }
    }
}
