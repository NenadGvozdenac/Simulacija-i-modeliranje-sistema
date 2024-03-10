using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
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
    private GuestRatingRepository _guestRatingRepository;
    private UserRepository _userRepository;
    public OwnerMainWindow(User user)
    {
        InitializeComponent();
        _user = user;
        _accommodations = new ObservableCollection<Accommodation>();
        _accommodationRepository = new AccommodationRepository();
        _locationRepository = new LocationRepository();
        _accommodationImageRepository = new AccommodationImageRepository();
        _userRepository = new UserRepository();
        _guestRatingRepository = new GuestRatingRepository();

        Update();
    }

    public void Update()
    {
        _accommodations.Clear();
        Accommodations.Children.Clear();

        foreach(Accommodation accommodation in _accommodationRepository.GetAccommodationsByOwnerId(_user.Id))
        {
            accommodation.Images = _accommodationImageRepository.GetImagesByAccommodationId(accommodation.Id);
            accommodation.Location = _locationRepository.GetById(accommodation.LocationId);
            AccommodationControl accommodationView = new AccommodationControl(accommodation, _locationRepository.GetById(accommodation.LocationId), _accommodationRepository, _accommodationImageRepository);
            accommodationView.Margin = new Thickness(15);
            _accommodations.Add(accommodation);
            Accommodations.Children.Add(accommodationView);
        }
        
        HideLeftNavbar();
        HideRightNavbar();
    }

    private void ExitApplication(object sender, MouseButtonEventArgs e)
    {
        Close();
    }

    private void HamburgerMenuClick(object sender, MouseButtonEventArgs e)
    {
        if (LeftNavbar.Visibility == Visibility.Collapsed)
        {
            ShowLeftNavbar();
        }
        else
        {
            HideLeftNavbar();
        }
    }

    private void ThreeDotsClick(object sender, MouseButtonEventArgs e)
    {
        if (RightNavbar.Visibility == Visibility.Collapsed)
        {
            ShowRightNavbar();
        }
        else
        {
            HideRightNavbar();
        }
    }

    private void ShowLeftNavbar()
    {
        LeftNavbar.Visibility = Visibility.Visible;
        Navbar.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
        RightNavbar.Visibility = Visibility.Collapsed;
        Navbar.ColumnDefinitions[2].Width = new GridLength(0);
    }

    private void HideLeftNavbar()
    {
        LeftNavbar.Visibility = Visibility.Collapsed;
        Navbar.ColumnDefinitions[0].Width = new GridLength(0);
    }

    private void ShowRightNavbar()
    {
        RightNavbar.Visibility = Visibility.Visible;
        Navbar.ColumnDefinitions[2].Width = new GridLength(0.6, GridUnitType.Star);
        LeftNavbar.Visibility = Visibility.Collapsed;
        Navbar.ColumnDefinitions[0].Width = new GridLength(0);
    }

    private void HideRightNavbar()
    {
        RightNavbar.Visibility = Visibility.Collapsed;
        Navbar.ColumnDefinitions[2].Width = new GridLength(0);
    }

    private void AddAccommodationClick(object sender, RoutedEventArgs e)
    {
        AddAccommodationWindow addAccommodationWindow = new AddAccommodationWindow(_user, _accommodationRepository, _accommodationImageRepository);
        addAccommodationWindow.ShowDialog();

        Update();
    }

    private void GuestReviewsButtonClick(object sender, RoutedEventArgs e)
    {
        GuestReviewWindow guestReviewWindow = new GuestReviewWindow(_user, _userRepository, _accommodationRepository, _guestRatingRepository, _locationRepository);
        guestReviewWindow.ShowDialog();
    }
}
