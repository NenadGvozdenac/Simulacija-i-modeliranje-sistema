using BookingApp.Domain.Models;
using BookingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for AlternativeTours.xaml
    /// </summary>
    public partial class AlternativeTours : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Tour> tours { get; set; }
        private TourRepository tourRepository { get; set; }
        private LocationRepository locationRepository { get; set; }
        private TourImageRepository tourImageRepository { get; set; }
        private LanguageRepository languageRepository { get; set; }
        private Location currentLocation { get; set; }
        private Tour currentTour { get; set; }
        private int currentLocationId { get; set; }

        public AlternativeTours(int locationId, Tour tour)
        {
            InitializeComponent();
            DataContext = this;
            tourRepository = new TourRepository();
            tours = new ObservableCollection<Tour>();
            locationRepository = new LocationRepository();
            currentLocation = locationRepository.GetById(locationId);
            currentLocationId = locationId;
            currentTour = tour;
            tourImageRepository = new TourImageRepository();
            languageRepository = new LanguageRepository();
            Update();
        }

        private ObservableCollection<Tour> _filteredTours;
        public ObservableCollection<Tour> FilteredTours
        {
            get { return _filteredTours; }
            set
            {
                if (_filteredTours != value)
                {
                    _filteredTours = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update()
        {
            foreach (Tour tour in tourRepository.GetAll())
            {
                if (currentLocationId == tour.LocationId)
                {
                    if (currentTour.Id != tour.Id)
                    {
                        tour.Location = locationRepository.GetById(tour.LocationId);
                        tour.Images = tourImageRepository.GetImagesByTourId(tour.Id);
                        tour.Language = languageRepository.GetById(tour.LanguageId);
                        tours.Add(tour);
                    }else
                    {
                        continue;
                    }
                }
            }
            FilteredTours = new ObservableCollection<Tour>(tours);

        }

        private void FindToursWithLocation()
        {
            Location location = locationRepository.GetById(currentLocationId);

            foreach (Tour tour in tourRepository.GetAll())
            {
                if(locationRepository.GetById(tour.LocationId).Country == currentLocation.Country && locationRepository.GetById(tour.LocationId).City == currentLocation.City)
                {
                    FilteredTours.Add(tour);
                }
            }
            FilteredTours = new ObservableCollection<Tour>(tours);

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowTourDetails(currentTour.Id);
            }
        }
    }
}
