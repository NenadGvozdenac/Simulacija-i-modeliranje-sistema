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

namespace BookingApp.View.OwnerViews.GuestReviewControls
{
    public partial class ReviewedGuestReviews : UserControl
    {
        private GuestRatingRepository _guestRatingRepository;
        private UserRepository _userRepository;
        private AccommodationRepository _accommodationRepository;
        private LocationRepository _locationRepository;
        private AccommodationReservationRepository _accommodationReservationRepository;
        private ObservableCollection<GuestRating> _guestRatings;

        private User _user;
        public ReviewedGuestReviews(User user, UserRepository userRepository, GuestRatingRepository guestRatingRepository, AccommodationRepository accommodationRepository, LocationRepository locationRepository, AccommodationReservationRepository accommodationReservationRepository)
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
                    if(guestRating.IsChecked == true)
                    {
                        guestRating.Reservation = _accommodationReservationRepository.GetById(guestRating.ReservationId);
                        _guestRatings.Add(guestRating);
                    }
                }
            }

            foreach (GuestRating guestRating in _guestRatings)
            {
                guestRating.Reservation = _accommodationReservationRepository.GetById(guestRating.ReservationId);
            }

            AddReviews();
        }

        private void AddReviews()
        {
            Reviews.Children.Clear();
            foreach(GuestRating guestRating in _guestRatings)
            {
                GuestRatingControl reviewedGuestReview = new GuestRatingControl(guestRating, _userRepository, _accommodationRepository, _locationRepository);
                reviewedGuestReview.Margin = new Thickness(0, 15, 0, 0);
                Reviews.Children.Add(reviewedGuestReview);
            }
        }
    }
}
