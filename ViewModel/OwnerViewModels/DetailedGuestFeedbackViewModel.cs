using BookingApp.DTOs.OwnerDTOs;
using BookingApp.Miscellaneous;
using BookingApp.Model.MutualModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.OwnerViewModels;

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
