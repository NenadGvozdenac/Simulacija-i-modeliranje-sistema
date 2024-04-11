using BookingApp.Domain.Models;
using BookingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;
using BookingApp.WPF.Views.GuestViews;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class ReservationReviewViewModel
{
    public User _user;
    public AccommodationReservation reservation;
    public Accommodation accommodation;
    public AccommodationReservationRepository _accommodationReservationRepository;
    public AccommodationRepository _accommodationRepository;
    public AccommodationReviewRepository _accommodationReviewRepository;
    public ReviewImageRepository _reviewImageRepository;
    public List<ReviewImage> _reviewImages;
    public ReservationReview ReservationReview { get; set; }

    public ReservationReviewViewModel(ReservationReview _ReservationReview, User user, AccommodationReservationRepository accommodationReservationRepository, AccommodationRepository accommodationRepository, AccommodationReviewRepository accommodationReviewRepository, ReviewImageRepository reviewImageRepository, int reservationId)
    {
        ReservationReview = _ReservationReview;
        _user = user;
        _accommodationReservationRepository = accommodationReservationRepository;
        _accommodationRepository = accommodationRepository;
        _accommodationReviewRepository = accommodationReviewRepository;
        _reviewImageRepository = reviewImageRepository;
        reservation = _accommodationReservationRepository.GetById(reservationId);
        accommodation = _accommodationRepository.GetById(reservation.AccommodationId);
        SetUpReviewPage(reservationId);
    }

    private void SetUpReviewPage(int reservationId)
    {
        reservation = _accommodationReservationRepository.GetById(reservationId);
        accommodation = _accommodationRepository.GetById(reservation.AccommodationId);
        _reviewImages = new List<ReviewImage>();
        ReservationReview.accommodationName_TextBlock.Text = accommodation.Name;
        ReservationReview.dearUsername_TextBlock.Text = "Dear " + _user.Username + ",";
        ReservationReview.username_TextBlock.Text = _user.Username;
    }

    public void GoBack_Click()
    {
        NavigationService.GetNavigationService(ReservationReview).GoBack();
    }

    public void FinishReview_Click()
    {
        AccommodationReview review = new AccommodationReview(_user.Id, reservation.AccommodationId, reservation.Id, (int)ReservationReview.cleanliness_Slider.Value, (int)ReservationReview.ownersCourtesy_Slider.Value, ReservationReview.feedback_TextBox.Text, false); // TODO: Dodao sam false kao polje za requires renovation. Promeni kad budes dodavao
        _accommodationReviewRepository.Add(review);

        foreach (ReviewImage reviewImage in _reviewImages)
        {
            reviewImage.ReviewId = review.Id;
            _reviewImageRepository.Add(reviewImage);
        }
        NavigationService.GetNavigationService(ReservationReview).GoBack();
    }

    public void AddPhoto_Click()
    {
        string imagePath = ImageService.GetInstance().GetImageFromUser("ReviewImages");
        Image slika = ImageService.GetInstance().ReadImage(imagePath);
        ReviewImage reviewImage = new ReviewImage(reservation.Id, reservation.AccommodationId, imagePath);

        slika.Width = 185;
        slika.Height = 135;

        _reviewImages.Add(reviewImage);
        ReservationReview.reviewImages_StackPanel.Children.Add(slika);
    }
}

