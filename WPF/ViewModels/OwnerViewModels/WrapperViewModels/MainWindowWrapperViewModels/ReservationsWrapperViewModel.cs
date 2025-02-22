﻿using BookingApp.Domain.Models;
using BookingApp.Resources.Types;
using BookingApp.WPF.Views.OwnerViews.Components;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;
using BookingApp.WPF.Views.GuestViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.WrapperViewModels.MainWindowWrapperViewModels;

public class ReservationsWrapperViewModel
{
    private AccommodationReservationWrapper accommodationReservationWrapper;
    private MainPageViewModel _mainPageViewModel;

    public ReservationsWrapperViewModel(AccommodationReservationWrapper accommodationReservationWrapper, MainPageViewModel mainPageViewModel)
    {
        this.accommodationReservationWrapper = accommodationReservationWrapper;
        this._mainPageViewModel = mainPageViewModel;

        AddReservations();
    }

    public void Refresh()
    {
        _mainPageViewModel.Load();
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
        accommodationReservationWrapper.Reservations.Children.Clear();
    }

    private void AddOngoingReservations()
    {
        foreach (AccommodationReservation reservation in _mainPageViewModel.AccommodationReservations)
        {
            if (reservation.FirstDateOfStaying <= DateTime.Now && reservation.LastDateOfStaying >= DateTime.Now)
            {
                ReservationControl component = new ReservationControl(accommodationReservationWrapper, reservation);
                component.Margin = new Thickness(15);

                accommodationReservationWrapper.Reservations.Children.Add(component);
            }
        }
    }

    private void AddUpcomingReservations()
    {
        foreach (AccommodationReservation reservation in _mainPageViewModel.AccommodationReservations)
        {
            if (reservation.FirstDateOfStaying > DateTime.Now)
            {
                ReservationControl component = new ReservationControl(accommodationReservationWrapper, reservation);
                component.Margin = new Thickness(15);

                accommodationReservationWrapper.Reservations.Children.Add(component);
            }
        }
    }

    private void AddFinishedReservations()
    {
        foreach (AccommodationReservation reservation in _mainPageViewModel.AccommodationReservations)
        {
            if (reservation.LastDateOfStaying < DateTime.Now)
            {
                ReservationControl component = new ReservationControl(accommodationReservationWrapper, reservation);
                component.Margin = new Thickness(15);

                accommodationReservationWrapper.Reservations.Children.Add(component);
            }
        }
    }
}
