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
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;

namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for TouristDetails.xaml
    /// </summary>
    public partial class TouristDetails : UserControl
    {
        public Tours ToursUserControl;
        public TouristDetails(Tour detailedTour, User user)
        {
            InitializeComponent();
            ToursUserControl = new Tours(user);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NumberOfGuests_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GuestAgeDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GuestAgeUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GuestNumber_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void GuestNumberUp_Click(object sender, RoutedEventArgs e)
        {

        }
        private void GuestNumberDown_Click(object sender, RoutedEventArgs e)
        {

        }
        private void GuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddTourist_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
