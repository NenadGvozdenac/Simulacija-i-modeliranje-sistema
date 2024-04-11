using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class ReservationControl : UserControl
{
    public event EventHandler<AccommodationReservation> ReservationSeeMore;

    private ReservationCardViewModel _reservationCardViewModel;

    public ReservationControl(AccommodationReservation accommodationReservation)
    {
        _reservationCardViewModel = new ReservationCardViewModel(accommodationReservation);
        DataContext = _reservationCardViewModel;
        InitializeComponent();
    }

    private void AccommodationReservationClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ReservationSeeMore?.Invoke(this, _reservationCardViewModel.AccommodationReservation);
    }
}
