using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BookingApp.Model.OwnerModels;
using BookingApp.View.OwnerViews.MainWindowWrappers;
using BookingApp.ViewModel.OwnerViewModels;
using BookingApp.Services.Owner;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.View.OwnerViews
{
    public partial class MainPage : Page
    {
        private AccommodationWrapper _accommodationWrapper;
        private AccommodationReservationWrapper _accommodationReservationWrapper;

        private MainPageViewModel _mainPageViewModel;

        public MainPage(User user)
        {
            InitializeComponent();

            _mainPageViewModel = new MainPageViewModel(user);

            Update();

            _accommodationWrapper = new AccommodationWrapper(_mainPageViewModel);
            _accommodationReservationWrapper = new AccommodationReservationWrapper(_mainPageViewModel);

            PrepareFirstPage();
            CheckForSuperOwner();
        }

        private void CheckForSuperOwner()
        {
            (OwnerInfo, User) ownerInfo = OwnerService.GetInstance().GetOwnerInfo(_mainPageViewModel.User.Id);

            // TODO: Get all reviews of the owner

            // TODO: Recalculate all reviews of the owner

            // TODO: Recalculate number of accommodations of the owner

            ownerInfo.Item1.Accommodations = new AccommodationService().GetByOwnerId(ownerInfo.Item1.OwnerId);

            ownerInfo.Item1.NumberOfAccommodations = ownerInfo.Item1.Accommodations.Count;

            // TODO: Get average review score of the owner

            // If user is reviewed by at least 50 guests, and has average rating above 4.5, make him a super owner
        
            if(ownerInfo.Item1.NumberOfReviews >= 50 && ownerInfo.Item1.AverageReviewScore >= 4.5)
            {
                OwnerService.GetInstance().UpdateOwnerInfo(new OwnerInfo()
                {
                    AverageReviewScore = ownerInfo.Item1.AverageReviewScore,
                    NumberOfReviews = ownerInfo.Item1.NumberOfReviews,
                    NumberOfAccommodations = ownerInfo.Item1.NumberOfAccommodations,
                    OwnerId = ownerInfo.Item1.OwnerId,
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
            foreach(AccommodationReservation accommodationReservation in GetReservationsOfOwner(_mainPageViewModel.User.Id))
            {
                if (ReservationNotAlreadyAdded(accommodationReservation))
                {
                    AddGuestRating(accommodationReservation);
                }
            }
        }

        private void AddGuestRating(AccommodationReservation accommodationReservation)
        {
            GuestRatingService.GetInstance().Add(new GuestRating()
            {
                ReservationId = accommodationReservation.Id,
                GuestId = accommodationReservation.UserId,
                IsChecked = false,
                AccommodationId = accommodationReservation.AccommodationId,
                Respectfulness = 0,
                Cleanliness = 0,
                Comment = ""
            });
        }

        private void CheckReservationsThatEnded()
        {
            foreach (GuestRating guestRating in GuestRatingService.GetInstance().GetAll())
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
            return AccommodationReservationService.GetInstance().GetById(guestRating.ReservationId).LastDateOfStaying;
        }

        private bool IsOwnerValid(GuestRating guestRating)
        {
            return AccommodationService.GetInstance().GetById(guestRating.AccommodationId).OwnerId == _mainPageViewModel.User.Id;
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
            return AccommodationReservationService.GetInstance().GetReservationsByOwnerId(userId);
        }

        private bool ReservationNotAlreadyAdded(AccommodationReservation accommodationReservation)
        {
            return GuestRatingService.GetInstance().GetAll().Find(guestRating => guestRating.ReservationId == accommodationReservation.Id) == null;
        }

        public void Update()
        {
            HideLeftNavbar();
            HideRightNavbar();
            AddAllReservationsThatArentCheckedYet();
            CheckReservationsThatEnded();
        }

        public void Refresh()
        {
            RefreshReservations();
            RefreshAccommodations();
        }

        private void RefreshAccommodations()
        {
            _accommodationWrapper.Refresh();
            MainPanel.Content = _accommodationWrapper;
        }

        private void RefreshReservations()
        {
            _accommodationReservationWrapper.Refresh();
            MainPanel.Content = _accommodationWrapper;
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
            AddAccommodationPage addAccommodationPage = new AddAccommodationPage(_mainPageViewModel.User);
            addAccommodationPage.PageClosed += (s, e) => Refresh();
            NavigationService.Navigate(addAccommodationPage);
        }

        private void GuestReviewsButtonClick(object sender, RoutedEventArgs e)
        {
            GuestReviewPage guestReviewPage = new GuestReviewPage(_mainPageViewModel.User);
            NavigationService.Navigate(guestReviewPage);
        }

        private void ClickHere_TextBlockClick(object sender, MouseButtonEventArgs e)
        {
            GuestReviewPage guestReviewPage = new GuestReviewPage(_mainPageViewModel.User);
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

            SettingsAndProfile settingsAndProfile = new SettingsAndProfile(_mainPageViewModel.User);
            NavigationService.Navigate(settingsAndProfile);
        }

        private void AccommodationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = _accommodationWrapper;
            SetActiveButton(AccommodationsButton);
        }

        private void RescheduleReservationClick(object sender, RoutedEventArgs e)
        {
            ReservationReschedulingPage reservationSchedulingPage = new ReservationReschedulingPage(_mainPageViewModel.User);
            reservationSchedulingPage.ReservationReschedulingPageClosed += (s, e) => Refresh();
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

        private void GuestFeedbackButtonClick(object sender, RoutedEventArgs e)
        {
            GuestFeedbackPage guestFeedbackPage = new GuestFeedbackPage(_mainPageViewModel.User);
            NavigationService.Navigate(guestFeedbackPage);
        }
    }
}
