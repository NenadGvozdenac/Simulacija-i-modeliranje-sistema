﻿using BookingApp.Model.GuestModels;
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.View.GuestViews.Components;
using BookingApp.ViewModel.GuestViewModels;
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

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for UpcomingReservations.xaml
    /// </summary>
    public partial class UpcomingReservations : UserControl
    {

        public event EventHandler<int> RescheduleClicked;
        public event EventHandler CancelClicked;

        private User _user;
        public AccommodationRepository _accommodationRepository;
        public AccommodationReservationRepository _accommodationReservationRepository;
        public AccommodationImageRepository _accommodationImageRepository { get; set; }
        public LocationRepository _locationRepository { get; set; }
        public ObservableCollection<UpcomingReservationsDTO> _upcomingReservations { get; set; }
        public UpcomingReservationsViewModel UpcomingReservationsViewModel { get; set; }

        public UpcomingReservations(User user, AccommodationRepository accommodationRepository, AccommodationReservationRepository accommodationReservationRepository)
        {
            InitializeComponent();            
            UpcomingReservationsViewModel = new UpcomingReservationsViewModel(this, user, accommodationRepository ,accommodationReservationRepository);
            DataContext = UpcomingReservationsViewModel;
        }
        private void UpcomingReservationsCard_RescheduleClicked(object sender, int reservationId)
        {
            UpcomingReservationsViewModel.UpcomingReservationsCard_RescheduleClicked(sender, reservationId);
        }

        private void UpcomingReservationsCard_CancelClicked(object sender, int reservationId)
        {
            UpcomingReservationsViewModel.UpcomingReservationsCard_CancelClicked(sender, reservationId);
        }
    }
}
