using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews.Components;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Windows.Controls;

namespace BookingApp.WPF.Views.OwnerViews;
public partial class GuestFeedbackPage : Page
{
    private User owner;
    public GuestFeedbackPage(User owner)
    {
        this.owner = owner;
        InitializeComponent();
    }

    private void BackButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }

    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        GuestFeedbacksViewModel guestFeedbacksViewModel = new GuestFeedbacksViewModel(this, owner);
        DataContext = guestFeedbacksViewModel;
    }
}
