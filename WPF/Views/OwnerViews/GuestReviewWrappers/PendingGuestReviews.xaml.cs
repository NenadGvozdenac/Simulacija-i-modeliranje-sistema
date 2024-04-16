using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.WPF.Views.OwnerViews.Components;
using System.Collections.ObjectModel;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.Views.OwnerViews.GuestReviewControls;

public partial class PendingGuestReviews : UserControl
{
    private ObservableCollection<GuestRating> _guestRatings;
    private User _user;
    public PendingGuestReviews(User user)
    {
        _user = user;

        _guestRatings = new ObservableCollection<GuestRating>();

        InitializeComponent();
        Update();
    }

    public void Update()
    {
        _guestRatings.Clear();

        foreach (Accommodation accommodation in AccommodationService.GetInstance().GetByOwnerId(_user.Id))
        {
            foreach(GuestRating guestRating in GuestRatingService.GetInstance().GetByAccommodationId(accommodation.Id))
            {
                AddGuestReviewIfSatisfiesConditions(guestRating);
            }
        }

        AddReviews();
    }

    private void AddGuestReviewIfSatisfiesConditions(GuestRating guestRating)
    {
        if (SatisfiedConditions(guestRating, AccommodationReservationService.GetInstance().GetById(guestRating.ReservationId)))
        {
            guestRating.Reservation = AccommodationReservationService.GetInstance().GetById(guestRating.ReservationId);
            _guestRatings.Add(guestRating);
        }
    }

    private bool SatisfiedConditions(GuestRating guestRating, AccommodationReservation accommodationReservation)
    {
        return guestRating.IsChecked == false && accommodationReservation.LastDateOfStaying <= DateTime.Now;
    }

    private void AddReviews()
    {
        Reviews.Children.Clear();
        DateTime fiveDaysAgo = DateTime.Now.AddDays(-5);
        foreach (GuestRating guestRating in _guestRatings)
        {
            DateTime lastDateOfStaying = guestRating.Reservation.LastDateOfStaying;
            bool isEnabled = lastDateOfStaying >= fiveDaysAgo && lastDateOfStaying <= DateTime.Now;
            GuestRatingControlPending reviewedGuestReview = new GuestRatingControlPending(guestRating, isEnabled);
            reviewedGuestReview.Margin = new Thickness(0, 15, 0, 0);
            Reviews.Children.Add(reviewedGuestReview);
        }
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        Update();
    }
}
