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

public partial class SuggestionsPage : Page
{
    private User user;
    public SuggestionsViewModel ViewModel { get; set; }

    public SuggestionsPage(User user)
    {
        InitializeComponent();
        this.user = user;
        ViewModel = new SuggestionsViewModel(this, user);
        DataContext = ViewModel;
    }

    private void Back_Click(object sender, MouseButtonEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.Refresh();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new AddAccommodationPage(this.user));
    }
}
