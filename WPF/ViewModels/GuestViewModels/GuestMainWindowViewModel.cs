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
using System.Windows.Shapes;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.View;
using BookingApp.WPF.Views.GuestViews;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class GuestMainWindowViewModel
{
    public EventHandler<int> ReviewClicked;

    private readonly User _user;
    public Accommodations AccommodationsUserControl;
    public MyReservations MyReservationsUserControl;
    public AnywhereAnytime AnywhereAnytimeUserControl { get; set; }
    public Frame GuestWindowFrame;
    public GuestMainWindow GuestMainWindow;

    public GuestMainWindowViewModel(GuestMainWindow _guestMainWindow, User user, Frame _guestWindowFrame)
    {
        _user = user;
        CheckForSuperGuest();
        GuestWindowFrame = _guestWindowFrame;
        GuestMainWindow = _guestMainWindow;
        Update(_user);
        AccommodationsUserControl = new Accommodations(user);
        AccommodationsUserControl.AccommodationsViewModel.AnywhereAnytimeClicked += (sender, e) => AnywhereAnytime_Click();
        AnywhereAnytimeUserControl = new AnywhereAnytime(user);
        GuestWindowFrame.Content = AccommodationsUserControl;
    }

    private void CheckForSuperGuest()
    {
        GuestInfo guestInfo = GuestService.GetInstance().GetByGuestId(_user.Id);
        GuestService.GetInstance().SuperGuestCheck(guestInfo);
    }
    public void Accommodations_Click()
    {
        GuestWindowFrame.Content = AccommodationsUserControl;
    }
    public void AnywhereAnytime_Click()
    {
        GuestWindowFrame.Content = AnywhereAnytimeUserControl;
    }
    public void MyReservations_Click()
    {
        GuestWindowFrame.Content = MyReservationsUserControl;
    }
    public void ShowAccommodationDetails(int accommodationId)
    {
        Accommodation detailedAccommodation = AccommodationService.GetInstance().GetById(accommodationId);
        var a = new AccommodationDetails(detailedAccommodation, _user);
        a.AccommodationDetailsViewModel.UpcomingReservationsChanged += (sender, e) =>
        {
            RefreshReservations();
        };
        GuestWindowFrame.Content = a;

    }

    public void ShowAccommodationDetailsAnywhere(int accommodationId, DateTime firstdate, DateTime lastdate)
    {
        Accommodation detailedAccommodation = AccommodationService.GetInstance().GetById(accommodationId);
        var a = new AccommodationDetails(detailedAccommodation, _user, firstdate, lastdate);
        a.AccommodationDetailsViewModel.UpcomingReservationsChanged += (sender, e) =>
        {
            RefreshReservations();
        };
        GuestWindowFrame.Content = a;

    }

    public void ShowForumPage(int forumid)
    {
        MyReservationsUserControl.MyReservationsViewModel.ForumOpened(forumid);
    }

    private void RefreshReservations()
    {
        MyReservationsUserControl.MyReservationsViewModel.RefreshUpcomingReservations();
    }

    private void Update(User user)
    {
        AccommodationsUserControl = new Accommodations(user);
        MyReservationsUserControl = new MyReservations(user);
        MyReservationsUserControl.MyReservationsViewModel.ReviewClicked += ShowReviewPage;
    }

    private void ShowReviewPage(object sender, int reservationId)
    {
        var a = new ReservationReview(_user, reservationId);
        a.ReservationReviewViewModel.RefreshOwnerFeedback += (sender, e) => MyReservationsUserControl.MyReservationsViewModel.OwnerFeedbackUserControl.OwnerFeedbackViewModel.SetUpOwnerFeedback(_user);
        GuestWindowFrame.Content = a;
    }

    public void SeeMore_Click()
    {
        if (GuestMainWindow.DataContext is Accommodation accommodation)
        {
            int accommodationId = accommodation.Id;

            Window parentWindow = Window.GetWindow(GuestMainWindow);

            if (parentWindow is GuestMainWindow mainWindow)
            {
                mainWindow.GuestViewModel.ShowAccommodationDetails(accommodationId);
            }
        }
    }
    public void Logout_Click()
    {
        SignInForm signIn = new SignInForm();
        signIn.Show();
        GuestMainWindow.Close();
    }
}
