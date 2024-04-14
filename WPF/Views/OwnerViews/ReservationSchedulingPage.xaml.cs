using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.Views.OwnerViews;

public partial class ReservationReschedulingPage : Page
{
    public event EventHandler PageClosed;
    public ReservationReschedulingViewModel ReservationReschedulingViewModel { get; set; }
    public ReservationReschedulingPage(User user)
    {
        InitializeComponent();
        ReservationReschedulingViewModel = new ReservationReschedulingViewModel(this, user);
        DataContext = ReservationReschedulingViewModel;
    }

    private void InvokePageClosed()
    {
        PageClosed?.Invoke(this, EventArgs.Empty);
    }

    private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            InvokePageClosed();
            NavigationService.GoBack();
        }
    }

    public void UpdateReservations()
    {
        ReservationReschedulingViewModel.Update();
    }
}