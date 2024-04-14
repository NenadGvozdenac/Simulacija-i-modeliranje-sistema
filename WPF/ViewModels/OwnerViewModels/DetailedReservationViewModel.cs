using BookingApp.Domain.Models;
using BookingApp.Resources.Types;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedReservationViewModel
{
    public AccommodationReservationDTO AccommodationReservationDTO { get; set; }

    public DetailedReservationViewModel(AccommodationReservation accommodationReservation)
    {
        AccommodationReservationDTO = new AccommodationReservationDTO(accommodationReservation);
    }
}
