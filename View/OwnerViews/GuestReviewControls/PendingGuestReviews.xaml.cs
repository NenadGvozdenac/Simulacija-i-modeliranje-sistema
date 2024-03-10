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
        private List<GuestRating> _guestRatings;
        private LocationRepository _locationRepository;

        private User _user;
        public PendingGuestReviews(User user, UserRepository userRepository, GuestRatingRepository guestRatingRepository, AccommodationRepository accommodationRepository, LocationRepository locationRepository)
        {
            _user = user;
            _guestRatingRepository = guestRatingRepository;
            _userRepository = userRepository;
            _accommodationRepository = accommodationRepository;
            _locationRepository = locationRepository;

            _guestRatings = new List<GuestRating>();

            InitializeComponent();
            Update();
        }

        private void Update()
        {
            _guestRatings.Clear();

            foreach (Accommodation accommodation in _accommodationRepository.GetAccommodationsByOwnerId(_user.Id))
            {
                _guestRatings.AddRange(_guestRatingRepository.GetGuestRatingsByAccommodationId(accommodation.Id));
            }

            _guestRatings = _guestRatings.Where(g => g.IsChecked == false).ToList();

            AddReviews();
        }

        private void AddReviews()
        {
            Reviews.Children.Clear();
            foreach (GuestRating guestRating in _guestRatings)
            {
                GuestRatingControlPending reviewedGuestReview = new GuestRatingControlPending(guestRating, _userRepository, _accommodationRepository, _guestRatingRepository, _locationRepository);
                reviewedGuestReview.Margin = new Thickness(0, 15, 0, 0);
                Reviews.Children.Add(reviewedGuestReview);
            }
        }
    }
}
