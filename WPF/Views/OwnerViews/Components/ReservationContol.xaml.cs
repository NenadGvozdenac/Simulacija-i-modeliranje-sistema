using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using BookingApp.WPF.Views.OwnerViews.AnimatorHelpers;
using BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class ReservationControl : UserControl
{
    private readonly AccommodationReservationWrapper accommodationReservationWrapper;
    private ReservationCardViewModel _reservationCardViewModel;

    public ReservationControl(AccommodationReservationWrapper accommodationReservationWrapper, AccommodationReservation accommodationReservation)
    {
        _reservationCardViewModel = new ReservationCardViewModel(accommodationReservation);
        DataContext = _reservationCardViewModel;
        InitializeComponent();
        this.accommodationReservationWrapper = accommodationReservationWrapper;
        HoverAnimation hoverAnimation = new HoverAnimation();
        hoverAnimation.AnimateHover(this.Border);
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _reservationCardViewModel.NavigateToReservationDetails(accommodationReservationWrapper);
    }
}
