using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.Components;

public class ReservationReschedulingCardViewModel
{
    public AccommodationReservationMoving AccommodationReservationMoving { get; set; }

    public ReservationReschedulingCardViewModel(AccommodationReservationMoving accommodationReservationMoving)
    {
        this.AccommodationReservationMoving = accommodationReservationMoving;
    }

    public void CardClicked(ReservationReschedulingPage reservationReschedulingPage)
    {
        ReservationReschedulingDetailsPage reservationReschedulingDetailsPage = new ReservationReschedulingDetailsPage(AccommodationReservationMoving);
        reservationReschedulingDetailsPage.ReservationReschedulingDetailsPageClosed += (sender, e) => reservationReschedulingPage.UpdateReservations();
        NavigationService.GetNavigationService(reservationReschedulingPage).Navigate(reservationReschedulingDetailsPage);
    }
}
