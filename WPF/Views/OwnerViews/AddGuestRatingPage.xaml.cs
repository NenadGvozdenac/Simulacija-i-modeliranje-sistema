using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.Domain.Models;

namespace BookingApp.View.OwnerViews;

public partial class AddGuestRatingPage : Page
{
    public event EventHandler NavigationCompleted;

    private AddGuestRatingViewModel _addGuestRatingViewModel;

    public AddGuestRatingPage(GuestRating uncheckedGuestRating)
    {
        _addGuestRatingViewModel = new AddGuestRatingViewModel(this, uncheckedGuestRating);

        DataContext = _addGuestRatingViewModel;

        InitializeComponent();
    }

    public void OnNavigationCompleted()
    {
        NavigationCompleted?.Invoke(this, EventArgs.Empty);
    }

    private void Back_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
