using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuestViewModels;
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

namespace BookingApp.WPF.Views.GuestViews;

/// <summary>
/// Interaction logic for UpcomingReservations.xaml
/// </summary>
public partial class UpcomingReservations : UserControl
{
    public UpcomingReservationsViewModel UpcomingReservationsViewModel { get; set; }

    public UpcomingReservations(User user)
    {
        InitializeComponent();            
        UpcomingReservationsViewModel = new UpcomingReservationsViewModel(this, user);
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
