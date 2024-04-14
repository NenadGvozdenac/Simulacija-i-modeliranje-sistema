using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.OwnerViews.GuestReviewControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class GuestReviewsViewModel
{
    public ReviewedGuestReviews ReviewedGuestReviews { get; set; }
    public PendingGuestReviews PendingGuestReviews { get; set; }
    public GuestReviewPage Page { get; set; }

    public GuestReviewsViewModel(GuestReviewPage page, ReviewedGuestReviews reviewedGuestReviews, PendingGuestReviews pendingGuestReviews)
    {
        Page = page;
        ReviewedGuestReviews = reviewedGuestReviews;
        PendingGuestReviews = pendingGuestReviews;
    }
}
