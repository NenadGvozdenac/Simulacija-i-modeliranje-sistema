using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.Components;

public class ReservationCardViewModel
{
    public AccommodationReservation AccommodationReservation;

    public AccommodationReservationDTO ReservationDTO { get; set; }

    public ReservationCardViewModel( AccommodationReservation accommodationReservation)
    {
        this.AccommodationReservation = accommodationReservation;
        this.ReservationDTO = new(accommodationReservation);
    }

    public void NavigateToReservationDetails(AccommodationReservationWrapper accommodationReservationWrapper)
    {
        DetailedReservationView detailedReservationView = new(AccommodationReservation);
        NavigationService.GetNavigationService(accommodationReservationWrapper).Navigate(detailedReservationView);
    }
}
