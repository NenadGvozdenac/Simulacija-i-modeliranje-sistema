using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.View.OwnerViews;
using BookingApp.View.OwnerViews.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class GuestFeedbacksViewModel
{
    private GuestFeedbackPage _page;
    private User _owner;

    public GuestFeedbacksViewModel(GuestFeedbackPage page, User user)
    {
        _owner = user;
        _page = page;
        LoadFeedbacks();
    }

    private void LoadFeedbacks()
    {
        foreach (AccommodationReview review in AccommodationReviewService.GetInstance().GetByOwnerId(_owner.Id))
        {
            if (!GuestRatingService.GetInstance().ExistsReviewOfGuest(review.Guest, review.Accommodation))
            {
                continue;
            }

            GuestFeedbackControl feedbackControl = new GuestFeedbackControl(review);
            feedbackControl.Margin = new System.Windows.Thickness(15);

            _page.MainPanel.Children.Add(feedbackControl);
        }
    }
}
