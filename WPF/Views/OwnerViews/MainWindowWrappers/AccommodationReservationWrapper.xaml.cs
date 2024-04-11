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
using BookingApp.WPF.ViewModels.OwnerViewModels.WrapperViewModels.MainWindowWrapperViewModels;

namespace BookingApp.View.OwnerViews.MainWindowWrappers;

public partial class AccommodationReservationWrapper : UserControl
{
    public ReservationsWrapperViewModel WrapperViewModel;

    public AccommodationReservationWrapper(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();

        WrapperViewModel = new ReservationsWrapperViewModel(this, mainPageViewModel);
    }
}
