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
using System.Collections.ObjectModel;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuestViewModels;

namespace BookingApp.WPF.Views.GuestViews;

/// <summary>
/// Interaction logic for PastReservations.xaml
/// </summary>
public partial class PastReservations : UserControl
{
    public PastReservationsViewModel PastReservationsViewModel { get; set; }
    public PastReservations(User user)
    {
        InitializeComponent();
        PastReservationsViewModel = new PastReservationsViewModel(this, user);
        DataContext = PastReservationsViewModel;
    }

    private void ReviewHandling(object sender, int reservationId)
    {
        PastReservationsViewModel.ReviewHandling(sender, reservationId);
    }
}