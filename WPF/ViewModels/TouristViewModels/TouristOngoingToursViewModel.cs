using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using BookingApp.WPF.Views.TouristViews.Components;
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
    public class TouristOngoingToursViewModel
    {
        public ObservableCollection<Tour> tours { get; set; }

        private ObservableCollection<Tour> _myTours;
        public ObservableCollection<Tour> MyActiveTours
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

        public TouristOngoingTours touristOngoingTours { get; set; }
        public TouristOngoingToursViewModel(User user, TouristOngoingTours _touristOngoingTours)
        {
            touristOngoingTours = _touristOngoingTours;
            touristReservationRepository = new TouristReservationRepository();
            tourStartTimeRepository = new TourStartTimeRepository();
            tourRepository = new TourRepository();
            languageRepository = new LanguageRepository();
            locationRepository = new LocationRepository();
            tourImageRepository = new TourImageRepository();
            tours = new ObservableCollection<Tour>();
            _user = user;
            //Update();
        }

        public void Update()
        {
            tours.Clear();
            foreach (TouristReservation touristReservation in touristReservationRepository.GetAll())
            {
                if (touristReservation.UserId == _user.Id)
                {
                    foreach (TourStartTime tourStartTime in tourStartTimeRepository.GetAll())
                    {
                        if (tourStartTime.Id == touristReservation.Id_TourTime && tourStartTime.Status == "ongoing")
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
            MyActiveTours = new ObservableCollection<Tour>(tours);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Ongoing_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is OngoingTourCard tourCard)
            {
                tourCard.DataContext = tourCard.DataContext;
                tourCard.SetUser(_user);
            }
        }
    }
}
