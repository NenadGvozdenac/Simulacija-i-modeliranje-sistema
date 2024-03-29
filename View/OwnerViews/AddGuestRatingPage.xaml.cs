using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BookingApp.DTOs.OwnerDTOs;
using BookingApp.ViewModel.OwnerViewModels;

namespace BookingApp.View.OwnerViews;

public partial class AddGuestRatingPage : Page
{
    public event EventHandler NavigationCompleted;

    private AddGuestRatingViewModel _addGuestRatingViewModel;

    public AddGuestRatingPage(GuestRating uncheckedGuestRating)
    {
        _addGuestRatingViewModel = new AddGuestRatingViewModel(new(uncheckedGuestRating));

        DataContext = _addGuestRatingViewModel;

        InitializeComponent();
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        NavigateToPreviousPage();
    }

    private void ConfirmButtonClick(object sender, RoutedEventArgs e)
    {
        bool successfullyUpdatedGuestRating = _addGuestRatingViewModel.UpdateGuestRating();

        if(!successfullyUpdatedGuestRating)
        {
            return;
        }

        NavigateToPreviousPage();
    }
    private void OnNavigationCompleted()
    {
        NavigationCompleted?.Invoke(this, EventArgs.Empty);
    }

    private void NavigateToPreviousPage()
    {
        if (NavigationService.CanGoBack)
        {
            OnNavigationCompleted();
            NavigationService.GoBack();
        }
    }


    private void Back_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
