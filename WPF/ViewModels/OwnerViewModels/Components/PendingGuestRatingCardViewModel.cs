using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.OwnerViews.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.Components;

public class PendingGuestRatingCardViewModel
{
    private GuestRatingControlPending guestRatingControlPending;
    private GuestRating guestRating;
    private bool isPencilEnabled;

    public GuestRatingDTO GuestRatingDTO { get; set; }

    public PendingGuestRatingCardViewModel(GuestRatingControlPending guestRatingControlPending, GuestRating guestRating, bool isPencilEnabled)
    {
        this.guestRatingControlPending = guestRatingControlPending;
        this.guestRating = guestRating;
        this.isPencilEnabled = isPencilEnabled;

        if (!isPencilEnabled)
        {
            guestRatingControlPending.EyeButton.Visibility = Visibility.Hidden;
        }

        GuestRatingDTO = new(guestRating);
    }

    public void EyeButtonClicked(GuestRatingControlPending guestRatingControlPending)
    {
        AddGuestRatingPage addGuestRating = new AddGuestRatingPage(guestRating);
        addGuestRating.NavigationCompleted += (s, e) => guestRatingControlPending.Refresh();
        NavigationService.GetNavigationService(guestRatingControlPending).Navigate(addGuestRating);
    }
}
