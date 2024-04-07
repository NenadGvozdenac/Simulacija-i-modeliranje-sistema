﻿using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.ViewModel.GuestViewModels;
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

namespace BookingApp.View.GuestViews;

/// <summary>
/// Interaction logic for MyReservations.xaml
/// </summary>
public partial class MyReservations : UserControl
{
    public MyReservationsViewModel MyReservationsViewModel { get; set; }

    public MyReservations(User user, AccommodationRepository accommodationRepository , AccommodationReservationRepository accommodationReservationRepository, AccommodationReservationMovingRepository accommodationReservationMovingRepository)
    {
        InitializeComponent();
        MyReservationsViewModel = new MyReservationsViewModel(this, user, accommodationRepository, accommodationReservationRepository, accommodationReservationMovingRepository);
        DataContext = MyReservationsViewModel;
    }

    private void UpcomingReservations_Click(object sender, RoutedEventArgs e)
    {
        MyReservationsViewModel.UpcomingReservations_Click();
    }

    private void PastReservations_Click(object sender, RoutedEventArgs e)
    {
        MyReservationsViewModel.PastReservations_Click();
    }

    private void RescheduleRequests_Click(object sender, RoutedEventArgs e)
    {
        MyReservationsViewModel.RescheduleRequests_Click();
    }
}
