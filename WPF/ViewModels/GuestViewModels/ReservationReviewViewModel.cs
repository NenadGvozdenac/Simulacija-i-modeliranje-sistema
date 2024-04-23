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
    public List<ReviewImage> _reviewImages;
    public ReservationReview ReservationReview { get; set; }

    public event EventHandler RefreshOwnerFeedback;
    public ReservationReviewViewModel(ReservationReview _ReservationReview, User user, int reservationId)
    {
        ReservationReview = _ReservationReview;
        _user = user;
        reservation = AccommodationReservationService.GetInstance().GetById(reservationId);
        accommodation = AccommodationService.GetInstance().GetById(reservation.AccommodationId);
        SetUpReviewPage(reservationId);
    }

    private void SetUpReviewPage(int reservationId)
    {
        reservation = AccommodationReservationService.GetInstance().GetById(reservationId);
        accommodation = AccommodationService.GetInstance().GetById(reservation.AccommodationId);
        _reviewImages = new List<ReviewImage>();
        ReservationReview.accommodationName_TextBlock.Text = accommodation.Name;
        ReservationReview.dearUsername_TextBlock.Text = "Dear " + _user.Username + ",";
        ReservationReview.username_Label.Content = _user.Username;
    }

    public void GoBack_Click()
    {
        NavigationService.GetNavigationService(ReservationReview).GoBack();
    }

    public void FinishReview_Click()
    {
        AccommodationReview review = new AccommodationReview();
        if (ReservationReview.Level1_CheckBox.IsChecked == true || ReservationReview.Level2_CheckBox.IsChecked == true || ReservationReview.Level3_CheckBox.IsChecked == true || ReservationReview.Level4_CheckBox.IsChecked == true || ReservationReview.Level5_CheckBox.IsChecked == true)
        {
            int LevelOfUrgency = ReservationReview.Level1_CheckBox.IsChecked == true ? 1
                    : ReservationReview.Level2_CheckBox.IsChecked == true ? 2
                    : ReservationReview.Level3_CheckBox.IsChecked == true ? 3
                    : ReservationReview.Level4_CheckBox.IsChecked == true ? 4
                    : ReservationReview.Level5_CheckBox.IsChecked == true ? 5
                    : 0;
            review = new AccommodationReview(_user.Id, reservation.AccommodationId, reservation.Id, (int)ReservationReview.cleanliness_Slider.Value, (int)ReservationReview.ownersCourtesy_Slider.Value, ReservationReview.feedback_TextBox.Text, LevelOfUrgency, ReservationReview.renovation_TextBox.Text, true);
        }
        else
        {
            review = new AccommodationReview(_user.Id, reservation.AccommodationId, reservation.Id, (int)ReservationReview.cleanliness_Slider.Value, (int)ReservationReview.ownersCourtesy_Slider.Value, ReservationReview.feedback_TextBox.Text, false);
        }

        AccommodationReviewService.GetInstance().Add(review);
        RefreshOwnerFeedback?.Invoke(this, EventArgs.Empty);

        foreach (ReviewImage reviewImage in _reviewImages)
        {
            reviewImage.ReviewId = review.Id;
            ImageService.GetInstance().AddReviewImage(reviewImage);
        }
        NavigationService.GetNavigationService(ReservationReview).GoBack();
    }

    public void AddPhoto_Click()
    {
        string imagePath = ImageService.GetInstance().GetImageFromUser("ReviewImages");
        if (imagePath != null)
        {
            Image slika = ImageService.GetInstance().ReadImage(imagePath);
            ReviewImage reviewImage = new ReviewImage(reservation.Id, reservation.AccommodationId, imagePath);

            slika.Width = 185;
            slika.Height = 135;

            _reviewImages.Add(reviewImage);
            ReservationReview.reviewImages_StackPanel.Children.Add(slika);
        }
    }
}

