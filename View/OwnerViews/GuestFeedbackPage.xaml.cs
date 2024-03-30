﻿using BookingApp.Model.MutualModels;
using BookingApp.Services.Owner;
using BookingApp.View.OwnerViews.Components;
using System;
using System.Windows.Controls;

namespace BookingApp.View.OwnerViews;
public partial class GuestFeedbackPage : Page
{
    private User _owner;
    public GuestFeedbackPage(User owner)
    {
        _owner = owner;
        InitializeComponent();
        LoadFeedbacks();
    }

    private void LoadFeedbacks()
    {
        foreach(AccommodationReview review in AccommodationReviewService.GetInstance().GetByOwnerId(_owner.Id))
        {
            GuestFeedbackControl feedbackControl = new GuestFeedbackControl(review);
            feedbackControl.Margin = new System.Windows.Thickness(15);

            MainPanel.Children.Add(feedbackControl);
        }
    }

    private void BackButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
