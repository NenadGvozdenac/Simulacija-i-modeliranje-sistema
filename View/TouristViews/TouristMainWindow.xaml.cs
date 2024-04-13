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
        }

        
        public void ShowTourDetails(int tourId)
        {
            Tour detailedTour = tourRepository.GetById(tourId);
            TouristWindowFrame.Content = new TouristDetails(detailedTour, _user);
        }

        public void ShowTourDates(Tour tour, int guestNumber, List<Tourist> tourists)
        {
            Tour detailedTour = tour;
            TouristWindowFrame.Content = new TourDatesUserControl(detailedTour, guestNumber, tourists, touristRepository, touristReservationRepository, tourStartTimeRepository);
        }
        public void ShowAlternativeTours(int locationId, Tour tour)
        {
            TouristWindowFrame.Content = new AlternativeTours(locationId, tour);
        }
        private void Update(User user)
        {
            ToursUserControl = new Tours(user);

        }
    }
}
