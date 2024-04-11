using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using BookingApp.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedGuestRatingViewModel
{
    public GuestRatingDTO GuestRatingDTO { get; set; }
    private GuestRating _guestRating;

    public DetailedGuestRatingViewModel(GuestRating guestRating)
    {
        _guestRating = guestRating;
        GuestRatingDTO = new(guestRating);
    }

    public void EyeButtonClicked(UserControl card)
    {
        DetailedGuestReviewPage detailedGuestReviewPage = new DetailedGuestReviewPage(_guestRating);
        NavigationService.GetNavigationService(card).Navigate(detailedGuestReviewPage);
    }
}
