using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using BookingApp.WPF.Views.OwnerViews.AnimatorHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class ReservationRescheduling : UserControl
{
    private ReservationReschedulingCardViewModel _reservationReschedulingCardViewModel;
    private ReservationReschedulingPage _reservationReschedulingPage;
    public ReservationRescheduling(ReservationReschedulingPage reservationReschedulingPage, AccommodationReservationMoving accommodationReservationMoving)
    {
        InitializeComponent();
        _reservationReschedulingPage = reservationReschedulingPage;
        _reservationReschedulingCardViewModel = new ReservationReschedulingCardViewModel(accommodationReservationMoving);
        DataContext = _reservationReschedulingCardViewModel;
        HoverAnimation hoverAnimation = new HoverAnimation();
        hoverAnimation.AnimateHover(this.Border);
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _reservationReschedulingCardViewModel.CardClicked(_reservationReschedulingPage);
    }
}
