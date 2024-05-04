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

public partial class AccommodationStatisticsPage : Page
{
    private AccommodationStatisticsViewModel _accommodationStatisticsViewModel;
    private readonly User user;

    public AccommodationStatisticsPage(User user)
    {
        InitializeComponent();
        this.user = user;
    }

    public void BackButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }

    private void AccommodationSearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _accommodationStatisticsViewModel.AccommodationTextBox_TextChanged();
    }

    private void AccommodationSearchBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        _accommodationStatisticsViewModel.AccommodationTextBox_PreviewKeyDown(e.Key);
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        _accommodationStatisticsViewModel = new AccommodationStatisticsViewModel(user, this);
        DataContext = _accommodationStatisticsViewModel;
    }
}
