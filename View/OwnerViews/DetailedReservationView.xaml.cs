using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Resources.Types;
using BookingApp.ViewModel.OwnerViewModels;
using System;
using System.Collections.Generic;
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

namespace BookingApp.View.OwnerViews;

public partial class DetailedReservationView : Page
{
    private DetailedReservationViewModel _viewModel;
    public DetailedReservationView(AccommodationReservation accommodationReservation)
    {
        _viewModel = new DetailedReservationViewModel(accommodationReservation);
        DataContext = _viewModel;

        InitializeComponent();
    }

    private void BackArrowClick(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
