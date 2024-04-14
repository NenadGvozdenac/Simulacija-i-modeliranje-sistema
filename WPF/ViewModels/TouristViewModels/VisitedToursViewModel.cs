using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.View.TouristViews;
using BookingApp.View.TouristViews.Components;
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

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class VisitedToursViewModel
    {
        public ObservableCollection<Tour> tours { get; set; }

        private ObservableCollection<Tour> _myTours;
        public ObservableCollection<Tour> MyTours
        {
            get { return _myTours; }
            set
            {
                if (_myTours != value)
                {
                    _myTours = value;
                    OnPropertyChanged();
                }
            }
        }
        public TouristReservationRepository touristReservationRepository { get; set; }
        public TourStartTimeRepository tourStartTimeRepository { get; set; }
        public LocationRepository locationRepository { get; set; }
        public TourImageRepository tourImageRepository { get; set; }
        public LanguageRepository languageRepository { get; set; }
        public TourRepository tourRepository { get; set; }
        public User _user { get; set; }
        public VisitedTours visitedTours { get; set; }
        public VisitedToursViewModel(User user, VisitedTours _visitedTours)
        {
            visitedTours = _visitedTours;
            touristReservationRepository = new TouristReservationRepository();
            tourStartTimeRepository = new TourStartTimeRepository();
            tourRepository = new TourRepository();
            languageRepository = new LanguageRepository();
            locationRepository = new LocationRepository();
            tourImageRepository = new TourImageRepository();
            tours = new ObservableCollection<Tour>();
            _user = user;
            Update();

        }

        public void Update()
        {
            tours.Clear();
            foreach (TouristReservation touristReservation in touristReservationRepository.GetAll())
            {
                if (touristReservation.UserId == _user.Id && touristReservation.CheckpointId != -1)
                {
                    foreach (TourStartTime tourStartTime in tourStartTimeRepository.GetAll())
                    {
                        if (tourStartTime.Id == touristReservation.Id_TourTime)
                        {
                            Tour tour = tourRepository.GetById(tourStartTime.TourId);
                            tour.Location = locationRepository.GetById(tour.LocationId);
                            tour.Images = tourImageRepository.GetImagesByTourId(tour.Id);
                            tour.Language = languageRepository.GetById(tour.LanguageId);
                            tours.Add(tour);
                        }
                    }
                }
            }
            MyTours = new ObservableCollection<Tour>(tours);
        }

        public void TourCardWithStar_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TourCardWithStar tourCard)
            {
                tourCard.DataContext = tourCard.DataContext;
                tourCard.SetUser(_user);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
