using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.GuestViews;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class MyReservationsViewModel
{
    public EventHandler<int> ReviewClicked;
    public MyReservations MyReservationsWindow { get; set; }

    private User _user;
    public UpcomingReservations UpcomingReservationsUserControl;
    public PastReservations PastReservationsUserControl;
    public RescheduleRequests RescheduleRequestsUserControl;
    public MyReservationsViewModel(MyReservations _myReservationsWindow, User user)
    {
        MyReservationsWindow = _myReservationsWindow;
        _user = user;
        SetUpMyReservations();
        Update();
    }

    public void SetUpMyReservations()
    {
        UpcomingReservationsUserControl = new UpcomingReservations(_user);
        PastReservationsUserControl = new PastReservations(_user);
        RescheduleRequestsUserControl = new RescheduleRequests(_user);
        UpcomingReservationsUserControl.UpcomingReservationsViewModel.RescheduleClicked += MyReservation_RescheduleClicked;
        PastReservationsUserControl.PastReservationsViewModel.ReviewClicked += MyReservation_ReviewClicked;
    }

    public void Update()
    {
        MyReservationsWindow.Username_TextBlock.Text = _user.Username;
        MyReservationsWindow.MyReservationFrame.Content = UpcomingReservationsUserControl;
    }

    private void RescheduleAccommodationChangedMind()
    {
        MyReservationsWindow.MyReservationFrame.Content = UpcomingReservationsUserControl;
    }

    public void RefreshUpcomingReservations()
    {
        UpcomingReservationsUserControl.UpcomingReservationsViewModel.Update();
    }

    public void RefreshRecheduleRequests()
    {
        RescheduleRequestsUserControl.RescheduleRequestsViewModel.Update();
    }

    public void UpcomingReservations_Click()
    {
        MyReservationsWindow.MyReservationFrame.Content = UpcomingReservationsUserControl;
    }

    public void PastReservations_Click()
    {
        MyReservationsWindow.MyReservationFrame.Content = PastReservationsUserControl;
    }
    public void RescheduleRequests_Click()
    {
        MyReservationsWindow.MyReservationFrame.Content = RescheduleRequestsUserControl;
    }
    public void MyReservation_RescheduleClicked(object sender, int reservationId)
    {
        AccommodationReservation reservation = AccommodationReservationService.GetInstance().GetById(reservationId);
        var a = new RescheduleAccommodation(reservation);
        a.RescheduleAccommodationViewModel.ChangedMind += (sender, e) => RescheduleAccommodationChangedMind();
        a.RescheduleAccommodationViewModel.SendRequestRefresh += (sender, e) => RefreshRecheduleRequests();
        MyReservationsWindow.MyReservationFrame.Content = a;
    }

    public void MyReservation_ReviewClicked(object sender, int reservationId)
    {
        ReviewClicked?.Invoke(sender, reservationId);
    }
}
