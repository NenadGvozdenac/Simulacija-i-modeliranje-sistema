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
using System.Windows.Media.Animation;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Linq;
using BookingApp.WPF.Views.OwnerViews.AnimatorHelpers;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private List<Accommodation> _accommodations;

    [ObservableProperty]
    private List<AccommodationReservation> _accommodationReservations;

    [ObservableProperty]
    private List<AccommodationRenovation> _accommodationRenovations;

    [ObservableProperty]
    private User _user;

    private MainPage mainPage;

    private AccommodationWrapper _accommodationWrapper;
    private AccommodationReservationWrapper _accommodationReservationWrapper;
    private AccommodationRenovationWrapper _accommodationRenovationWrapper;

    public ICommand NavigateAddAccommodationPage => new NavigateToPageCommand(mainPage, new AddAccommodationPage(User));
    public ICommand NavigateGuestReviewPage => new NavigateToPageCommand(mainPage, new GuestReviewPage(User));
    public ICommand NavigateReservationReschedulingPage => new NavigateToPageCommand(mainPage, new ReservationReschedulingPage(User));
    public ICommand NavigateSettingsPage => new NavigateToPageCommand(mainPage, new SettingsAndProfile(User));
    public ICommand NavigateGuestFeedbackPage => new NavigateToPageCommand(mainPage, new GuestFeedbackPage(User));
    public ICommand NavigateScheduleRenovationPage => new NavigateToPageCommand(mainPage, new ScheduleRenovationPage(User));
    public ICommand NavigateStatisticsPage => new NavigateToPageCommand(mainPage, new AccommodationStatisticsPage(User));
    public ICommand NavigateSuggestionsPage => new NavigateToPageCommand(mainPage, new SuggestionsPage(User));
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
        _accommodationRenovationWrapper = new AccommodationRenovationWrapper(this);

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
        mainPage.MainPanel.Children.Add(_accommodationReservationWrapper);
        _accommodationReservationWrapper.Visibility = Visibility.Collapsed;

        mainPage.MainPanel.Children.Add(_accommodationWrapper);
        _accommodationWrapper.Visibility = Visibility.Visible;
        
        mainPage.MainPanel.Children.Add(_accommodationRenovationWrapper);
        _accommodationRenovationWrapper.Visibility = Visibility.Collapsed;

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
        AccommodationRenovations = new(AccommodationRenovationService.GetInstance().GetRenovationsByOwnerId(User.Id));
    }

    public void Refresh()
    {
        RefreshReservations();
        RefreshRenovations();
        RefreshAccommodations();
        HideNavbar(mainPage.LeftNavbar);
        HideNavbar(mainPage.RightNavbar);
        AnimateIn(_accommodationWrapper);
    }

    private void RefreshRenovations()
    {
        _accommodationRenovationWrapper.WrapperViewModel.Refresh();
        SetActiveButton(mainPage.RenovationsButton);
    }

    private void RefreshAccommodations()
    {
        _accommodationWrapper.WrapperViewModel.Refresh();
        SetActiveButton(mainPage.AccommodationsButton);
    }

    private void RefreshReservations()
    {
        _accommodationReservationWrapper.WrapperViewModel.Refresh();
        SetActiveButton(mainPage.ReservationsButton);
    }

    public void ToggleNavbar(StackPanel navbar)
    {
        if (navbar.Visibility == Visibility.Collapsed)
        {
            ShowNavbar(navbar, navbar == mainPage.LeftNavbar ? 1 : 0.6);
        }
        else if (navbar.Visibility == Visibility.Visible)
        {
            HideNavbar(navbar);
        }
    }

    private void ShowNavbar(StackPanel navbar, double v)
    {
        navbar.Visibility = Visibility.Visible;
        ColumnDefinition column = mainPage.Navbar.ColumnDefinitions[navbar == mainPage.LeftNavbar ? 0 : 2];
        GridLengthAnimation animation = new GridLengthAnimation
        {
            From = new GridLength(0),
            To = new GridLength(v, GridUnitType.Star),
            Duration = new Duration(TimeSpan.FromSeconds(0.3)) // Adjust duration as needed
        };
        column.BeginAnimation(ColumnDefinition.WidthProperty, animation);
        HideNavbar(navbar == mainPage.LeftNavbar ? mainPage.RightNavbar : mainPage.LeftNavbar);
    }

    private void HideNavbar(StackPanel navbar)
    {
        ColumnDefinition column = mainPage.Navbar.ColumnDefinitions[navbar == mainPage.LeftNavbar ? 0 : 2];
        GridLengthAnimation animation = new GridLengthAnimation
        {
            From = column.Width,
            To = new GridLength(0),
            Duration = new Duration(TimeSpan.FromSeconds(0.3)) // Adjust duration as needed
        };
        animation.Completed += (sender, e) => navbar.Visibility = Visibility.Collapsed;
        column.BeginAnimation(ColumnDefinition.WidthProperty, animation);
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
        _ = AnimateIn(_accommodationWrapper);
        SetActiveButton(mainPage.AccommodationsButton);
    }

    public void ReservationsClicked()
    {
        _ = AnimateIn(_accommodationReservationWrapper);
        SetActiveButton(mainPage.ReservationsButton);
    }

    public void RenovationsClicked()
    {
        _ = AnimateIn(_accommodationRenovationWrapper);
        SetActiveButton(mainPage.RenovationsButton);
    }

    private async Task AnimateIn(UserControl wrapper)
    {
        List<UserControl> controls = mainPage.MainPanel.Children.OfType<UserControl>().ToList();
        SlideHelper slideHelper = new SlideHelper(controls, mainPage.MainPanel);

        await slideHelper.AnimateIn(wrapper);
    }
}
