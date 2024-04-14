using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class AlternativeToursViewModel
    {
        public ObservableCollection<Tour> tours { get; set; }
        private TourRepository tourRepository { get; set; }
        private LocationRepository locationRepository { get; set; }
        private TourImageRepository tourImageRepository { get; set; }
        private LanguageRepository languageRepository { get; set; }
        private Location currentLocation { get; set; }
        private Tour currentTour { get; set; }
        private int currentLocationId { get; set; }
        public AlternativeTours alternativeTours {  get; set; }
        public AlternativeToursViewModel(int locationId, Tour tour, AlternativeTours alternativeTours)
        {
            tourRepository = new TourRepository();
            tours = new ObservableCollection<Tour>();
            locationRepository = new LocationRepository();
            currentLocation = locationRepository.GetById(locationId);
            currentLocationId = locationId;
            currentTour = tour;
            tourImageRepository = new TourImageRepository();
            languageRepository = new LanguageRepository();
            Update();
            this.alternativeTours = alternativeTours;
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

        public void Update()
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
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            FilteredTours = new ObservableCollection<Tour>(tours);

        }

        public void FindToursWithLocation()
        {
            Location location = locationRepository.GetById(currentLocationId);

            foreach (Tour tour in tourRepository.GetAll())
            {
                if (locationRepository.GetById(tour.LocationId).Country == currentLocation.Country && locationRepository.GetById(tour.LocationId).City == currentLocation.City)
                {
                    FilteredTours.Add(tour);
                }
            }
            FilteredTours = new ObservableCollection<Tour>(tours);

        }

        public void Return_Click(object sender, RoutedEventArgs e)
        {
            /*Window parentWindow = Window.GetWindow(this);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowTourDetails(currentTour.Id);
            }*/
            NavigationService.GetNavigationService(alternativeTours).GoBack();

        }
    }
}
