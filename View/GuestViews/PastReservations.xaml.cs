using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository;
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
using BookingApp.Model.GuestModels;
using System.Collections.ObjectModel;
using BookingApp.Model.MutualModels;
using BookingApp.ViewModel.GuestViewModels;
using System.ComponentModel;

namespace BookingApp.View.GuestViews;

/// <summary>
/// Interaction logic for PastReservations.xaml
/// </summary>
public partial class PastReservations : UserControl
{
    public PastReservationsViewModel PastReservationsViewModel { get; set; }
    public PastReservations(User user, AccommodationRepository accommodationRepository, AccommodationReservationRepository accommodationReservationRepository)
    {
        InitializeComponent();
        PastReservationsViewModel = new PastReservationsViewModel(this, user, accommodationRepository, accommodationReservationRepository);
        DataContext = PastReservationsViewModel;
    }

    private void ReviewHandling(object sender, int reservationId)
    {
        PastReservationsViewModel.ReviewHandling(sender, reservationId);
    }
}
