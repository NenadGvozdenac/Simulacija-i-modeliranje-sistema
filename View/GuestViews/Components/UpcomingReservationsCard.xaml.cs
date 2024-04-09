using BookingApp.Model.GuestModels;
using BookingApp.Domain.Models;
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

namespace BookingApp.View.GuestViews.Components
{
    /// <summary>
    /// Interaction logic for UpcomingReservationsCard.xaml
    /// </summary>
    public partial class UpcomingReservationsCard : UserControl
    {
        public event EventHandler<int> RescheduleClicked;
        public event EventHandler<int> CancelClicked;
        public UpcomingReservationsCard()
        {
            InitializeComponent();
        }

        private void UpdateButtonState(object sender, DependencyPropertyChangedEventArgs e)
        {

            UpcomingReservationsDTO reservation = (UpcomingReservationsDTO)DataContext;
                
                if (reservation.RemainingDaysToCancel <= 0)
                {
                    CancelReservationButton.IsEnabled = false;
                    CancelReservationButton.Content = "Can't cancel the reservation";
                    RemainingDays_TextBlock.Text = "Remaining days to cancel: 0";
                }
                else
                {
                    CancelReservationButton.IsEnabled = true;
                    CancelReservationButton.Content = "Tap here to cancel the reservation";
                }

        }
        private void Reschedule_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is UpcomingReservationsDTO reservation)
            {
                int reservationId = reservation.ReservationId;

                RescheduleClicked?.Invoke(this, reservationId);
            }
        }

        private void CancelReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is UpcomingReservationsDTO reservation)
            {
                int reservationId = reservation.ReservationId;

                CancelClicked?.Invoke(this, reservationId);
            }
        }
    }
}
