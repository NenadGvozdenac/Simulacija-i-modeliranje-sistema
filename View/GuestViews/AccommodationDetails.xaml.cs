using BookingApp.Model.GuestModels;
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace BookingApp.View.GuestViews;

public partial class AccommodationDetails : UserControl
{
    public Accommodation selectedAccommodation { get; set; }
    public AccommodationReservationRepository accomodationreservationrepository { get; set; }
    
    public AccommodationReservation reservation;
    public ObservableCollection<string> _availableDates;
    public string SelectedDate {  get; set; }

    int minvalueDaysOfStay = -1,
    maxvalueDaysOfStay = 100,
    startvalueDaysOfStay = 1;

    int minvalueGuestNumber = 1,
    maxvalueGuestNumber = 30,
    startvalueGuestNumber = 1;
    public User _user { get; set; }
    public AccommodationDetails(Accommodation detailedaccomodation, User user)
    {
        InitializeComponent();
        DataContext = this;
        SetAccommodation(detailedaccomodation, user);
    }

    public void SetAccommodation(Accommodation accommodation, User user)
    {
        _availableDates = new ObservableCollection<string>();
        accomodationreservationrepository = new AccommodationReservationRepository();
        reservation = new AccommodationReservation();
        selectedAccommodation = accommodation;
        _user = user;
        username.Content = _user.Username;
        HideElements();
        SetDefaultValues(accommodation);
    }

    private void HideElements()
    {
        firstDate.Text = lastDate.Text = string.Empty;
        lastDate.IsEnabled = FreeDatesCheckButton.IsEnabled = false;
        ConfirmButton.IsEnabled = false;
    }

    private void SetDefaultValues(Accommodation accommodation)
    {
        firstDate.DisplayDateStart = DateTime.Now;
        minvalueDaysOfStay = accommodation.MinReservationDays;
        DaysOfStay.Text = accommodation.MinReservationDays.ToString();
        GuestNumber.Text = "1";
        MinDaysofStayTextBlock.Text = $"Choose how long would you like to stay here (minimum of {accommodation.MinReservationDays} days):";
        MaxGuestsNumberTextBlock.Text = $"Choose how many guests will there be (maximum {accommodation.MaxGuestNumber}):";
        maxvalueGuestNumber = accommodation.MaxGuestNumber;
        accommodationName.Text = accommodation.Name;
        accomodationAverageReviewScore.Text = $"{accommodation.AverageReviewScore}/10";
    }

    private void FreeDatesCheck_Click(object sender, RoutedEventArgs e)
    {
        List<DateTime> takenDates = new List<DateTime>();
        takenDates = accomodationreservationrepository.FindTakenDates(selectedAccommodation.Id);

        _availableDates.Clear();
        DateTime? whileDate = firstDate.SelectedDate;
        DateTime? firstAvailableDate = null;
        DateTime? lastAvailableDate = null;
        int freeDaysInRowCounter = 0;

        while (whileDate != lastDate.SelectedDate.Value.AddDays(1))
        {
            freeDaysInRowCounter = 0;
            DateTime tempDate = whileDate.Value;
            
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
                    if (firstAvailableDate.HasValue && lastAvailableDate.HasValue)
                    {
                        AvailableDates available = new AvailableDates((DateTime)firstAvailableDate, (DateTime)lastAvailableDate);
                        if (!_availableDates.Contains(available.ToString())){
                            _availableDates.Add(available.ToString());
                        }
                    }
                    freeDaysInRowCounter = 0;
                    break;
                }
                tempDate = tempDate.AddDays(1);
            }
            whileDate = whileDate.Value.AddDays(1);            
        }
        if (_availableDates.Count > 0) {
            FoundDatesTextBox.Visibility = Visibility.Visible;
            FoundAlternativeDatesTextBox.Visibility = Visibility.Collapsed;
            availableDatesListBox.ItemsSource = _availableDates;
            ConfirmButton.IsEnabled = true;
        }
        else
        {
            FreeAlternativeDates();
        }
        FreeDatesCheckButton.IsEnabled = false;
    }

    private void FreeAlternativeDates()
    {
        List<DateTime> takenDates = new List<DateTime>();
        takenDates = accomodationreservationrepository.FindTakenDates(selectedAccommodation.Id);

        _availableDates.Clear();
        //DateTime? whileDate = lastDate.SelectedDate.Value.AddDays(1);
        DateTime? whileDate = firstDate.SelectedDate.Value;
        DateTime? firstAvailableDate = null;
        DateTime? lastAvailableDate = null;
        int freeDaysInRowCounter = 0;
        int foundAvailableDates = 0;

        while (foundAvailableDates!=5)
        {
            freeDaysInRowCounter = 0;
            DateTime tempDate = whileDate.Value;
            while (tempDate != tempDate.AddDays(Convert.ToInt32(DaysOfStay.Text)))
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
                    if (firstAvailableDate.HasValue && lastAvailableDate.HasValue)
                    {
                        AvailableDates available = new AvailableDates((DateTime)firstAvailableDate, (DateTime)lastAvailableDate);
                        if (!_availableDates.Contains(available.ToString()))
                        {
                            _availableDates.Add(available.ToString());
                            foundAvailableDates++;
                        }
                    }
                    freeDaysInRowCounter = 0;
                    break;
                }
                tempDate = tempDate.AddDays(1);
            }
            whileDate = whileDate.Value.AddDays(1);
        }
        
        FoundDatesTextBox.Visibility = Visibility.Collapsed;
        FoundAlternativeDatesTextBox.Visibility = Visibility.Visible;
        availableDatesListBox.ItemsSource = _availableDates;
        ConfirmButton.IsEnabled = true;
    }

    private void ConfrimReservation_Click(object sender, RoutedEventArgs e)
    {
        string regex = @"(\d{2}-[A-Za-z]{3}-\d{2})\s-\s(\d{2}-[A-Za-z]{3}-\d{2})";
        Match match = Regex.Match(SelectedDate, regex);

        DateTime firstDate;
        DateTime lastDate;

        if (match.Success)
        {
            string startDateString = match.Groups[1].Value;
            string endDateString = match.Groups[2].Value;

            firstDate = DateTime.ParseExact(startDateString, "dd-MMM-yy", null);
            lastDate = DateTime.ParseExact(endDateString, "dd-MMM-yy", null);

            reservation = new AccommodationReservation(_user.Id, selectedAccommodation.Id, Convert.ToInt32(GuestNumber.Text), firstDate, lastDate);

            accomodationreservationrepository.Add(reservation);
            ConfirmButton.IsEnabled = false;
            ConfirmedReservationTextBox.Visibility = Visibility.Visible;
        }      
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
            lastDate.SelectedDate = null;
            lastDate.IsEnabled = true;
        }
    }

    private void LastDateChanged(object sender, SelectionChangedEventArgs e)
    {
        FreeDatesCheckButton.IsEnabled = true;
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
