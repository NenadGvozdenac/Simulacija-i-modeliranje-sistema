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
using BookingApp.Model.PathfinderModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.View.PathfinderViews;

namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for TourDatesUserControl.xaml
    /// </summary>
    public partial class TourDatesUserControl : UserControl
    {
        private Tour selectedTour {  get; set; }
        private int guestNumber {  get; set; }
        public TourStartTimeRepository tourStartTimeRepository { get; set; }
        public TouristRepository touristRepository { get; set; }
        public TouristReservationRepository touristReservationRepository { get; set; }
        public TourStartTimeRepository tourStarTimeRepository { get; set; }
        public List<TourStartTime> tourStartTimes { get; set; }
        public TourStartTime selectedTourStartTime {  get; set; }
        public List<Tourist> tourists { get; set; }
        public TourDatesUserControl(Tour detailedTour, int guestNumber, List<Tourist> tourists)
        {
            InitializeComponent();
            selectedTour = detailedTour;
            tourStartTimeRepository = new TourStartTimeRepository();
            tourStartTimes = new List<TourStartTime>();
            selectedTourStartTime = new TourStartTime();
            touristRepository = new TouristRepository();
            tourStarTimeRepository = new TourStartTimeRepository();
            touristReservationRepository = new TouristReservationRepository();
            this.guestNumber = guestNumber;
            this.tourists = tourists;

            findTourDates();
            tourDatesDataGrid.ItemsSource = tourStartTimes;

        }

        public void findTourDates()
        {
            foreach (TourStartTime tourStartTime in tourStartTimeRepository.GetAll())
            {
                if (tourStartTime.TourId == selectedTour.Id)
                {
                    tourStartTimes.Add(tourStartTime);
                }
            }
        }

        private void TourDatesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tourDatesDataGrid.SelectedItem != null)
            {
                selectedTourStartTime = (TourStartTime)tourDatesDataGrid.SelectedItem;
                TourReservation(selectedTourStartTime);

            }
        }

        private void TourReservation(TourStartTime tourStartTime)
        {
            if (!isTourGuestNumberValid())
            {
                return;
            }
            
                foreach(Tourist tourist in tourists)
                {
                    tourist.Id = touristRepository.NextId();
                    AddTourist(tourist);

                    CreateReservation(tourist, tourStartTime);

                    UpdateTourStartTime(tourStartTime);
                }
                MessageBox.Show("Dodati turisti!");
            
        }

        private bool isTourGuestNumberValid()
        {
            if (guestNumber > (selectedTour.Capacity - selectedTourStartTime.Guests))
            {
                noFreeSpacesMessage.Visibility = Visibility.Visible;
                freeSpaces.Visibility = Visibility.Visible;
                freeSpaces.Text = (selectedTour.Capacity - selectedTourStartTime.Guests).ToString();
                return false;

            }
            return true;
        }
        private void UpdateTourStartTime(TourStartTime tourStartTime)
        {
            tourStartTime.Guests = guestNumber;
            tourStartTimeRepository.Update(tourStartTime);
        }
        private void CreateReservation(Tourist tourist, TourStartTime tourStartTime)
        {
            TouristReservation touristReservation = new TouristReservation();
            touristReservation.Id = touristReservationRepository.NextId();
            touristReservation.Id_Tourist = tourist.Id;
            touristReservation.Id_TourTime = tourStartTime.Id;
            touristReservation.CheckpointId = -1;
            touristReservationRepository.Add(touristReservation);
        }
        private void AddTourist(Tourist tourist)
        {
            touristRepository.Add(tourist);
        }
        private void BackToTouristDetails_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowTourDetails(selectedTour.Id);
            }
        }
    }
}
