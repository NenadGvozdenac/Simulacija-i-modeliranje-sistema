using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.Repository;
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
using BookingApp.View.GuestViews;
using BookingApp.Model.OwnerModels;

namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly User _user;
        private ObservableCollection<Accommodation> _accommodations;
        private AccommodationRepository _accommodationRepository;
        private LocationRepository _locationRepository;
        private AccommodationImageRepository _accommodationImageRepository;
        private GuestRatingRepository _guestRatingRepository;
        private UserRepository _userRepository;
        private AccommodationReservationRepository _accommodationReservationRepository;
        public MainPage(User user)
        {
            InitializeComponent();
            _user = user;
            _accommodations = new ObservableCollection<Accommodation>();
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _accommodationImageRepository = new AccommodationImageRepository();
            _userRepository = new UserRepository();
            _guestRatingRepository = new GuestRatingRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();

            Update();
        }

        private void AddAllReservationsThatArentCheckedYet()
        {
            foreach(AccommodationReservation accommodationReservation in GetReservationsOfOwner(_user.Id))
            {
                if (ReservationNotAlreadyAdded(accommodationReservation, _user.Id))
                {
                    _guestRatingRepository.Add(new GuestRating()
                    {
                        ReservationId = accommodationReservation.Id,
                        GuestId = _user.Id,
                        IsChecked = false,
                        AccommodationId = accommodationReservation.AccommodationId,
                        Respectfulness = 0,
                        Cleanliness = 0,
                        Id = _guestRatingRepository.NextId(),
                        Comment = ""
                    });
                }
            }
        }

        private void CheckReservationsThatEnded()
        {
            foreach(GuestRating guestRating in _guestRatingRepository.GetAll())
            {
                if(_accommodationRepository.GetById(guestRating.AccommodationId).OwnerId == _user.Id)
                {
                    if(_accommodationReservationRepository.GetById(guestRating.ReservationId).LastDateOfStaying < DateTime.Now)
                    {
                        if(guestRating.IsChecked == false)
                        {
                            ActivateVisibilityOfNotification();
                        }
                    }
                }
            }
        }

        private void ActivateVisibilityOfNotification()
        {
            MessagePanel.Visibility = Visibility.Visible;
        }

        private void DeactivateVisibilityOfNotification()
        {
            MessagePanel.Visibility = Visibility.Collapsed;
        }

        private List<AccommodationReservation> GetReservationsOfOwner(int userId)
        {
            return _accommodationReservationRepository.GetReservationsByAccommodations(_accommodationRepository.GetAccommodationsByOwnerId(userId));
        }

        private bool ReservationNotAlreadyAdded(AccommodationReservation accommodationReservation, int userId)
        {
            foreach(GuestRating guestRating in _guestRatingRepository.GetAll())
            {
                if(guestRating.ReservationId == accommodationReservation.Id && guestRating.GuestId == userId)
                {
                    return false;
                }
            }
            return true;
        }

        public void Update()
        {
            _accommodations.Clear();
            Accommodations.Children.Clear();

            foreach (Accommodation accommodation in _accommodationRepository.GetAccommodationsByOwnerId(_user.Id))
            {
                accommodation.Images = _accommodationImageRepository.GetImagesByAccommodationId(accommodation.Id);
                accommodation.Location = _locationRepository.GetById(accommodation.LocationId);

                AccommodationControl accommodationView = new AccommodationControl(accommodation, _locationRepository.GetById(accommodation.LocationId), _accommodationRepository, _accommodationImageRepository);
                accommodationView.EyeButtonClicked += (sender, e) => AccommodationUserControlEyeClick(sender, accommodation);
                accommodationView.TrashButtonClicked += (sender, e) => AccommodationUserControlTrashClick(sender, accommodation);

                accommodationView.Margin = new Thickness(15);
                _accommodations.Add(accommodation);
                Accommodations.Children.Add(accommodationView);
            }

            HideLeftNavbar();
            HideRightNavbar();
            AddAllReservationsThatArentCheckedYet();
            CheckReservationsThatEnded();
        }

        private void ExitApplication(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void HamburgerMenuClick(object sender, MouseButtonEventArgs e)
        {
            if (LeftNavbar.Visibility == Visibility.Collapsed)
            {
                ShowLeftNavbar();
            }
            else
            {
                HideLeftNavbar();
            }
        }

        private void ThreeDotsClick(object sender, MouseButtonEventArgs e)
        {
            if (RightNavbar.Visibility == Visibility.Collapsed)
            {
                ShowRightNavbar();
            }
            else
            {
                HideRightNavbar();
            }
        }

        private void ShowLeftNavbar()
        {
            LeftNavbar.Visibility = Visibility.Visible;
            Navbar.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            RightNavbar.Visibility = Visibility.Collapsed;
            Navbar.ColumnDefinitions[2].Width = new GridLength(0);
        }

        private void HideLeftNavbar()
        {
            LeftNavbar.Visibility = Visibility.Collapsed;
            Navbar.ColumnDefinitions[0].Width = new GridLength(0);
        }

        private void ShowRightNavbar()
        {
            RightNavbar.Visibility = Visibility.Visible;
            Navbar.ColumnDefinitions[2].Width = new GridLength(0.6, GridUnitType.Star);
            LeftNavbar.Visibility = Visibility.Collapsed;
            Navbar.ColumnDefinitions[0].Width = new GridLength(0);
        }

        private void HideRightNavbar()
        {
            RightNavbar.Visibility = Visibility.Collapsed;
            Navbar.ColumnDefinitions[2].Width = new GridLength(0);
        }

        private void AddAccommodationClick(object sender, RoutedEventArgs e)
        {
            AddAccommodationPage addAccommodationPage = new AddAccommodationPage(_user, _accommodationRepository, _accommodationImageRepository);
            addAccommodationPage.PageClosed += (s, e) => Update();
            NavigationService.Navigate(addAccommodationPage);
        }

        private void GuestReviewsButtonClick(object sender, RoutedEventArgs e)
        {
            GuestReviewPage guestReviewPage = new GuestReviewPage(_user, _userRepository, _accommodationRepository, _guestRatingRepository, _locationRepository, _accommodationReservationRepository);
            NavigationService.Navigate(guestReviewPage);
        }

        private void AccommodationUserControlEyeClick(object sender, Accommodation accommodation)
        {
            DetailedAccommodationPage detailedAccommodationPage = new DetailedAccommodationPage(accommodation);
            NavigationService.Navigate(detailedAccommodationPage);
        }

        private void AccommodationUserControlTrashClick(object sender, Accommodation accommodation)
        {
            if (!_accommodationRepository.IsAccommodationDeletable(accommodation.Id))
            {
                MessageBox.Show((OwnerMainWindow)Window.GetWindow(this), "Accommodation cannot be deleted because it has active reservations.", "Delete accommodation", MessageBoxButton.OK);
                return;
            }

            if (MessageBox.Show((OwnerMainWindow)Window.GetWindow(this), "Are you sure you want to delete this accommodation?", "Delete accommodation", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            _accommodationRepository.Delete(accommodation.Id);
            _accommodationImageRepository.DeleteByAccommodationId(accommodation.Id);

            Update();
        }

        private void ClickHere_TextBlockClick(object sender, MouseButtonEventArgs e)
        {
            GuestReviewPage guestReviewPage = new GuestReviewPage(_user, _userRepository, _accommodationRepository, _guestRatingRepository, _locationRepository, _accommodationReservationRepository);
            DeactivateVisibilityOfNotification();
            NavigationService.Navigate(guestReviewPage);
        }
    }
}
