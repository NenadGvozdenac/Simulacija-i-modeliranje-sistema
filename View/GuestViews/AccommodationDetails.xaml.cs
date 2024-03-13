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

namespace BookingApp.View.GuestViews;

public partial class AccommodationDetails : UserControl
{
    public Accommodation selectedAccommodation { get; set; }
    public AccommodationReservationRepository accomodationreservationrepository { get; set; }
    
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
        accomodationreservationrepository = new AccommodationReservationRepository();
        SetAccommodation(detailedaccomodation, user);
    }

    public void SetAccommodation(Accommodation accommodation, User user)
    {
        selectedAccommodation = accommodation;
        _user = user;
        username.Content = _user.Username;
        HideElements();
        SetDefaultValues(accommodation);
    }

    private void HideElements()
    {
        FreeDatesUserControl.Visibility = FreeDatesAlternativeUserControl.Visibility = Visibility.Collapsed;
        firstDate.Text = lastDate.Text = string.Empty;
        lastDate.IsEnabled = FreeDatesCheckButton.IsEnabled = false;
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

    //Opening user controls for final/possible reservation
    public void SetFreeDates(DateTime? firstDay, DateTime? lastDay)
    {
        FreeDatesCheckButton.IsEnabled = false;
        FreeDatesUserControl.accomodationReservationRepository = accomodationreservationrepository;
        FreeDatesUserControl.reservation.UserId = _user.Id;
        FreeDatesUserControl.reservation.AccommodationId = selectedAccommodation.Id;
        FreeDatesUserControl.reservation.FirstDateOfStaying = firstDay.Value;
        FreeDatesUserControl.reservation.LastDateOfStaying = lastDay.Value;
        FreeDatesUserControl.reservation.GuestsNumber = Convert.ToInt32(GuestNumber.Text);
        FreeDatesUserControl.ConfirmButton.IsEnabled = true;

        FreeDatesUserControl.first.Text = "First day: " + DateOnly.FromDateTime((DateTime)firstDay).ToString();
        FreeDatesUserControl.last.Text = "Last day: " + DateOnly.FromDateTime((DateTime)lastDay).ToString();

        FreeDatesUserControl.SuccessfullTextBox.Visibility = Visibility.Collapsed;
        FreeDatesAlternativeUserControl.Visibility = Visibility.Collapsed;
        FreeDatesUserControl.Visibility = Visibility.Visible;
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

        FreeDatesCheckButton.IsEnabled = false;
        FreeDatesAlternativeUserControl.accomodationReservationRepository = accomodationreservationrepository;
        FreeDatesAlternativeUserControl.reservation.UserId = _user.Id;
        FreeDatesAlternativeUserControl.reservation.AccommodationId = selectedAccommodation.Id;
        FreeDatesAlternativeUserControl.reservation.FirstDateOfStaying = firstAvailableDate.Value;
        FreeDatesAlternativeUserControl.reservation.LastDateOfStaying = lastAvailableDate.Value;
        FreeDatesAlternativeUserControl.reservation.GuestsNumber = Convert.ToInt32(GuestNumber.Text);
        FreeDatesAlternativeUserControl.ConfirmButton.IsEnabled = true;

        FreeDatesAlternativeUserControl.first.Text = "First day: " + DateOnly.FromDateTime((DateTime)firstAvailableDate).ToString();
        FreeDatesAlternativeUserControl.last.Text = "Last day: " + DateOnly.FromDateTime((DateTime)lastAvailableDate).ToString();

        FreeDatesAlternativeUserControl.SuccessfullTextBox.Visibility = Visibility.Collapsed;
        FreeDatesUserControl.Visibility = Visibility.Collapsed;
        FreeDatesAlternativeUserControl.Visibility = Visibility.Visible;
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
