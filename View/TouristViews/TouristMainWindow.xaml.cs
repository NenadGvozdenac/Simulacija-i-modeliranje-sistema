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
using BookingApp.Domain.Models;
using BookingApp.Repositories;

namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window
    {
        private readonly User _user;
        public TourRepository tourRepository { get; set; }
        public Tours ToursUserControl { get; set; }
        public TouristRepository touristRepository {  get; set; }
        public TouristReservationRepository touristReservationRepository {get; set; }
        public TourStartTimeRepository tourStartTimeRepository { get; set; }
        public TouristDetails ToursDetailsUserControl { get; set; }
        public VisitedTours ToursVisitedUserControl { get; set; }
        public TourVoucherRepository tourVoucherRepository {  get; set; }

        public TouristMainWindow(User user)
        {
            InitializeComponent();
            tourRepository = new TourRepository();
            _user = user;
            Update(_user);
            ToursUserControl = new Tours(user);
            TouristWindowFrame.Content = ToursUserControl;
            touristRepository = new TouristRepository();
            touristReservationRepository = new TouristReservationRepository();
            tourStartTimeRepository = new TourStartTimeRepository();
            tourVoucherRepository = new TourVoucherRepository();
        }

        
        public void ShowTourDetails(int tourId)
        {
            Tour detailedTour = tourRepository.GetById(tourId);
            TouristWindowFrame.Content = new TouristDetails(detailedTour, _user);
        }

        public void ShowTourDates(Tour tour, int guestNumber, List<Tourist> tourists, TourVoucher tourVoucher)
        {
            Tour detailedTour = tour;
            TouristWindowFrame.Content = new TourDatesUserControl(_user, detailedTour, guestNumber, tourists, touristRepository, touristReservationRepository, tourStartTimeRepository, tourVoucher, tourVoucherRepository);
        }
        public void ShowAlternativeTours(int locationId, Tour tour)
        {
            TouristWindowFrame.Content = new AlternativeTours(locationId, tour);
        }
        private void Update(User user)
        {
            ToursUserControl = new Tours(user);

        }

        private void MyTours_Click(object sender, RoutedEventArgs e)
        {
            TouristWindowFrame.Content = new VisitedTours(_user);
        }

        public void RateTour(User user, TouristReservationRepository touristReservationRepository, TourRepository tourRepository, TourReviewRepository tourReviewRepository, TourReviewImageRepository tourReviewImageRepository, int tourId)
        {
            TouristWindowFrame.Content = new RateTour(user, touristReservationRepository, tourRepository, tourReviewRepository, tourReviewImageRepository, tourId);
        }

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            TouristWindowFrame.Content = ToursUserControl;
        }

        private void MyVouchers_Click(object sender, RoutedEventArgs e)
        {
            TouristWindowFrame.Content = new TouristVouchers(_user.Id);
        }

        internal void ShowTourDates(Tour selectedTour, int guestNumber, List<Tourist> tourists)
        {
            throw new NotImplementedException();
        }
    }
}
