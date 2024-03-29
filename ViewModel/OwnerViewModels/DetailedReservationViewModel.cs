using BookingApp.DTOs.OwnerDTOs;
using BookingApp.Model.MutualModels;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.OwnerViewModels;

public class DetailedReservationViewModel
{
    public AccommodationReservationDTO AccommodationReservationDTO { get; set; }

    public DetailedReservationViewModel(AccommodationReservation accommodationReservation)
    {
        AccommodationReservationDTO = new AccommodationReservationDTO(accommodationReservation);
    }
}
