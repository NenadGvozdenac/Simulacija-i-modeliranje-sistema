﻿using System;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using BookingApp.View.PathfinderViews.Componentss;
using BookingApp.View.TouristViews.Components;
using BookingApp.Domain.Models;
using BookingApp.Repositories;

namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for VisitedTours.xaml
    /// </summary>
    public partial class VisitedTours : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Tour> tours { get; set; }

        private ObservableCollection<Tour> _myTours;
        public ObservableCollection<Tour> MyTours
        {
            get { return _myTours; }
            set {
                if (_myTours != value)
                {
                    _myTours = value;
                    OnPropertyChanged();
                }
            }
        }
        public TouristReservationRepository touristReservationRepository { get; set; }
        public TourStartTimeRepository tourStartTimeRepository {  get; set; }
        public LocationRepository locationRepository {  get; set; }
        public TourImageRepository tourImageRepository { get; set; }
        public LanguageRepository languageRepository {  get; set; }
        public TourRepository tourRepository { get; set; }
        public User _user { get; set; }
        public VisitedTours(User user)
        {
            InitializeComponent();
            DataContext = this;
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
            foreach(TouristReservation touristReservation in touristReservationRepository.GetAll())
            {
                if(touristReservation.UserId == _user.Id && touristReservation.CheckpointId!=-1)
                {
                    foreach(TourStartTime tourStartTime in tourStartTimeRepository.GetAll())
                    {
                        if(tourStartTime.Id == touristReservation.Id_TourTime)
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

        private void TourCardWithStar_Loaded(object sender, RoutedEventArgs e)
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