using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Resources.Types;
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

namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for ReservationSchedulingPage.xaml
    /// </summary>
    public partial class ReservationReschedulingPage : Page
    {
        private readonly User _user;
        private List<Accommodation> ownerAccommodations;
        private List<AccommodationReservationMoving> reservationMoving;

        public event EventHandler ReservationReschedulingPageClosed;
        public ReservationReschedulingPage(User user)
        {
            _user = user;

            reservationMoving = new List<AccommodationReservationMoving>();
            ownerAccommodations = new List<Accommodation>();

            InitializeComponent();

            Update();
        }

        private void InvokePageClosed()
        {
            ReservationReschedulingPageClosed?.Invoke(this, EventArgs.Empty);
        }

        private void Update()
        {
            ownerAccommodations.Clear();
            reservationMoving.Clear();

            ownerAccommodations = AccommodationRepository.GetInstance().GetAccommodationsByOwnerId(_user.Id);
            reservationMoving = AccommodationReservationMovingRepository.GetInstance().GetMovingsByAccommodations(ownerAccommodations).FindAll(moving => moving.Status == ReschedulingStatus.Pending);

            AddToPanel();
        }

        private void AddToPanel()
        {
            MainPanel.Children.Clear();

            foreach (AccommodationReservationMoving moving in reservationMoving)
            {
                LoadAccommodation(moving);
                LoadUser(moving);
                LoadReservation(moving);

                ReservationRescheduling reservationRescheduling = new ReservationRescheduling(moving);
                reservationRescheduling.Margin = new Thickness(15);

                reservationRescheduling.ReservationReschedulingDetails += (s, e) => ShowDetails(e);

                MainPanel.Children.Add(reservationRescheduling);
            }
        }

        private void LoadReservation(AccommodationReservationMoving moving)
        {
            moving.Reservation = AccommodationReservationRepository.GetInstance().GetById(moving.ReservationId);
        }

        private void ShowDetails(AccommodationReservationMoving e)
        {
            ReservationReschedulingDetailsPage reservationReschedulingDetailsPage = new ReservationReschedulingDetailsPage(e);
            reservationReschedulingDetailsPage.ReservationReschedulingDetailsPageClosed += (s, e) => Update();
            NavigationService.Navigate(reservationReschedulingDetailsPage);
        }

        private void LoadUser(AccommodationReservationMoving moving)
        {
            moving.Guest = UserRepository.GetInstance().GetById(moving.GuestId);
        }

        private void LoadAccommodation(AccommodationReservationMoving moving)
        {
            moving.Accommodation = AccommodationRepository.GetInstance().GetById(moving.AccommodationId);
        }

        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                InvokePageClosed();
                NavigationService.GoBack();
            }
        }
    }
}
