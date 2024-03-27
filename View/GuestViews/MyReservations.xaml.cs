using BookingApp.Model.MutualModels;
using BookingApp.Repository;
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

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : UserControl
    {

        private User _user;
        public UpcomingReservations UpcomingReservationsUserControl;
        public AccommodationRepository _accommodationRepository;
        public AccommodationReservationRepository _accommodationReservationRepository;
        public MyReservations(User user, AccommodationRepository accommodationRepository , AccommodationReservationRepository accommodationReservationRepository)
        {
            InitializeComponent();
            _user = user;
            _accommodationRepository = accommodationRepository;
            _accommodationReservationRepository = accommodationReservationRepository;
            UpcomingReservationsUserControl = new UpcomingReservations(_accommodationRepository, _accommodationReservationRepository);
            Update();
        }

        public void Update()
        {
            Username_TextBlock.Text = _user.Username;
            //var b = new UpcomingReservations(_accommodationRepository, _accommodationReservationRepository); ;
            //UpcomingReservationsUserControl = b;
            MyReservationFrame.Content = UpcomingReservationsUserControl;
        }

        public void RefreshUpcomingReservations()
        {
            UpcomingReservationsUserControl.Update();
        }

        private void UpcomingReservations_Click(object sender, RoutedEventArgs e)
        {
            MyReservationFrame.Content = UpcomingReservationsUserControl;
        }
    }
}
