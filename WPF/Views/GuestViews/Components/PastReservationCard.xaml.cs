using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
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

public partial class PastReservationCard : UserControl
{
    public event EventHandler<int> ReviewHandler;
    public PastReservationCard()
    {
        InitializeComponent();
    }
    private void UpdateButtonState(object sender, DependencyPropertyChangedEventArgs e)
    {

        PastReservationsDTO reservation = (PastReservationsDTO)DataContext;

        if (reservation.DaysSinceLastDateOfStaying > 5)
        {
            ReviewButton.IsEnabled = false;
            ReviewButton.Content = "Can't leave a review";
            RemainingDays_TextBlock.Text = "Remaining days to leave a review: 0";
        }
        else
        {
            ReviewButton.IsEnabled = true;
            ReviewButton.Content = "Tap here to leave a review";
            RemainingDays_TextBlock.Text = "Remaining days to leave a review: " + (5 - reservation.DaysSinceLastDateOfStaying);
        }

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
