using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.View.OwnerViews.Components;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.Domain.Models;
using BookingApp.Repositories;

namespace BookingApp.View.OwnerViews.MainWindowWrappers;

public partial class AccommodationWrapper : UserControl
{
    private MainPageViewModel _mainPageViewModel;

    public AccommodationWrapper(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();

        _mainPageViewModel = mainPageViewModel;

        AddAccommodations();
    }

    public void Refresh()
    {
        _mainPageViewModel.Refresh();
        AddAccommodations();
    }

    private void AddAccommodations()
    {
        Accommodations.Children.Clear();

        foreach(Accommodation accommodation in _mainPageViewModel.Accommodations)
        {
            AccommodationControl component = new AccommodationControl(accommodation, LocationRepository.GetInstance().GetById(accommodation.LocationId));
            component.Margin = new Thickness(15);

            component.AccommodationSeeMore += (sender, e) => InvokeSeeMore(e);

            Accommodations.Children.Add(component);
        }
    }

    private void InvokeSeeMore(Accommodation e)
    {
        DetailedAccommodationPage detailedAccommodationPage = new DetailedAccommodationPage(e);
        NavigationService.GetNavigationService(this).Navigate(detailedAccommodationPage);
    }
}
