using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
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

namespace BookingApp.View.OwnerViews.Components
{
    /// <summary>
    /// Interaction logic for GuestRatingControlPending.xaml
    /// </summary>
    public partial class GuestRatingControlPending : UserControl
    {
        private GuestRating _guestRating;
        private UserRepository _userRepository;
        private AccommodationRepository _accommodationRepository;
        private LocationRepository _locationRepository;
        public EventHandler RefreshPage { get; internal set; }

        public GuestRatingControlPending(GuestRating guestRating, bool isPencilEnabled)
        {
            _guestRating = guestRating;

            _userRepository = UserRepository.GetInstance();
            _accommodationRepository = AccommodationRepository.GetInstance();
            _locationRepository = LocationRepository.GetInstance();

            InitializeComponent();

            if(!isPencilEnabled)
            {
                EyeButton.Visibility = Visibility.Hidden;
            }

            SetupUserControl();
        }

        private void SetupUserControl()
        {
            UserName.Content = _userRepository.GetById(_guestRating.GuestId).Username;
            AccommodationName.Content = _accommodationRepository.GetById(_guestRating.AccommodationId).Name;
            AccommodationLocation.Content = _accommodationRepository.GetById(_guestRating.AccommodationId).Location;
            ReservationTimespan.Content = string.Format("{0} - {1}", _guestRating.Reservation.FirstDateOfStaying.ToShortDateString(), _guestRating.Reservation.LastDateOfStaying.ToShortDateString());
        }

        private void EyeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Accommodation accommodation = _accommodationRepository.GetById(_guestRating.AccommodationId);
            accommodation.Location = _locationRepository.GetById(accommodation.LocationId);
            AddGuestRatingPage addGuestRating = new AddGuestRatingPage(_guestRating);
            addGuestRating.NavigationCompleted += NavigationCompleted;
            NavigationService.GetNavigationService(this).Navigate(addGuestRating);
        }

        private void NavigationCompleted(object? sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            RefreshPage.Invoke(this, EventArgs.Empty);
        }
    }
}