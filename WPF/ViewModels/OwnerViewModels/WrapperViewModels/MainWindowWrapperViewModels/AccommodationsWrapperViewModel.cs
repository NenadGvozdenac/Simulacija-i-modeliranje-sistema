using BookingApp.Domain.Models;
using BookingApp.Resources.Types;
using BookingApp.WPF.Views.OwnerViews.Components;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.GuestViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;
using BookingApp.Repositories;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.WrapperViewModels.MainWindowWrapperViewModels;

public class AccommodationsWrapperViewModel
{
    public MainPageViewModel _mainPageViewModel;
    public AccommodationWrapper accommodationWrapper;

    public AccommodationsWrapperViewModel(AccommodationWrapper accommodationWrapper, MainPageViewModel mainPageViewModel)
    {
        this.accommodationWrapper = accommodationWrapper;
        this._mainPageViewModel = mainPageViewModel;

        AddAccommodations();
    }

    public void Refresh()
    {
        _mainPageViewModel.Load();
        AddAccommodations();
    }

    private void AddAccommodations()
    {
        accommodationWrapper.Accommodations.Children.Clear();

        foreach (Accommodation accommodation in _mainPageViewModel.Accommodations)
        {
            AccommodationControl component = new AccommodationControl(accommodation);
            component.Margin = new Thickness(15);

            accommodationWrapper.Accommodations.Children.Add(component);
        }
    }

    private void InvokeSeeMore(Accommodation e)
    {
        DetailedAccommodationPage detailedAccommodationPage = new DetailedAccommodationPage(e);
        NavigationService.GetNavigationService(accommodationWrapper).Navigate(detailedAccommodationPage);
    }
}
