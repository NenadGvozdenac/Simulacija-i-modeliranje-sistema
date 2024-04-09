using BookingApp.Domain.Models;
using BookingApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class AccommodationReviewService
{
    private AccommodationReviewRepository _accommodationReviewRepository;
    private AccommodationService _accommodationService;
    private UserRepository _userRepository;
    private AccommodationReservationRepository _accommodationReservationRepository;
    private ReviewImageRepository _reviewImageRepository;
    public AccommodationReviewService()
    {
        _accommodationReviewRepository = AccommodationReviewRepository.GetInstance();
        _accommodationService = AccommodationService.GetInstance();
        _userRepository = UserRepository.GetInstance();
        _accommodationReservationRepository = AccommodationReservationRepository.GetInstance();
        _reviewImageRepository = ReviewImageRepository.GetInstance();
    }

    public static AccommodationReviewService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<AccommodationReviewService>();
    }

    public void Add(AccommodationReview entity)
    {
        _accommodationReviewRepository.Add(entity);
    }

    public void Delete(AccommodationReview entity)
    {
        _accommodationReviewRepository.Delete(entity.Id);
        _reviewImageRepository.DeleteByReviewId(entity.Id);
    }

    public void LoadAll()
    {
        List<AccommodationReview> reviews = _accommodationReviewRepository.GetAll();
        foreach (var review in reviews)
        {
            review.Accommodation = _accommodationService.GetById(review.AccommodationId);
            review.Guest = _userRepository.GetById(review.UserId);
            review.Reservation = _accommodationReservationRepository.GetById(review.ReservationId);
            review.ReviewImages = _reviewImageRepository.GetByReviewId(review.Id);
        }
    }

    public List<AccommodationReview> GetAll()
    {
        LoadAll();
        return _accommodationReviewRepository.GetAll();
    }

    public AccommodationReview GetById(int entityId)
    {
        LoadAll();
        return _accommodationReviewRepository.GetById(entityId);
    }

    public void Update(AccommodationReview entity)
    {
        _accommodationReviewRepository.Update(entity);
    }

    public List<AccommodationReview> GetByAccommodationId(int accommodationId)
    {
        LoadAll();
        return _accommodationReviewRepository.GetByAccommodationId(accommodationId);
    }

    public List<AccommodationReview> GetByOwnerId(int userId)
    {
        LoadAll();
        return _accommodationReviewRepository.GetByOwnerId(userId);
    }

    public double GetAverageReviewScore(int ownerId)
    {
        LoadAll();
        List<AccommodationReview> reviews = GetByOwnerId(ownerId);
        double sum = 0;
        foreach (var review in reviews)
        {
            sum += (review.Cleanliness + review.OwnersCourtesy) / (double)2;
        }
        return sum / reviews.Count is double.NaN ? 0 : sum / reviews.Count;
    }

    public void CheckForCancelledReservations()
    {
        List<AccommodationReview> reviews = GetAll();
        foreach (var review in reviews)
        {
            if (review.Reservation == null)
            {
                Delete(review);
            }
        }
    }
}
