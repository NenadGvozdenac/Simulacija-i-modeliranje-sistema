using BookingApp.Model.OwnerModels;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.Repository;
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
using BookingApp.Model.MutualModels;
using BookingApp.View.OwnerViews.Components;
using BookingApp.Repository.MutualRepositories;
using System.Collections.ObjectModel;

namespace BookingApp.View.OwnerViews.GuestReviewControls
{
    /// <summary>
    /// Interaction logic for PendingGuestReviews.xaml
    /// </summary>
    public partial class PendingGuestReviews : UserControl
    {
        private GuestRatingRepository _guestRatingRepository;
        private UserRepository _userRepository;
        private AccommodationRepository _accommodationRepository;
        private ObservableCollection<GuestRating> _guestRatings;
        private LocationRepository _locationRepository;
        private AccommodationReservationRepository _accommodationReservationRepository;
        private User _user;
        public PendingGuestReviews(User user, UserRepository userRepository, GuestRatingRepository guestRatingRepository, AccommodationRepository accommodationRepository, LocationRepository locationRepository, AccommodationReservationRepository accommodationReservationRepository)
        {
            _user = user;
            _guestRatingRepository = guestRatingRepository;
            _userRepository = userRepository;
            _accommodationRepository = accommodationRepository;
            _locationRepository = locationRepository;
            _accommodationReservationRepository = accommodationReservationRepository;

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
                    AccommodationReservation accommodationReservation = _accommodationReservationRepository.GetById(guestRating.ReservationId);
                    if(guestRating.IsChecked == false && accommodationReservation.LastDateOfStaying <= DateTime.Now)
                    {
                        guestRating.Reservation = _accommodationReservationRepository.GetById(guestRating.ReservationId);
                        _guestRatings.Add(guestRating);
                    }
                }
            }

            foreach(GuestRating guestRating in _guestRatings)
            {
                guestRating.Reservation = _accommodationReservationRepository.GetById(guestRating.ReservationId);
            }

            AddReviews();
        }

        private void AddReviews()
        {
            Reviews.Children.Clear();
            DateTime fiveDaysAgo = DateTime.Now.AddDays(-5);
            foreach (GuestRating guestRating in _guestRatings)
            {
                DateTime lastDateOfStaying = guestRating.Reservation.LastDateOfStaying;
                bool isEnabled = lastDateOfStaying >= fiveDaysAgo && lastDateOfStaying <= DateTime.Now;
                GuestRatingControlPending reviewedGuestReview = new GuestRatingControlPending(guestRating, _userRepository, _accommodationRepository, _guestRatingRepository, _locationRepository, isEnabled);
                reviewedGuestReview.Margin = new Thickness(0, 15, 0, 0);
                reviewedGuestReview.RefreshPage += (sender, e) => Update();
                Reviews.Children.Add(reviewedGuestReview);
            }
        }
    }
}
