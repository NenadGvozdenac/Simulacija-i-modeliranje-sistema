using BookingApp.Model.GuestModels;
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
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
        public AccommodationReservationMovingRepository _accommodationMovingRepository;

        public EventHandler ChangedMind;
        public RescheduleAccommodation(AccommodationReservation _selectedReservation, AccommodationRepository accommodationRepository)
        {
            InitializeComponent();
            selectedReservation = _selectedReservation;
            _accommodationRepository = accommodationRepository;
            _accommodationMovingRepository = new AccommodationReservationMovingRepository();
            SetUpUserControl();
        }

        private void SetUpUserControl()
        {
            Accommodation accommodation = _accommodationRepository.GetById(selectedReservation.AccommodationId);
            NameOfTheAccommodation_TextBlock.Text = accommodation.Name;
            AvailableDates temp = new AvailableDates(selectedReservation.FirstDateOfStaying, selectedReservation.LastDateOfStaying);
            OriginalCheckInDate_TextBlock.Text = "Original Check-In Date: " + temp.ToString();
            firstDate.DisplayDateStart = DateTime.Now;
            HideElements();
        }

        private void ChangedMind_Click(object sender, RoutedEventArgs e)
        {
            ChangedMind?.Invoke(this, EventArgs.Empty);
        }

        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            _accommodationMovingRepository.Add(new AccommodationReservationMoving(selectedReservation.AccommodationId, selectedReservation.Id, selectedReservation.UserId, selectedReservation.FirstDateOfStaying, selectedReservation.LastDateOfStaying, firstDate.SelectedDate.Value, lastDate.SelectedDate.Value)); 
        }

        private void DatePickerCantWrite(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void FirstDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? endDate = firstDate.SelectedDate;

            if (endDate.HasValue)
            {
                lastDate.DisplayDateStart = endDate.Value.AddDays(1);
                if (lastDate.SelectedDate.HasValue && lastDate.SelectedDate.Value < firstDate.SelectedDate.Value)
                {
                    lastDate.SelectedDate = null;
                    YesButton.IsEnabled = false;
                }
                else if (lastDate.SelectedDate.HasValue)
                    YesButton.IsEnabled = true;

                lastDate.IsEnabled = true;
            }
        }

        private void LastDateChanged(object sender, SelectionChangedEventArgs e)
        {
            YesButton.IsEnabled = true;
        }

        private void HideElements()
        {
            firstDate.Text = lastDate.Text = string.Empty;
            lastDate.IsEnabled = YesButton.IsEnabled = false;
        }
    }
}
