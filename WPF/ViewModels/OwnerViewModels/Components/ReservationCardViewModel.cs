using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.Components;

public class ReservationCardViewModel
{
    public AccommodationReservation AccommodationReservation;

    public AccommodationReservationDTO ReservationDTO { get; set; }

    public ReservationCardViewModel(AccommodationReservation accommodationReservation)
    {
        this.AccommodationReservation = accommodationReservation;
        this.ReservationDTO = new(accommodationReservation);
    }
}
