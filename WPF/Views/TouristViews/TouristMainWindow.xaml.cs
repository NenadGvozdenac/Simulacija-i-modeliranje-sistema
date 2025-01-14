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
using System.Windows.Shapes;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.TouristViewModels;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window
    {
        TouristMainWindowViewModel touristMainWindowViewModel {  get; set; }
        public TouristMainWindow(User user)
        {
            InitializeComponent();
            touristMainWindowViewModel = new TouristMainWindowViewModel(user, this, TouristWindowFrame);
            DataContext = touristMainWindowViewModel;
            Update(user);
        }
        public void ShowMainWindow()
        {
            touristMainWindowViewModel.ShowMainWindow();
        }
        public void TourReservation(Tour tour, User user)
        {
            touristMainWindowViewModel.TourReservation(tour, user);
        }
        public void ShowTourDetails(int tourId)
        {
            touristMainWindowViewModel.ShowTourDetails(tourId);
        }

        public void ShowRequestsOnComplexTour(int requestId)
        {
            touristMainWindowViewModel.ShowRequestsOnComplexTour(requestId);
        }
        public void ShowReservationReview(User user, Tour tour, int guestNumber, List<Tourist> tourists, TourVoucher voucher, TourStartTime tourStartTime)
        {
            touristMainWindowViewModel.ShowReservationReview(user, tour, guestNumber, tourists, voucher, tourStartTime);
        }

        public void ShowComplexTourRequest(User user)
        {
            touristMainWindowViewModel.ShowComplexTourRequest(user);
        }
        public void ShowTourDates(Tour tour, int guestNumber, List<Tourist> tourists, TourVoucher tourVoucher)
        {
            touristMainWindowViewModel.ShowTourDates(tour, guestNumber, tourists, tourVoucher);
        }
        public void ShowAlternativeTours(int locationId, Tour tour)
        {
            touristMainWindowViewModel.ShowAlternativeTours(locationId, tour);
        }
        public void Update(User user)
        {
            touristMainWindowViewModel.Update(user);

        }

        public void MyTours_Click(object sender, RoutedEventArgs e)
        {
            touristMainWindowViewModel.MyTours_Click(sender, e);
        }

        public void RateTour(User user, int tourId)
        {
            touristMainWindowViewModel.RateTour(user, tourId);
        }

        public void Home_Click(object sender, MouseButtonEventArgs e)
        {
            touristMainWindowViewModel.Home_Click(sender, e);
        }

        public void MyVouchers_Click(object sender, RoutedEventArgs e)
        {
            touristMainWindowViewModel.MyVouchers_Click(sender, e);
        }

        public void MyActiveTours_Click(object sender, RoutedEventArgs e)
        {
            touristMainWindowViewModel.MyActiveTours_Click(sender, e);
        }
        public void SeeCheckpoints(User user, int tourId)
        {
            touristMainWindowViewModel.SeeCheckpoints(user, tourId);
        }

        public void Notifications_Click(object sender, MouseButtonEventArgs e)
        {
            touristMainWindowViewModel.Notifications_Click(sender, e);
        }
        public void Requests_Click(object sender, RoutedEventArgs e)
        {
            touristMainWindowViewModel.Requests_Click(sender, e);
        }
        public void AddRequest(User user)
        {
            touristMainWindowViewModel.AddRequest(user);
        }

        public void ComplexRequests_Click(object sender, RoutedEventArgs e)
        {
            touristMainWindowViewModel.ComplexRequests_Click(sender, e);
        }
        public void ShowTourRequestStatistics()
        {
            touristMainWindowViewModel.ShowTourRequestStatistics();
        }

        /*internal void ShowTourDates(Tour selectedTour, int guestNumber, List<Tourist> tourists)
        {
            touristMainWindowViewModel.ShowTourDates(selectedTour, guestNumber, tourists);
        }*/
    }
}
