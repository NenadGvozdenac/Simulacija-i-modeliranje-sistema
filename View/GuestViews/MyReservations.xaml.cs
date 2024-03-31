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
        public EventHandler<int> ReviewClicked;

        private User _user;
        public UpcomingReservations UpcomingReservationsUserControl;
        public PastReservations PastReservationsUserControl;
        public RescheduleRequests RescheduleRequestsUserControl;
        public AccommodationRepository _accommodationRepository;
        public AccommodationReservationRepository _accommodationReservationRepository;
        public AccommodationReservationMovingRepository _accommodationReservationMovingRepository;
        public MyReservations(User user, AccommodationRepository accommodationRepository , AccommodationReservationRepository accommodationReservationRepository, AccommodationReservationMovingRepository accommodationReservationMovingRepository)
        {
            InitializeComponent();
            _user = user;
            _accommodationRepository = accommodationRepository;
            _accommodationReservationRepository = accommodationReservationRepository;
            _accommodationReservationMovingRepository = accommodationReservationMovingRepository;         
            SetUpMyReservations();
            Update();
        }

        public void SetUpMyReservations()
        {
            UpcomingReservationsUserControl = new UpcomingReservations(_user, _accommodationRepository, _accommodationReservationRepository);
            PastReservationsUserControl = new PastReservations(_user, _accommodationRepository, _accommodationReservationRepository);
            RescheduleRequestsUserControl = new RescheduleRequests(_user,_accommodationRepository, _accommodationReservationMovingRepository);
            UpcomingReservationsUserControl.RescheduleClicked += MyReservation_RescheduleClicked;
            PastReservationsUserControl.ReviewClicked += MyReservation_ReviewClicked;
        }

        public void Update()
        {
            Username_TextBlock.Text = _user.Username;
            MyReservationFrame.Content = UpcomingReservationsUserControl;
        }

        private void RescheduleAccommodationChangedMind()
        {
            MyReservationFrame.Content = UpcomingReservationsUserControl;
        }

        public void RefreshUpcomingReservations()
        {
            UpcomingReservationsUserControl.Update();
        }

        public void RefreshRecheduleRequests()
        {
            RescheduleRequestsUserControl.Update();
        }

        private void UpcomingReservations_Click(object sender, RoutedEventArgs e)
        {
            MyReservationFrame.Content = UpcomingReservationsUserControl;
        }

        private void PastReservations_Click(object sender, RoutedEventArgs e)
        {
            MyReservationFrame.Content = PastReservationsUserControl;
        }
        private void RescheduleRequests_Click(object sender, RoutedEventArgs e)
        {
            MyReservationFrame.Content = RescheduleRequestsUserControl;
        }
        private void MyReservation_RescheduleClicked(object sender, int reservationId)
        {
            AccommodationReservation reservation = _accommodationReservationRepository.GetById(reservationId);
            var a = new RescheduleAccommodation(reservation, _accommodationRepository, _accommodationReservationMovingRepository);
            a.ChangedMind += (sender, e) => RescheduleAccommodationChangedMind();
            a.SendRequestRefresh += (sender, e) => RefreshRecheduleRequests();
            MyReservationFrame.Content = a;
        } 

        private void MyReservation_ReviewClicked(object sender, int reservationId)
        {
            ReviewClicked?.Invoke(sender, reservationId);
        }
    }
}
