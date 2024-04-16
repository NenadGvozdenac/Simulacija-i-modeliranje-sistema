using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.Views.OwnerViews;

public partial class ReservationReschedulingPage : Page
{
    public ReservationReschedulingViewModel ReservationReschedulingViewModel { get; set; }
    public ReservationReschedulingPage(User user)
    {
        InitializeComponent();
        ReservationReschedulingViewModel = new ReservationReschedulingViewModel(this, user);
        DataContext = ReservationReschedulingViewModel;
    }

    private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }

    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        ReservationReschedulingViewModel.Update();
    }
}