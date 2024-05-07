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
using BookingApp.Application.UseCases;

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
        public User _user { get; set; }

        public TouristOngoingTours touristOngoingTours { get; set; }
        public TouristOngoingToursViewModel(User user, TouristOngoingTours _touristOngoingTours)
        {
            touristOngoingTours = _touristOngoingTours;
            tours = new ObservableCollection<Tour>();
            _user = user;
            //Update();
        }

        public void Update()
        {
            tours.Clear();
            foreach (TouristReservation touristReservation in TourReservationService.GetInstance().GetAll())
            {
                if (touristReservation.UserId == _user.Id)
                {
                    foreach (TourStartTime tourStartTime in TourStartTimeService.GetInstance().GetAll())
                    {
                        if (tourStartTime.Id == touristReservation.Id_TourTime && tourStartTime.Status == "ongoing")
                        {
                            Tour tour = TourService.GetInstance().GetById(tourStartTime.TourId);
                            tour.Location = LocationService.GetInstance().GetById(tour.LocationId);
                            tour.Images = new(TourImageService.GetInstance().GetImagesByTourId(tour.Id));
                            tour.Language = LanguageService.GetInstance().GetById(tour.LanguageId);
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
