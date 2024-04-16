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
        public User _user { get; set; }
        public VisitedTours visitedTours { get; set; }
        public VisitedToursViewModel(User user, VisitedTours _visitedTours)
        {
            visitedTours = _visitedTours;
            tours = new ObservableCollection<Tour>();
            _user = user;
            Update();

        }

        public void Update()
        {
            tours.Clear();
            foreach (TouristReservation touristReservation in TourReservationService.GetInstance().GetAll())
            {
                if (touristReservation.UserId == _user.Id && touristReservation.CheckpointId != -1)
                {
                    foreach (TourStartTime tourStartTime in TourStartTimeService.GetInstance().GetAll())
                    {
                        if (tourStartTime.Id == touristReservation.Id_TourTime)
                        {
                            Tour tour = TourService.GetInstance().GetById(tourStartTime.TourId);
                            tour.Location = LocationService.GetInstance().GetById(tour.LocationId);
                            tour.Images = TourImageService.GetInstance().GetImagesByTourId(tour.Id);
                            tour.Language = LanguageService.GetInstance().GetById(tour.LanguageId);
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
