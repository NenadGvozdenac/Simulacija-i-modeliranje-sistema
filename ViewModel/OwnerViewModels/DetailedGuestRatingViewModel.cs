using BookingApp.DTOs.OwnerDTOs;
using BookingApp.Model.OwnerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.OwnerViewModels;

public class DetailedGuestRatingViewModel
{
    public GuestRatingDTO GuestRatingDTO { get; set; }

    public DetailedGuestRatingViewModel(GuestRating guestRating)
    {
        GuestRatingDTO = new(guestRating);
    }
}
