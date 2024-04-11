using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Resources.Types;
using BookingApp.WPF.ViewModels.GuestViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
public partial class RescheduleRequests : UserControl
{
    public RescheduleRequestsViewModel RescheduleRequestsViewModel { get; set; }
    public RescheduleRequests(User user, AccommodationRepository accommodationRepository, AccommodationReservationMovingRepository accommodationReservationMovingRepository)
    {
        InitializeComponent();
        RescheduleRequestsViewModel = new RescheduleRequestsViewModel(this, user, accommodationRepository, accommodationReservationMovingRepository);
        DataContext = RescheduleRequestsViewModel;
    }
}
