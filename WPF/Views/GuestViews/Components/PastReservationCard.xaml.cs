using BookingApp.WPF.DTOs.GuestDTOs;
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

namespace BookingApp.WPF.Views.GuestViews.Components;

/// <summary>
/// Interaction logic for PastReservationCard.xaml
/// </summary>
public partial class PastReservationCard : UserControl
{
    public event EventHandler<int> ReviewHandler;
    public PastReservationCard()
    {
        InitializeComponent();
    }

    private void Review_Click(object sender, RoutedEventArgs e)
    {
        if (this.DataContext is PastReservationsDTO reservation)
        {
            int reservationId = reservation.ReservationId;

            ReviewHandler?.Invoke(this, reservationId);
        }
    }
}
