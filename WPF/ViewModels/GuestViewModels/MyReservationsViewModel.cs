﻿using System;
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
    public OwnerFeedback OwnerFeedbackUserControl;
    public SuperGuest SuperGuestUserControl;
    public Forums ForumUserControl;
    public ForumOpened ForumOpenedUserControl;
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
        OwnerFeedbackUserControl = new OwnerFeedback(_user);
        SuperGuestUserControl = new SuperGuest(_user);
        ForumUserControl = new Forums(_user);

        UpcomingReservationsUserControl.UpcomingReservationsViewModel.RescheduleClicked += MyReservation_RescheduleClicked;
        PastReservationsUserControl.PastReservationsViewModel.ReviewClicked += MyReservation_ReviewClicked;
        OwnerFeedbackUserControl.OwnerFeedbackViewModel.ReviewClicked += MyReservation_ReviewClicked;
    }

    public void Update()
    {
        MyReservationsWindow.Username_TextBlock.Text = _user.Username;
        MyReservationsWindow.MyReservationFrame.Content = UpcomingReservationsUserControl;

        if (GuestService.GetInstance().GetByGuestId(_user.Id).IsSuperGuest)
        {
            MyReservationsWindow.crownImage.Visibility = Visibility.Visible;
            MyReservationsWindow.superGuestTextBlock.Text = "super-guest";
            MyReservationsWindow.superGuestTextBlock.Foreground = System.Windows.Media.Brushes.Gold;
        }
        else
        {
            MyReservationsWindow.crownImage.Visibility = Visibility.Hidden;
        }
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

    public void OwnerFeedback_Click()
    {
        MyReservationsWindow.MyReservationFrame.Content = OwnerFeedbackUserControl; 
    }

    public void WhatIsSuperGuest_Click()
    {
        MyReservationsWindow.MyReservationFrame.Content = SuperGuestUserControl;
    }

    public void Forums_Click()
    {
        MyReservationsWindow.MyReservationFrame.Content = ForumUserControl;
    }

    public void MyReservation_RescheduleClicked(object sender, int reservationId)
    {
        AccommodationReservation reservation = AccommodationReservationService.GetInstance().GetById(reservationId);
        var a = new RescheduleAccommodation(reservation);
        a.RescheduleAccommodationViewModel.ChangedMind += (sender, e) => RescheduleAccommodationChangedMind();
        a.RescheduleAccommodationViewModel.SendRequestRefresh += (sender, e) => RefreshRecheduleRequests();
        MyReservationsWindow.MyReservationFrame.Content = a;
    }

    public void ForumOpened(int forumid)
    {
        ForumOpenedUserControl = new ForumOpened(forumid, _user);
        MyReservationsWindow.MyReservationFrame.Content = ForumOpenedUserControl;
    }

    public void MyReservation_ReviewClicked(object sender, int reservationId)
    {
        ReviewClicked?.Invoke(sender, reservationId);
    }
}
