using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.View.OwnerViews.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.OwnerViews.GuestReviewControls;

public partial class ReviewedGuestReviews : UserControl
{
    private GuestRatingRepository _guestRatingRepository;
    private AccommodationRepository _accommodationRepository;
    private AccommodationReservationRepository _accommodationReservationRepository;
    private ObservableCollection<GuestRating> _guestRatings;

    private User _user;
    public ReviewedGuestReviews(User user)
    {
        _user = user;
        _guestRatingRepository = GuestRatingRepository.GetInstance();
        _accommodationRepository = AccommodationRepository.GetInstance();
        _accommodationReservationRepository = AccommodationReservationRepository.GetInstance();

        _guestRatings = new ObservableCollection<GuestRating>();

        InitializeComponent();
        Update();
    }

    public void Update()
    {
        _guestRatings.Clear();

        foreach (Accommodation accommodation in _accommodationRepository.GetAccommodationsByOwnerId(_user.Id))
        {
            foreach(GuestRating guestRating in _guestRatingRepository.GetGuestRatingsByAccommodationId(accommodation.Id))
            {
                AddGuestRatingIfChecked(guestRating);
            }
        }

        AddReviews();
    }

    private void AddGuestRatingIfChecked(GuestRating guestRating)
    {
        if (guestRating.IsChecked == true)
        {
            guestRating.Reservation = _accommodationReservationRepository.GetById(guestRating.ReservationId);
            _guestRatings.Add(guestRating);
        }
    }

    private void AddReviews()
    {
        Reviews.Children.Clear();
        foreach(GuestRating guestRating in _guestRatings)
        {
            GuestRatingControl reviewedGuestReview = new GuestRatingControl(_user, guestRating);
            reviewedGuestReview.Margin = new Thickness(0, 15, 0, 0);
            Reviews.Children.Add(reviewedGuestReview);
        }
    }
}
