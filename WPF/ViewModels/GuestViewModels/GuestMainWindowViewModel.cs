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
using BookingApp.WPF.Views.GuestViews;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class GuestMainWindowViewModel
{
    public EventHandler<int> ReviewClicked;

    private readonly User _user;
    public Accommodations AccommodationsUserControl;
    public MyReservations MyReservationsUserControl;
    public Frame GuestWindowFrame;
    public GuestMainWindow GuestMainWindow;

    public GuestMainWindowViewModel(GuestMainWindow _guestMainWindow, User user, Frame _guestWindowFrame)
    {
        _user = user;
        GuestWindowFrame = _guestWindowFrame;
        GuestMainWindow = _guestMainWindow;
        Update(_user);
        AccommodationsUserControl = new Accommodations(user);
        GuestWindowFrame.Content = AccommodationsUserControl;
    }
    public void Accommodations_Click()
    {
        GuestWindowFrame.Content = AccommodationsUserControl;
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
        GuestWindowFrame.Content = new ReservationReview(_user, reservationId);
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
        GuestMainWindow.Close();
    }
}
