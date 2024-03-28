using BookingApp.Model.GuestModels;
using BookingApp.Model.MutualModels;
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

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for RescheduleAccommodation.xaml
    /// </summary>
    public partial class RescheduleAccommodation : UserControl
    {

        public AccommodationReservation selectedReservation;
        public AccommodationRepository _accommodationRepository;

        public EventHandler ChangedMind;
        public RescheduleAccommodation(AccommodationReservation _selectedReservation, AccommodationRepository accommodationRepository)
        {
            InitializeComponent();
            selectedReservation = _selectedReservation;
            _accommodationRepository = accommodationRepository;
            Update();
        }

        private void Update()
        {
            Accommodation accommodation = _accommodationRepository.GetById(selectedReservation.AccommodationId);
            NameOfTheAccommodation_TextBlock.Text = accommodation.Name;
            AvailableDates temp = new AvailableDates(selectedReservation.FirstDateOfStaying, selectedReservation.LastDateOfStaying);
            OriginalCheckInDate_TextBlock.Text = "Original Check-In Date: " + temp.ToString();
        }

        private void ChangedMind_Click(object sender, RoutedEventArgs e)
        {
            ChangedMind?.Invoke(this, EventArgs.Empty);
        }
    }
}
