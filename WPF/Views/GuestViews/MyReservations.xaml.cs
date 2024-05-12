using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuestViewModels;
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

namespace BookingApp.WPF.Views.GuestViews;

/// <summary>
/// Interaction logic for MyReservations.xaml
/// </summary>
public partial class MyReservations : UserControl
{
    public MyReservationsViewModel MyReservationsViewModel { get; set; }
    public MyReservations(User user)
    {
        InitializeComponent();
        MyReservationsViewModel = new MyReservationsViewModel(this, user);
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

    private void OwnerFeedback_Click(object sender, RoutedEventArgs e)
    {
        MyReservationsViewModel.OwnerFeedback_Click();
    }

    private void WhatIsSuperGuest_Click(object sender, RoutedEventArgs e)
    {
        MyReservationsViewModel.WhatIsSuperGuest_Click();
    }   
}
