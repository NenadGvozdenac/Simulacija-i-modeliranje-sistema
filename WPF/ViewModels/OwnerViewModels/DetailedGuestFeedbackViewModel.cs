using BookingApp.Domain.Models;
using BookingApp.Domain.Miscellaneous;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedGuestFeedbackViewModel
{
    public GuestFeedbackDTO GuestFeedbackDTO { get; set; }
    public AccommodationReview AccommodationReview { get; set; }
    public DetailedGuestFeedbackViewModel(AccommodationReview accommodationReview)
    {
        GuestFeedbackDTO = new GuestFeedbackDTO(accommodationReview);
        AccommodationReview = accommodationReview;
    }
}
