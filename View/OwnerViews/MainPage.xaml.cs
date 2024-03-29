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
using BookingApp.View.OwnerViews.MainWindowWrappers;

namespace BookingApp.View.OwnerViews
{
    public partial class MainPage : Page
    {
        private readonly User _user;
        private ObservableCollection<Accommodation> _accommodations;
        private AccommodationRepository _accommodationRepository;
        private LocationRepository _locationRepository;
        private AccommodationImageRepository _accommodationImageRepository;
        private GuestRatingRepository _guestRatingRepository;
        private AccommodationReservationRepository _accommodationReservationRepository;

        private AccommodationWrapper _accommodationWrapper;
        private AccommodationReservationWrapper _accommodationReservationWrapper;

        public MainPage(User user)
        {
            InitializeComponent();
            _user = user;
            _accommodations = new ObservableCollection<Accommodation>();

            _accommodationRepository = AccommodationRepository.GetInstance();
            _locationRepository = LocationRepository.GetInstance();
            _accommodationImageRepository = AccommodationImageRepository.GetInstance();
            _guestRatingRepository = GuestRatingRepository.GetInstance();
            _accommodationReservationRepository = AccommodationReservationRepository.GetInstance();

            Update();

            _accommodationWrapper = new AccommodationWrapper(_user);
            _accommodationReservationWrapper = new AccommodationReservationWrapper(_user);

            PrepareFirstPage();
            CheckForSuperOwner();
        }

        private void CheckForSuperOwner()
        {
            OwnerInfo ownerInfo = OwnerInfoRepository.GetInstance().GetByOwnerId(_user.Id);

            // TODO: Get all reviews of the owner

            // TODO: Recalculate all reviews of the owner

            // TODO: Recalculate number of accommodations of the owner

            // TODO: Get average review score of the owner

            // If user is reviewed by at least 50 guests, and has average rating above 4.5, make him a super owner
        
            if(ownerInfo.NumberOfReviews >= 50 && ownerInfo.AverageReviewScore >= 4.5)
            {
                OwnerInfoRepository.GetInstance().Update(new OwnerInfo()
                {
                    AverageReviewScore = ownerInfo.AverageReviewScore,
                    NumberOfReviews = ownerInfo.NumberOfReviews,
                    NumberOfAccommodations = ownerInfo.NumberOfAccommodations,
                    OwnerId = ownerInfo.OwnerId,
                    IsSuperOwner = true
                });
            }
        }

        private void PrepareFirstPage()
        {
            MainPanel.Content = _accommodationWrapper;
            SetActiveButton(AccommodationsButton);
        }

        private void AddAllReservationsThatArentCheckedYet()
        {
            foreach(AccommodationReservation accommodationReservation in GetReservationsOfOwner(_user.Id))
            {
                if (ReservationNotAlreadyAdded(accommodationReservation, _user.Id))
                {
                    AddGuestRating(accommodationReservation);
                }
            }
        }

        private void AddGuestRating(AccommodationReservation accommodationReservation)
        {
            _guestRatingRepository.Add(new GuestRating()
            {
                ReservationId = accommodationReservation.Id,
                GuestId = accommodationReservation.UserId,
                IsChecked = false,
                AccommodationId = accommodationReservation.AccommodationId,
                Respectfulness = 0,
                Cleanliness = 0,
                Id = _guestRatingRepository.NextId(),
                Comment = ""
            });
        }

        private void CheckReservationsThatEnded()
        {
            foreach (GuestRating guestRating in _guestRatingRepository.GetAll())
            {
                if (IsGuestRatingValid(guestRating))
                {
                    ActivateVisibilityOfNotification();
                }
            }
        }

        private bool IsGuestRatingValid(GuestRating guestRating)
        {
            return IsOwnerValid(guestRating) && IsDateValid(GetLastDateOfStaying(guestRating), DateTime.Now.AddDays(-5)) && !guestRating.IsChecked;
        }

        private DateTime GetLastDateOfStaying(GuestRating guestRating)
        {
            return _accommodationReservationRepository.GetById(guestRating.ReservationId).LastDateOfStaying;
        }

        private bool IsOwnerValid(GuestRating guestRating)
        {
            return _accommodationRepository.GetById(guestRating.AccommodationId).OwnerId == _user.Id;
        }

        private bool IsDateValid(DateTime lastDateOfStaying, DateTime fiveDaysAgo)
        {
            return lastDateOfStaying >= fiveDaysAgo && lastDateOfStaying <= DateTime.Now;
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
            return _guestRatingRepository.GetGuestRatingByReservationId(accommodationReservation.Id) == null;
        }

        public void Update()
        {
            LoadAccommodationInfo();
            HideLeftNavbar();
            HideRightNavbar();
            AddAllReservationsThatArentCheckedYet();
            CheckReservationsThatEnded();
        }

        private void LoadAccommodationInfo()
        {
            foreach (Accommodation accommodation in _accommodationRepository.GetAccommodationsByOwnerId(_user.Id))
            {
                LoadAccommodationImages(accommodation);
                LoadAccommodationLocation(accommodation);
            }
        }

        public void Refresh()
        {
            RefreshReservations();
            RefreshAccommodations();
        }

        private void RefreshAccommodations()
        {
            _accommodationWrapper.Update();
            MainPanel.Content = _accommodationWrapper;
        }

        private void RefreshReservations()
        {
            _accommodationReservationWrapper.Update();
            MainPanel.Content = _accommodationWrapper;
        }

        private void LoadAccommodationLocation(Accommodation accommodation)
        {
            accommodation.Location = _locationRepository.GetById(accommodation.LocationId);
        }

        private void LoadAccommodationImages(Accommodation accommodation)
        {
            accommodation.Images = _accommodationImageRepository.GetImagesByAccommodationId(accommodation.Id);
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
            AddAccommodationPage addAccommodationPage = new AddAccommodationPage(_user);
            addAccommodationPage.PageClosed += (s, e) => Refresh();
            NavigationService.Navigate(addAccommodationPage);
        }

        private void GuestReviewsButtonClick(object sender, RoutedEventArgs e)
        {
            GuestReviewPage guestReviewPage = new GuestReviewPage(_user);
            NavigationService.Navigate(guestReviewPage);
        }

        private void ClickHere_TextBlockClick(object sender, MouseButtonEventArgs e)
        {
            GuestReviewPage guestReviewPage = new GuestReviewPage(_user);
            DeactivateVisibilityOfNotification();
            NavigationService.Navigate(guestReviewPage);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Window.GetWindow(this).Close();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            HideLeftNavbar();
            HideRightNavbar();

            OwnerInfo ownerInfo = OwnerInfoRepository.GetInstance().GetByOwnerId(_user.Id);

            SettingsAndProfile settingsAndProfile = new SettingsAndProfile(_user);
            NavigationService.Navigate(settingsAndProfile);
        }

        private void AccommodationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = _accommodationWrapper;
            SetActiveButton(AccommodationsButton);
        }

        private void RescheduleReservationClick(object sender, RoutedEventArgs e)
        {
            ReservationReschedulingPage reservationSchedulingPage = new ReservationReschedulingPage(_user);
            NavigationService.Navigate(reservationSchedulingPage);
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = _accommodationReservationWrapper;
            SetActiveButton(ReservationsButton);
        }

        private void MakeAllButtonsInactive()
        {
            ReservationsButton.Style = (Style)FindResource("FooterButtonStyle");
            AccommodationsButton.Style = (Style)FindResource("FooterButtonStyle");
            RenovationsButton.Style = (Style)FindResource("FooterButtonStyle");
        }

        private void SetActiveButton(Button activeButton)
        {
            MakeAllButtonsInactive();
            activeButton.Style = (Style)FindResource("ActiveFooterButtonStyle");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
