using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using System;
using System.Collections.Generic;
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

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class ReservationRescheduling : UserControl
{
    public event EventHandler<AccommodationReservationMoving> ReservationReschedulingDetails;
    public ReservationReschedulingCardViewModel ReservationReschedulingCardViewModel { get; set; }
    public ReservationRescheduling(AccommodationReservationMoving accommodationReservationMoving)
    {
        InitializeComponent();
        ReservationReschedulingCardViewModel = new ReservationReschedulingCardViewModel(accommodationReservationMoving);
        DataContext = ReservationReschedulingCardViewModel;
    }

    private void ReservationRescheduling_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ReservationReschedulingDetails.Invoke(this, ReservationReschedulingCardViewModel.AccommodationReservationMoving);
    }  
}
