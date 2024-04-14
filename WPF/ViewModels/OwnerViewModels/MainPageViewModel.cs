using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;
using BookingApp.WPF.Views.OwnerViews;
using System.Windows.Navigation;
using BookingApp.View;
using System.Windows.Input;
using BookingApp.Application.Commands;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private List<Accommodation> _accommodations;

    [ObservableProperty]
    private List<AccommodationReservation> _accommodationReservations;

    [ObservableProperty]
    private User _user;

    private MainPage mainPage;

    private AccommodationWrapper _accommodationWrapper;
    private AccommodationReservationWrapper _accommodationReservationWrapper;

    public ICommand NavigateAddAccommodationPage => new NavigateToPageCommand(mainPage, new AddAccommodationPage(User), Refresh);
    public ICommand NavigateGuestReviewPage => new NavigateToPageCommand(mainPage, new GuestReviewPage(User));
    public ICommand NavigateReservationReschedulingPage => new NavigateToPageCommand(mainPage, new ReservationReschedulingPage(User), Refresh);
    public ICommand NavigateSettingsPage => new NavigateToPageCommand(mainPage, new SettingsAndProfile(User));
    public ICommand NavigateGuestFeedbackPage => new NavigateToPageCommand(mainPage, new GuestFeedbackPage(User));

    public MainPageViewModel(MainPage mainPage, User user)
    {
        this.mainPage = mainPage;
        User = user;

        Load();

        AccommodationReviewService.GetInstance().CheckForCancelledReservations();
        AccommodationReservationService.GetInstance().CheckForCancelledReservations();
        OwnerService.GetInstance().CheckForSuperOwner(user);

        _accommodationWrapper = new AccommodationWrapper(this);
        _accommodationReservationWrapper = new AccommodationReservationWrapper(this);

        PrepareFirstPage();
    }

    private void MakeAllButtonsInactive()
    {
        mainPage.ReservationsButton.Style = (Style)mainPage.FindResource("FooterButtonStyle");
        mainPage.AccommodationsButton.Style = (Style)mainPage.FindResource("FooterButtonStyle");
        mainPage.RenovationsButton.Style = (Style)mainPage.FindResource("FooterButtonStyle");
    }

    private void SetActiveButton(Button activeButton)
    {
        MakeAllButtonsInactive();
        activeButton.Style = (Style)mainPage.FindResource("ActiveFooterButtonStyle");
    }

    private void PrepareFirstPage()
    {
        mainPage.MainPanel.Content = _accommodationWrapper;
        SetActiveButton(mainPage.AccommodationsButton);
        Update();
    }

    private void AddAllReservationsThatArentCheckedYet()
    {
        foreach (AccommodationReservation accommodationReservation in GetReservationsOfOwner(User.Id))
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
        return AccommodationService.GetInstance().GetById(guestRating.AccommodationId).OwnerId == User.Id;
    }

    private bool IsDateValid(DateTime lastDateOfStaying, DateTime fiveDaysAgo)
    {
        return lastDateOfStaying >= fiveDaysAgo && lastDateOfStaying <= DateTime.Now;
    }

    private void ActivateVisibilityOfNotification()
    {
        mainPage.MessagePanel.Visibility = Visibility.Visible;
    }

    private void DeactivateVisibilityOfNotification()
    {
        mainPage.MessagePanel.Visibility = Visibility.Collapsed;
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
        AddAllReservationsThatArentCheckedYet();
        DeleteAllGuestRatingsThatDontHaveReservations();
        CheckReservationsThatEnded();
    }

    private void DeleteAllGuestRatingsThatDontHaveReservations()
    {
        GuestRatingService.GetInstance().DeleteAll(GuestRating => ReservationDoesntExist(GuestRating));
    }

    public bool ReservationDoesntExist(GuestRating guestRating)
    {
        return AccommodationReservationService.GetInstance().GetAll().Find(reservation => reservation.Id == guestRating.ReservationId) == null;
    }

    public void Load()
    {
        Accommodations = new(AccommodationService.GetInstance().GetByOwnerId(User.Id));
        AccommodationReservations = new(AccommodationReservationService.GetInstance().GetReservationsByOwnerId(User.Id));
    }

    public void Refresh()
    {
        RefreshReservations();
        RefreshAccommodations();
    }

    private void RefreshAccommodations()
    {
        _accommodationWrapper.WrapperViewModel.Refresh();
        mainPage.MainPanel.Content = _accommodationWrapper;
        SetActiveButton(mainPage.AccommodationsButton);
    }

    private void RefreshReservations()
    {
        _accommodationReservationWrapper.WrapperViewModel.Refresh();
        mainPage.MainPanel.Content = _accommodationWrapper;
        SetActiveButton(mainPage.ReservationsButton);
    }

    public void ToggleNavbar(StackPanel navbar)
    {
        if (navbar.Visibility == Visibility.Collapsed)
        {
            ShowNavbar(navbar, navbar == mainPage.LeftNavbar ? 1 : 0.6);
        } else if (navbar.Visibility == Visibility.Visible)
        {
            HideNavbar(navbar);
        }
    }

    private void ShowNavbar(StackPanel navbar, double v)
    {
        navbar.Visibility = Visibility.Visible;
        mainPage.Navbar.ColumnDefinitions[navbar == mainPage.LeftNavbar ? 0 : 2].Width = new GridLength(v, GridUnitType.Star);
        HideNavbar(navbar == mainPage.LeftNavbar ? mainPage.RightNavbar : mainPage.LeftNavbar);
    }

    private void HideNavbar(StackPanel navbar)
    {
        navbar.Visibility = Visibility.Collapsed;
        mainPage.Navbar.ColumnDefinitions[navbar == mainPage.LeftNavbar ? 0 : 2].Width = new GridLength(0, GridUnitType.Star);
    }

    public void ClickHere()
    {
        GuestReviewPage guestReviewPage = new GuestReviewPage(User);
        DeactivateVisibilityOfNotification();
        NavigationService.GetNavigationService(mainPage).Navigate(guestReviewPage);
    }

    public void Logout()
    {
        SignInForm signInForm = new SignInForm();
        signInForm.Show();
        Window.GetWindow(mainPage).Close();
    }

    public void AccommodationsClicked()
    {
        mainPage.MainPanel.Content = _accommodationWrapper;
        SetActiveButton(mainPage.AccommodationsButton);
    }

    public void ReservationsClicked()
    {
        mainPage.MainPanel.Content = _accommodationReservationWrapper;
        SetActiveButton(mainPage.ReservationsButton);
    }
}
