using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
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

namespace BookingApp.WPF.Views.OwnerViews;

public partial class DetailedRenovationView : Page
{
    public DetailedRenovationViewModel viewModel { get; set; }
    public DetailedRenovationView(AccommodationRenovation renovation)
    {
        viewModel = new(this, renovation);
        DataContext = viewModel;
        InitializeComponent();
    }

    private void BackArrowClick(object sender, MouseButtonEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
