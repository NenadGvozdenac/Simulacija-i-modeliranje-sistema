﻿using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using System;
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

namespace BookingApp.View.OwnerViews.Components
{
    /// <summary>
    /// Interaction logic for GuestRatingControl.xaml
    /// </summary>
    public partial class GuestRatingControl : UserControl
    {
        private GuestRating _guestRating;

        private UserRepository _userRepository;
        private AccommodationRepository _accommodationRepository;
        private LocationRepository _locationRepository;
        private User _user;
        public GuestRatingControl(User user, GuestRating guestRating)
        {
            _guestRating = guestRating;
            _user = user;
            _userRepository = UserRepository.GetInstance();
            _accommodationRepository = AccommodationRepository.GetInstance();
            _locationRepository = LocationRepository.GetInstance();

            InitializeComponent();
            SetupUserControl();
        }

        private void SetupUserControl()
        {
            UserName.Content = _userRepository.GetById(_guestRating.GuestId).Username;
            AccommodationName.Content = _accommodationRepository.GetById(_guestRating.AccommodationId).Name;
            AccommodationLocation.Content = _locationRepository.GetById(_accommodationRepository.GetById(_guestRating.AccommodationId).LocationId);
            ReservationTimespan.Content = string.Format("{0} - {1}", _guestRating.Reservation.FirstDateOfStaying.ToShortDateString(), _guestRating.Reservation.LastDateOfStaying.ToShortDateString());
        }

        private void EyeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            User guestUser = _userRepository.GetById(_guestRating.GuestId);
            DetailedGuestReviewPage detailedGuestReviewPage = new DetailedGuestReviewPage(guestUser, _guestRating, _accommodationRepository.GetById(_guestRating.AccommodationId));
            NavigationService.GetNavigationService(this).Navigate(detailedGuestReviewPage);
        }
    }
}
