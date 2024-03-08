using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.View.OwnerViews;
using BookingApp.View.OwnerViews.Components;
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

namespace BookingApp.View;

/// <summary>
/// Interaction logic for OwnerMainWindow.xaml
/// </summary>
public partial class OwnerMainWindow : Window
{
    private readonly User _user;
    private List<Accommodation> _accommodations;
    private AccommodationRepository _accommodationRepository;
    public OwnerMainWindow(User user)
    {
        InitializeComponent();
        _user = user;
        _accommodations = new List<Accommodation>();
        _accommodationRepository = new AccommodationRepository();
        LoadAccommodations();
    }

    private void LoadAccommodations()
    {
        _accommodations.Clear();
        Accommodations.Children.Clear();

        _accommodations = _accommodationRepository.GetAccommodationsByOwnerId(_user.Id);

        foreach(Accommodation accommodation in _accommodations)
        {
            AccommodationControl accommodationView = new AccommodationControl(accommodation);
            Accommodations.Children.Add(accommodationView);
        }
    }

    private void ExitApplication(object sender, MouseButtonEventArgs e)
    {
        Close();
    }

    private void HamburgerMenuClick(object sender, MouseButtonEventArgs e)
    {
        if(Reservations.Visibility == Visibility.Visible)
        {
            Reservations.Visibility = Visibility.Collapsed;
            Navbar.ColumnDefinitions[0].Width = new GridLength(0);
        }
        else
        {
            Reservations.Visibility = Visibility.Visible;
            Navbar.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
        }
    }

    private void AddAccommodationEvent(object sender, RoutedEventArgs e)
    {
        AddAccommodationWindow addAccommodationWindow = new AddAccommodationWindow(_user);
        addAccommodationWindow.ShowDialog();
    }
}
