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
using System.Windows.Shapes;
using BookingApp.Model.MutualModels;
using BookingApp.Repository;

namespace BookingApp.View.GuestViews;


public partial class GuestMainWindow : Window
{
    private readonly User _user;
    public AccommodationRepository accomodationrepository { get; set; }
    public GuestMainWindow(User user)
    {
        InitializeComponent();       
        accomodationrepository = new AccommodationRepository();
        _user = user;
        Update();
    }

    public void SetActiveUserControl(UserControl control)
    {
        accommodation.Visibility = Visibility.Collapsed;
        myreservation.Visibility = Visibility.Collapsed;
        accommodationDetails.Visibility = Visibility.Collapsed;

        control.Visibility = Visibility.Visible;
    }

    private void Accommodations_Click(object sender, RoutedEventArgs e)
    {
        SetActiveUserControl(accommodation);
    }

    private void MyReservations_Click(object sender, RoutedEventArgs e)
    {
        SetActiveUserControl(myreservation);
    }

    public void ShowAccommodationDetails(int accommodationId)
    {
        Accommodation detailedAccommodation = accomodationrepository.GetById(accommodationId);

        accommodationDetails.SetAccommodation(detailedAccommodation);
        accommodationDetails.Visibility = Visibility.Visible;

        accommodation.Visibility = Visibility.Collapsed;
        myreservation.Visibility = Visibility.Collapsed;
    }

    private void Update()
    {
        accommodation.username.Content = _user.Username;
        accommodationDetails.username.Content = _user.Username;
        accommodationDetails._user = _user;
    }
}
