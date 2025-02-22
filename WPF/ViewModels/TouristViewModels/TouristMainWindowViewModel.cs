﻿using BookingApp.Domain.Models;
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
using BookingApp.Application.UseCases;
using System.Security.RightsManagement;
using System.Windows.Media;
using LiveCharts.Dtos;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TouristMainWindowViewModel
    {
        private readonly User _user;
        public Tours ToursUserControl { get; set; }
        public TouristDetails ToursDetailsUserControl { get; set; }
        public VisitedTours ToursVisitedUserControl { get; set; }
        public TouristMainWindow touristMainWindow {  get; set; }
        public Frame TouristWindowFrame;

        public TouristMainWindowViewModel(User user, TouristMainWindow _touristMainWindow, Frame _touristWindowFrame)
        {
            touristMainWindow = _touristMainWindow;
            _user = user;
            Update(_user);
            TouristWindowFrame = _touristWindowFrame;
            ToursUserControl = new Tours(user);
            TouristWindowFrame.Content = ToursUserControl;
            touristMainWindow.HomeRectangle.Fill = Brushes.LightGray;
        }

        private void ResetButtonStyles()
        {
            touristMainWindow.MyToursButton.Style = (Style)touristMainWindow.FindResource("DefaultButtonStyle");
            touristMainWindow.MyVouchersButton.Style = (Style)touristMainWindow.FindResource("DefaultButtonStyle");
            touristMainWindow.MyActiveToursButton.Style = (Style)touristMainWindow.FindResource("DefaultButtonStyle");
            touristMainWindow.RequestsButton.Style = (Style)touristMainWindow.FindResource("DefaultButtonStyle");
            touristMainWindow.ComplexRequestsButton.Style = (Style)touristMainWindow.FindResource("DefaultButtonStyle");

            touristMainWindow.HomeRectangle.Fill = Brushes.Transparent;

        }
        public void CheckVouchers()
        {
            
        }
        public void ShowMainWindow()
        {
            TouristWindowFrame.Content = ToursUserControl;
        }
        public void ShowReservationReview(User user, Tour tour, int guestNumber, List<Tourist> tourists, TourVoucher voucher, TourStartTime tourStartTime)
        {
            TouristWindowFrame.Content = new ReservationReview(user, tour, guestNumber, tourists, voucher, tourStartTime);
        }

        public void ShowComplexTourRequest(User user)
        {
            TouristWindowFrame.Content = new RequestComplexTour(_user);
        }
        public void ShowTourDetails(int tourId)
        {
            Tour detailedTour = TourService.GetInstance().GetById(tourId);
            TouristWindowFrame.Content = new TouristDetails(detailedTour, _user);
        }

        public void ShowRequestsOnComplexTour(int requestId)
        {
            TouristWindowFrame.Content = new RequestsOnComplexTourRequest(_user, requestId);
        }
        public void ShowTourRequestStatistics()
        {
            TouristWindowFrame.Content = new TourRequestStatistics();
        }
        public void TourReservation(Tour tour, User user)
        {
            TouristWindowFrame.Content = new TourReservation(tour, user);
        }
        public void ShowTourDates(Tour tour, int guestNumber, List<Tourist> tourists, TourVoucher tourVoucher)
        {
            Tour detailedTour = tour;
            TouristWindowFrame.Content = new TourDatesUserControl(_user, detailedTour, guestNumber, tourists, tourVoucher);
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
            ResetButtonStyles();
            touristMainWindow.MyToursButton.Style = (Style)touristMainWindow.FindResource("ActiveButtonStyle");
            TouristWindowFrame.Content = new VisitedTours(_user);
        }

        public void RateTour(User user, int tourId)
        {
            TouristWindowFrame.Content = new RateTour(user, tourId);
        }
        public void SeeCheckpoints(User user, int tourId)
        {
            TouristWindowFrame.Content = new Checkpoints(user, tourId);
        }

        public void Home_Click(object sender, MouseButtonEventArgs e)
        {
            ResetButtonStyles();
            touristMainWindow.HomeRectangle.Fill = Brushes.LightGray;
            TouristWindowFrame.Content = ToursUserControl;
        }

        public void MyVouchers_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonStyles();
            touristMainWindow.MyVouchersButton.Style = (Style)touristMainWindow.FindResource("ActiveButtonStyle");
            TouristWindowFrame.Content = new TouristVouchers(_user.Id);
        }
        public void MyActiveTours_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonStyles();
            touristMainWindow.MyActiveToursButton.Style = (Style)touristMainWindow.FindResource("ActiveButtonStyle");
            TouristWindowFrame.Content = new TouristOngoingTours(_user);
        }

        public void Notifications_Click(object sender, MouseButtonEventArgs e)
        {
            TouristWindowFrame.Content = new TouristNotifications(_user);
        }

        public void Requests_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonStyles();
            touristMainWindow.RequestsButton.Style = (Style)touristMainWindow.FindResource("ActiveButtonStyle");
            TouristWindowFrame.Content = new TourRequests(_user);
        }

        public void ComplexRequests_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonStyles();
            touristMainWindow.ComplexRequestsButton.Style = (Style)touristMainWindow.FindResource("ActiveButtonStyle");
            TouristWindowFrame.Content = new ComplexTourRequests(_user);
        }
        public void AddRequest(User user)
        {
            TouristWindowFrame.Content = new RequestTour(_user);
        }

        /*internal void ShowTourDates(Tour selectedTour, int guestNumber, List<Tourist> tourists)
        {
            throw new NotImplementedException();
        }*/
    }
}
