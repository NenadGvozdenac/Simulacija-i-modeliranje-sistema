using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
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

namespace BookingApp.View.GuestViews.Components
{
    /// <summary>
    /// Interaction logic for FreeDatesAlternative.xaml
    /// </summary>
    public partial class FreeDatesAlternative : UserControl
    {

        public AccommodationReservationRepository accomodationReservationRepository;
        public AccommodationReservation reservation { get; set; }
        public FreeDatesAlternative()
        {
            InitializeComponent();
            reservation = new AccommodationReservation();
        }

        private void ConfrimReservation_Click(object sender, RoutedEventArgs e)
        {
            accomodationReservationRepository.Add(reservation);
            ConfirmButton.IsEnabled = false;
            SuccessfullTextBox.Visibility = Visibility.Visible;
        }
    }
}
