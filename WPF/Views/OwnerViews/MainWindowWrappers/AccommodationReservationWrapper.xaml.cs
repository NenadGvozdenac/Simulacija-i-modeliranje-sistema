using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.View.OwnerViews.Components;
using BookingApp.Resources.Types;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.Domain.Models;

namespace BookingApp.View.OwnerViews.MainWindowWrappers;

public partial class AccommodationReservationWrapper : UserControl
{
    private MainPageViewModel _mainPageViewModel;

    public AccommodationReservationWrapper(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();

        _mainPageViewModel = mainPageViewModel;

        AddReservations();
    }

    public void Refresh()
    {
        _mainPageViewModel.Refresh();
        AddReservations();
    }

    private void AddReservations()
    {
        ClearReservations();
        AddOngoingReservations();
        AddUpcomingReservations();
        AddFinishedReservations();
    }

    private void ClearReservations()
    {
        Reservations.Children.Clear();
    }

    private void AddOngoingReservations()
    {
        foreach(AccommodationReservation reservation in _mainPageViewModel.AccommodationReservations)
        {
            if (reservation.FirstDateOfStaying <= DateTime.Now && reservation.LastDateOfStaying >= DateTime.Now)
            {
                ReservationControl component = new ReservationControl(reservation, ReservationType.Ongoing);
                component.Margin = new Thickness(15);

                component.ReservationSeeMore += (sender, e) => InvokeSeeMore(e);

                Reservations.Children.Add(component);
            }
        }
    }

    private void InvokeSeeMore(AccommodationReservation e)
    {
        DetailedReservationView detailedReservationView = new DetailedReservationView(e);
        NavigationService.GetNavigationService(this).Navigate(detailedReservationView);
    }

    private void AddUpcomingReservations()
    {
        foreach (AccommodationReservation reservation in _mainPageViewModel.AccommodationReservations)
        {
            if (reservation.FirstDateOfStaying > DateTime.Now)
            {
                ReservationControl component = new ReservationControl(reservation, ReservationType.Upcoming);
                component.Margin = new Thickness(15);

                component.ReservationSeeMore += (sender, e) => InvokeSeeMore(e);

                Reservations.Children.Add(component);
            }
        }
    }

    private void AddFinishedReservations()
    {
        foreach (AccommodationReservation reservation in _mainPageViewModel.AccommodationReservations)
        {
            if (reservation.LastDateOfStaying < DateTime.Now)
            {
                ReservationControl component = new ReservationControl(reservation, ReservationType.Finished);
                component.Margin = new Thickness(15);

                component.ReservationSeeMore += (sender, e) => InvokeSeeMore(e);

                Reservations.Children.Add(component);
            }
        }
    }
}
