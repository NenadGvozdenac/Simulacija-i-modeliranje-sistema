using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;
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
