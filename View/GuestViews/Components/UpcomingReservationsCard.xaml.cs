using BookingApp.Model.GuestModels;
using BookingApp.Model.MutualModels;
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
        public UpcomingReservationsCard()
        {
            InitializeComponent();
        }

        private void Reschedule_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is UpcomingReservationsDTO reservation)
            {
                int reservationId = reservation.ReservationId;

                RescheduleClicked?.Invoke(this, reservationId);
            }
        }
    }
}
