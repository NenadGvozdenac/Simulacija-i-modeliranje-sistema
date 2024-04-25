using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class RenovationControl : UserControl
{
    private RenovationCardViewModel renovationCardViewModel;
    public RenovationControl(AccommodationRenovationWrapper accommodationRenovationWrapper, AccommodationRenovation renovation)
    {
        InitializeComponent();

        renovationCardViewModel = new(this, renovation);
        DataContext = renovationCardViewModel;
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DetailedRenovationView detailedRenovationView = new(renovationCardViewModel.Renovation);
        NavigationService.GetNavigationService(this).Navigate(detailedRenovationView);
    }
}
