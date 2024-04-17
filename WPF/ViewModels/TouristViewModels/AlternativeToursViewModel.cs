using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Application.UseCases;
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
        private Location currentLocation { get; set; }
        private Tour currentTour { get; set; }
        private int currentLocationId { get; set; }
        public AlternativeTours alternativeTours {  get; set; }
        public AlternativeToursViewModel(int locationId, Tour tour, AlternativeTours alternativeTours)
        {
            tours = new ObservableCollection<Tour>();
            currentLocation = LocationService.GetInstance().GetById(locationId);
            currentLocationId = locationId;
            currentTour = tour;
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
            foreach (Tour tour in TourService.GetInstance().GetAll())
            {
                if (currentLocationId == tour.LocationId)
                {
                    if (currentTour.Id != tour.Id)
                    {
                        tour.Location = LocationService.GetInstance().GetById(tour.LocationId);
                        tour.Images = TourImageService.GetInstance().GetImagesByTourId(tour.Id);
                        tour.Language = LanguageService.GetInstance().GetById(tour.LanguageId);
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
            Location location = LocationService.GetInstance().GetById(currentLocationId);

            foreach (Tour tour in TourService.GetInstance().GetAll())
            {
                if (LocationService.GetInstance().GetById(tour.LocationId).Country == currentLocation.Country && LocationService.GetInstance().GetById(tour.LocationId).City == currentLocation.City)
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
