using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using BookingApp.WPF.ViewModels.TouristViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TouristMainWindowViewModel
    {
        private readonly User _user;
        public TourRepository tourRepository { get; set; }
        public Tours ToursUserControl { get; set; }
        public TouristRepository touristRepository { get; set; }
        public TouristReservationRepository touristReservationRepository { get; set; }
        public TourStartTimeRepository tourStartTimeRepository { get; set; }
        public TouristDetails ToursDetailsUserControl { get; set; }
        public VisitedTours ToursVisitedUserControl { get; set; }
        public TourVoucherRepository tourVoucherRepository { get; set; }
        public TouristMainWindow touristMainWindow {  get; set; }
        public Frame TouristWindowFrame;

        public TouristMainWindowViewModel(User user, TouristMainWindow _touristMainWindow, Frame _touristWindowFrame)
        {
            touristMainWindow = _touristMainWindow;
            tourRepository = new TourRepository();
            _user = user;
            Update(_user);
            TouristWindowFrame = _touristWindowFrame;
            ToursUserControl = new Tours(user);
            TouristWindowFrame.Content = ToursUserControl;
            touristRepository = new TouristRepository();
            touristReservationRepository = new TouristReservationRepository();
            tourStartTimeRepository = new TourStartTimeRepository();
            tourVoucherRepository = new TourVoucherRepository();
        }


        public void ShowTourDetails(int tourId)
        {
            Tour detailedTour = tourRepository.GetById(tourId);
            TouristWindowFrame.Content = new TouristDetails(detailedTour, _user);
        }

        public void ShowTourDates(Tour tour, int guestNumber, List<Tourist> tourists, TourVoucher tourVoucher)
        {
            Tour detailedTour = tour;
            TouristWindowFrame.Content = new TourDatesUserControl(_user, detailedTour, guestNumber, tourists, touristRepository, touristReservationRepository, tourStartTimeRepository, tourVoucher, tourVoucherRepository);
        }
        public void ShowAlternativeTours(int locationId, Tour tour)
        {
            TouristWindowFrame.Content = new AlternativeTours(locationId, tour);
        }
        public void Update(User user)
        {
            ToursUserControl = new Tours(user);

        }

        public void MyTours_Click(object sender, RoutedEventArgs e)
        {
            TouristWindowFrame.Content = new VisitedTours(_user);
        }

        public void RateTour(User user, TouristReservationRepository touristReservationRepository, TourRepository tourRepository, TourReviewRepository tourReviewRepository, TourReviewImageRepository tourReviewImageRepository, int tourId)
        {
            TouristWindowFrame.Content = new RateTour(user, touristReservationRepository, tourRepository, tourReviewRepository, tourReviewImageRepository, tourId);
        }
        public void SeeCheckpoints(User user, int tourId)
        {
            TouristWindowFrame.Content = new Checkpoints(user, tourId);
        }

        public void Home_Click(object sender, MouseButtonEventArgs e)
        {
            TouristWindowFrame.Content = ToursUserControl;
        }

        public void MyVouchers_Click(object sender, RoutedEventArgs e)
        {
            TouristWindowFrame.Content = new TouristVouchers(_user.Id);
        }
        public void MyActiveTours_Click(object sender, RoutedEventArgs e)
        {
            TouristWindowFrame.Content = new TouristOngoingTours(_user);
        }

        /*internal void ShowTourDates(Tour selectedTour, int guestNumber, List<Tourist> tourists)
        {
            throw new NotImplementedException();
        }*/
    }
}
