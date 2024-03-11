using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.Repository;
using BookingApp.View.OwnerViews.GuestReviewControls;
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

namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for GuestReviewPage.xaml
    /// </summary>
    public partial class GuestReviewPage : Page
    {
        private User _user;
        private UserRepository _userRepository;
        private AccommodationRepository _accommodationRepository;
        private GuestRatingRepository _guestRatingRepository;
        private LocationRepository _locationRepository;
        private AccommodationReservationRepository _accommodationReservationRepository;

        private ReviewedGuestReviews _reviewedGuestReviews;
        private PendingGuestReviews _pendingGuestReviews;
        public GuestReviewPage(User user, UserRepository userRepository, AccommodationRepository accommodationRepository, GuestRatingRepository guestRatingRepository, LocationRepository locationRepository, AccommodationReservationRepository accommodationReservationRepository)
        {
            _user = user;
            _userRepository = userRepository;
            _accommodationReservationRepository = accommodationReservationRepository;
            _accommodationRepository = accommodationRepository;
            _locationRepository = locationRepository;
            _guestRatingRepository = guestRatingRepository;
            InitializeComponent();

            _reviewedGuestReviews = new ReviewedGuestReviews(_user, _userRepository, _guestRatingRepository, _accommodationRepository, _locationRepository, _accommodationReservationRepository);
            _pendingGuestReviews = new PendingGuestReviews(_user, _userRepository, _guestRatingRepository, _accommodationRepository, _locationRepository, _accommodationReservationRepository);

            MainPanel.Content = _reviewedGuestReviews;
        }

        private void BackArrowClick(object sender, MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        public void ThreeDotsClick(object sender, MouseButtonEventArgs e)
        {
            // TODO: Implement three dots click
        }

        private void PendingButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = _pendingGuestReviews;
            PendingButton.IsEnabled = false;
            ReviewedButton.IsEnabled = true;
        }

        private void ReviewedButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = _reviewedGuestReviews;
            PendingButton.IsEnabled = true;
            ReviewedButton.IsEnabled = false;
        }
    }
}
