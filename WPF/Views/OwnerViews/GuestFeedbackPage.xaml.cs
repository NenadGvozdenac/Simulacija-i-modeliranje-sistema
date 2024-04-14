using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews.Components;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Windows.Controls;

namespace BookingApp.WPF.Views.OwnerViews;
public partial class GuestFeedbackPage : Page
{
    public GuestFeedbackPage(User owner)
    {
        InitializeComponent();
        GuestFeedbacksViewModel guestFeedbacksViewModel = new GuestFeedbacksViewModel(this, owner);
        DataContext = guestFeedbacksViewModel;
    }

    private void BackButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
