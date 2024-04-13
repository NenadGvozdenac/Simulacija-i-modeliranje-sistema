using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public User user {  get; set; }
        public TourDatesUserControl(User user, Tour detailedTour, int guestNumber, List<Tourist> tourists, TouristRepository touristRepository, TouristReservationRepository touristReservationRepository, TourStartTimeRepository tourStartTimeRepo)
        {
            InitializeComponent();
            this.user = user;
            selectedTour = detailedTour;
            tourStartTimes = new List<TourStartTime>();
            selectedTourStartTime = new TourStartTime();
            this.touristRepository = touristRepository;
            this.tourStartTimeRepository = tourStartTimeRepo;
            this.touristReservationRepository = touristReservationRepository;
            this.guestNumber = guestNumber;
            this.tourists = tourists;
            touristsAddedMessage.Visibility = Visibility.Hidden;


            findTourDates();
            tourDatesDataGrid.ItemsSource = tourStartTimes;

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                noFreeSpacesMessage.Visibility = Visibility.Hidden;
                freeSpaces.Visibility = Visibility.Hidden;
                alternativeTours.Visibility = Visibility.Hidden;

                if (touristsAddedMessage.Visibility == Visibility.Visible)
                {
                    return;
                }
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
            List<Tourist> touristList = new List<Tourist>();
            foreach(Tourist t in tourists)
            {
                touristList.Add(t);
            }
                foreach(Tourist tourist in touristList)
                {
                    tourist.Id = touristRepository.NextId();
                    AddTourist(tourist);

                    CreateReservation(tourist, tourStartTime);

                    UpdateTourStartTime(tourStartTime);
                }
            touristsAddedMessage.Visibility = Visibility.Visible;
            return;
        }

        private bool isTourGuestNumberValid()
        {
            if (tourists.Count() >= (selectedTour.Capacity - selectedTourStartTime.Guests)+1)
            {
                noFreeSpacesMessage.Visibility = Visibility.Visible;
                freeSpaces.Visibility = Visibility.Visible;
                if(selectedTour.Capacity - selectedTourStartTime.Guests == 0)
                {
                    alternativeTours.Visibility = Visibility.Visible;

                }
                touristsAddedMessage.Visibility = Visibility.Hidden;
                freeSpaces.Text = (selectedTour.Capacity - selectedTourStartTime.Guests).ToString();
                return false;

            }
            return true;
        }
        private void UpdateTourStartTime(TourStartTime tourStartTime)
        {
            tourStartTime.Guests += tourists.Count();
            tourStartTimeRepository.Update(tourStartTime);
            tourists.Clear();
        }
        private void CreateReservation(Tourist tourist, TourStartTime tourStartTime)
        {
            TouristReservation touristReservation = new TouristReservation();
            touristReservation.Id = touristReservationRepository.NextId();
            touristReservation.Id_Tourist = tourist.Id;
            touristReservation.Id_TourTime = tourStartTime.Id;
            touristReservation.CheckpointId = -1;
            touristReservation.UserId = user.Id;
            touristReservationRepository.Add(touristReservation);
        }
        private void AddTourist(Tourist tourist)
        {
            foreach(Tourist t in touristRepository.GetAll())
            {
                if(t.Id == tourist.Id)
                {
                    return;
                }
            }
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

        private void AlternativeTours_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowAlternativeTours(selectedTour.LocationId, selectedTour);

            }
        }
    }
}
