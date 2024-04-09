using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedGuestRatingViewModel
{
    public GuestRatingDTO GuestRatingDTO { get; set; }

    public DetailedGuestRatingViewModel(GuestRating guestRating)
    {
        GuestRatingDTO = new(guestRating);
    }
}
