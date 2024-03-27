using BookingApp.DTOs.OwnerDTOs;
using BookingApp.Model.MutualModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.Owner;

public class DetailedAccommodationViewModel
{
    public AccommodationDTO Accommodation { get; set; }

    public DetailedAccommodationViewModel(Accommodation accommodation)
    {
        Accommodation = new(accommodation);
    }
}
