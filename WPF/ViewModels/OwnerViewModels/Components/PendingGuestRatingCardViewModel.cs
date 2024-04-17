using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.OwnerViews.Components;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.Components;

public class PendingGuestRatingCardViewModel
{
    private GuestRating guestRating;

    public GuestRatingDTO GuestRatingDTO { get; set; }

    public PendingGuestRatingCardViewModel(GuestRatingControlPending guestRatingControlPending, GuestRating guestRating, bool isPencilEnabled)
    {
        this.guestRating = guestRating;

        if (!isPencilEnabled)
        {
            guestRatingControlPending.EyeButton.Visibility = Visibility.Hidden;
        }

        GuestRatingDTO = new(guestRating);
    }

    public void EyeButtonClicked(GuestRatingControlPending guestRatingControlPending)
    {
        AddGuestRatingPage addGuestRating = new AddGuestRatingPage(guestRating);
        NavigationService.GetNavigationService(guestRatingControlPending).Navigate(addGuestRating);
    }
}
