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

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for GuestMainWindow.xaml
    /// </summary>
    public partial class GuestMainWindow : Window
    {
        private readonly User _user;

        public AccommodationRepository accomodationrepository { get; set; }
        public GuestMainWindow(User user)
        {
            InitializeComponent();
            _user = user;
            accomodationrepository = new AccommodationRepository();
            accommodation.username.Content = _user.Username;
            accommodationDetails.username.Content = _user.Username;
            accommodationDetails._user = _user;
        }

        public void SetActiveUserControl(UserControl control)
        {
            accommodation.Visibility = Visibility.Collapsed;
            myreservation.Visibility = Visibility.Collapsed;
            accommodationDetails.Visibility = Visibility.Collapsed;

            control.Visibility = Visibility.Visible;
        }

        private void BtnAccommodations_Click(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(accommodation);
        }

        private void BtnMyReservations_Click(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(myreservation);
        }

        public void ShowAccommodationDetails(int accommodationId)
        {
            // Pass the selected accommodation to the details UserControl
            Accommodation detailedAccommodation = accomodationrepository.GetById(accommodationId);

            accommodationDetails.SetAccommodation(detailedAccommodation);

            // Show the details UserControl
            accommodationDetails.Visibility = Visibility.Visible;

            // Hide other controls if needed
            accommodation.Visibility = Visibility.Collapsed;
            myreservation.Visibility = Visibility.Collapsed;
        }
    }
}
