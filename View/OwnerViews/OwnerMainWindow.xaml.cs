using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.View.OwnerViews;
using BookingApp.View.OwnerViews.Components;
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

namespace BookingApp.View;

/// <summary>
/// Interaction logic for OwnerMainWindow.xaml
/// </summary>
public partial class OwnerMainWindow : Window
{
    private readonly User _user;
    private ObservableCollection<Accommodation> _accommodations;
    private AccommodationRepository _accommodationRepository;
    private LocationRepository _locationRepository;
    private AccommodationImageRepository _accommodationImageRepository;
    public OwnerMainWindow(User user)
    {
        InitializeComponent();
        _user = user;
        _accommodations = new ObservableCollection<Accommodation>();
        _accommodationRepository = new AccommodationRepository();
        _locationRepository = new LocationRepository();
        _accommodationImageRepository = new AccommodationImageRepository();
        Update();
    }

    private void Update()
    {
        _accommodations.Clear();
        Accommodations.Children.Clear();

        foreach(Accommodation accommodation in _accommodationRepository.GetAccommodationsByOwnerId(_user.Id))
        {
            accommodation.Images = _accommodationImageRepository.GetImagesByAccommodationId(accommodation.Id);
            AccommodationControl accommodationView = new AccommodationControl(accommodation, _locationRepository.GetById(accommodation.LocationId));
            accommodationView.Margin = new Thickness(15);
            _accommodations.Add(accommodation);
            Accommodations.Children.Add(accommodationView);
        }

        ToggleNavbar();
    }

    private void ExitApplication(object sender, MouseButtonEventArgs e)
    {
        Close();
    }

    private void HamburgerMenuClick(object sender, MouseButtonEventArgs e)
    {
        ToggleNavbar();
    }

    private void ToggleNavbar()
    {
        if (Reservations.Visibility == Visibility.Visible)
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
        AddAccommodationWindow addAccommodationWindow = new AddAccommodationWindow(_user, _accommodationRepository, _accommodationImageRepository);
        addAccommodationWindow.ShowDialog();

        Update();
    }
}
