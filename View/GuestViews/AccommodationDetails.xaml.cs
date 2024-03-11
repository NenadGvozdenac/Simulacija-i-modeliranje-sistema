﻿using BookingApp.Model.MutualModels;
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
    /// Interaction logic for AccommodationDetails.xaml
    /// </summary>
    public partial class AccommodationDetails : UserControl
    {
        int minvalueDaysOfStay = -1,
        maxvalueDaysOfStay = 100,
        startvalueDaysOfStay = 1;

        int minvalueGuestNumber = 1,
        maxvalueGuestNumber = 30,
        startvalueGuestNumber = 1;

        public Accommodation selectedAccommodation { get; set; }
        public AccommodationReservationRepository accomodationreservationrepository { get; set; }

        public User _user { get; set; }
        public AccommodationDetails()
        {
            InitializeComponent();
            accomodationreservationrepository = new AccommodationReservationRepository();
        }

        public void SetAccommodation(Accommodation accommodation)
        {
            selectedAccommodation = accommodation;
            
            freedates.Visibility = Visibility.Collapsed;
            freedatesalternative.Visibility = Visibility.Collapsed;
            firstDate.Text = string.Empty;
            lastDate.Text = string.Empty;
            lastDate.IsEnabled = false;
            freedatescheck.IsEnabled = false;
            minvalueDaysOfStay = accommodation.MinReservationDays;
            DaysOfStay.Text = accommodation.MinReservationDays.ToString();
            GuestNumber.Text = "1";
            MinDaysofStayTextBlock.Text = "Choose how long would you like to stay here (minimum of " + accommodation.MinReservationDays + " days):";
            MaxGuestsNumberTextBlock.Text = "Choose how many guests will there be (maximum " + accommodation.MaxGuestNumber + " ):";
            maxvalueGuestNumber = accommodation.MaxGuestNumber;

            accommodationName.Text = accommodation.Name;
            accomodationAverageReviewScore.Text = accommodation.AverageReviewScore.ToString() + "/10";
        }
       
        private void FirstFreeDatesCheck_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> takenDates = new List<DateTime>();
            takenDates = accomodationreservationrepository.FindTakenDates(selectedAccommodation.Id);

            DateTime? tempDate = firstDate.SelectedDate;
            int freeDaysInRowCounter = 0;
            DateTime? firstAvailableDate = null;
            DateTime? lastAvailableDate = null;
            while (tempDate != lastDate.SelectedDate.Value.AddDays(1))
            {
                if (freeDaysInRowCounter == 0) 
                {
                    firstAvailableDate = (DateTime)tempDate;
                }

                if (takenDates.Contains((DateTime)tempDate))
                {
                    freeDaysInRowCounter = 0;
                }
                else
                {
                    freeDaysInRowCounter++;
                }

                if (freeDaysInRowCounter == Convert.ToInt32(DaysOfStay.Text))
                {
                    lastAvailableDate = tempDate;
                    break;
                }
                tempDate = tempDate.Value.AddDays(1);

            }

            if (firstAvailableDate.HasValue && lastAvailableDate.HasValue)
            {
                SetFreeDates(firstAvailableDate, lastAvailableDate);
            }
            else
            {
                SetFreeDatesAlternative();
            }
        }

        public void SetFreeDates(DateTime? firstDay, DateTime? lastDay)
        {
            freedatescheck.IsEnabled = false;
            freedates.accomodationReservationRepository = accomodationreservationrepository;
            freedates.reservation.UserId = _user.Id;
            freedates.reservation.AccommodationId = selectedAccommodation.Id;
            freedates.reservation.FirstDateOfStaying = firstDay.Value;
            freedates.reservation.LastDateOfStaying = lastDay.Value;
            freedates.reservation.GuestsNumber = Convert.ToInt32(GuestNumber.Text);
            freedates.ConfirmButton.IsEnabled = true;

            freedates.first.Text = "First day: " + DateOnly.FromDateTime((DateTime)firstDay).ToString();
            freedates.last.Text = "Last day: " + DateOnly.FromDateTime((DateTime)lastDay).ToString();

            freedates.SuccessfullTextBox.Visibility = Visibility.Collapsed;
            freedatesalternative.Visibility = Visibility.Collapsed;
            freedates.Visibility = Visibility.Visible;
        }

        public void SetFreeDatesAlternative() 
        {
            List<DateTime> takenDates = new List<DateTime>();
            takenDates = accomodationreservationrepository.FindTakenDates(selectedAccommodation.Id);

            DateTime? tempDate = firstDate.SelectedDate;
            int freeDaysInRowCounter = 0;
            DateTime? firstAvailableDate = null;
            DateTime? lastAvailableDate = null;
            bool datesFound = false;

            while (datesFound == false)
            {
                if (freeDaysInRowCounter == 0)
                {
                    firstAvailableDate = (DateTime)tempDate;
                }

                if (takenDates.Contains((DateTime)tempDate))
                {
                    freeDaysInRowCounter = 0;
                }
                else
                {
                    freeDaysInRowCounter++;
                }

                if (freeDaysInRowCounter == Convert.ToInt32(DaysOfStay.Text))
                {
                    lastAvailableDate = tempDate;
                    datesFound = true;
                }
                tempDate = tempDate.Value.AddDays(1);
            }

            freedatescheck.IsEnabled = false;
            freedatesalternative.accomodationReservationRepository = accomodationreservationrepository;
            freedatesalternative.reservation.UserId = _user.Id;
            freedatesalternative.reservation.AccommodationId = selectedAccommodation.Id;
            freedatesalternative.reservation.FirstDateOfStaying = firstAvailableDate.Value;
            freedatesalternative.reservation.LastDateOfStaying = lastAvailableDate.Value;
            freedatesalternative.reservation.GuestsNumber = Convert.ToInt32(GuestNumber.Text);
            freedatesalternative.ConfirmButton.IsEnabled = true;

            freedatesalternative.first.Text = "First day: " + DateOnly.FromDateTime((DateTime)firstAvailableDate).ToString();
            freedatesalternative.last.Text = "Last day: " + DateOnly.FromDateTime((DateTime)lastAvailableDate).ToString();

            freedatesalternative.SuccessfullTextBox.Visibility = Visibility.Collapsed;
            freedates.Visibility = Visibility.Collapsed;
            freedatesalternative.Visibility = Visibility.Visible;
        }
        
        //Design Functions
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
                lastDate.IsEnabled = true;
            }
        }

        private void LastDateChanged(object sender, SelectionChangedEventArgs e)
        {
            freedatescheck.IsEnabled = true;
        }

        private void DaysOfStayUp_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (DaysOfStay.Text != "") number = Convert.ToInt32(DaysOfStay.Text);
            else number = 0;
            if (number < maxvalueDaysOfStay)
                DaysOfStay.Text = Convert.ToString(number + 1);
        }

        private void DaysOfStayDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (DaysOfStay.Text != "") number = Convert.ToInt32(DaysOfStay.Text);
            else number = 0;
            if (number > minvalueDaysOfStay)
                DaysOfStay.Text = Convert.ToString(number - 1);
        }

        private void DaysOfStay_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (DaysOfStay.Text != "")
                if (!int.TryParse(DaysOfStay.Text, out number)) DaysOfStay.Text = startvalueDaysOfStay.ToString();
            if (number > maxvalueDaysOfStay) DaysOfStay.Text = maxvalueDaysOfStay.ToString();
            if (number < minvalueDaysOfStay) DaysOfStay.Text = minvalueDaysOfStay.ToString();
            DaysOfStay.SelectionStart = DaysOfStay.Text.Length;
        }

        private void GuestNumberUp_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (GuestNumber.Text != "") number = Convert.ToInt32(GuestNumber.Text);
            else number = 0;
            if (number < maxvalueGuestNumber)
                GuestNumber.Text = Convert.ToString(number + 1);
        }

        private void GuestNumberDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (GuestNumber.Text != "") number = Convert.ToInt32(GuestNumber.Text);
            else number = 0;
            if (number > minvalueGuestNumber)
                GuestNumber.Text = Convert.ToString(number - 1);
        }

        private void GuestNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (GuestNumber.Text != "")
                if (!int.TryParse(GuestNumber.Text, out number)) GuestNumber.Text = startvalueGuestNumber.ToString();
            if (number > maxvalueGuestNumber) GuestNumber.Text = maxvalueGuestNumber.ToString();
            if (number < minvalueGuestNumber) GuestNumber.Text = minvalueGuestNumber.ToString();
            GuestNumber.SelectionStart = GuestNumber.Text.Length;
        }
    }
}