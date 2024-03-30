using BookingApp.Miscellaneous;
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
    public AccommodationReviewRepository _accommodationReviewRepository;
    
    public AccommodationReservation reservation;
    public ObservableCollection<string> _availableDates;
    public ObservableCollection<AccommodationReview> _reviews { get; set; }

    public event EventHandler UpcomingReservationsChanged;
    public string SelectedDate {  get; set; }

    int minvalueDaysOfStay = -1,
    maxvalueDaysOfStay = 100,
    startvalueDaysOfStay = 1;

    int minvalueGuestNumber = 1,
    maxvalueGuestNumber = 30,
    startvalueGuestNumber = 1;
    public User _user { get; set; }
    public AccommodationDetails(Accommodation detailedaccomodation, User user,  AccommodationReviewRepository accommodationReviewRepository)
    {
        InitializeComponent();
        DataContext = this;
        _reviews = new ObservableCollection<AccommodationReview>();
        _accommodationReviewRepository = accommodationReviewRepository;
        SetAccommodation(detailedaccomodation, user);
    }

    public void UpcomingReservationsChanged_Invoke()
    {
        UpcomingReservationsChanged.Invoke(this, EventArgs.Empty);
    }

    public void SetAccommodation(Accommodation accommodation, User user)
    {
        _availableDates = new ObservableCollection<string>();
        accomodationreservationrepository = AccommodationReservationRepository.GetInstance();
        reservation = new AccommodationReservation();
        selectedAccommodation = accommodation;
        _user = user;
        username.Content = _user.Username;
        HideElements();
        SetDefaultValues(accommodation);
        LoadReviews();
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

    private void LoadReviews()
    {
        _reviews.Clear();
        foreach (AccommodationReview review in _accommodationReviewRepository.GetAll())
        {
            if (review.AccommodationId == selectedAccommodation.Id)
            {
                _reviews.Add(review);
            }
        }
    }

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        (Window.GetWindow(this) as GuestMainWindow).Accommodations_Click(sender,e);
    }
    private void RepeatCheck(AvailableDates available)
    {
        if (!_availableDates.Contains(available.ToString()))
        {
            _availableDates.Add(available.ToString());
        }
    }

    private int RepeatCheckAlternative(AvailableDates available, int foundAvailableDates)
    {
        if (!_availableDates.Contains(available.ToString()))
        {
            _availableDates.Add(available.ToString());
            foundAvailableDates++;
        }

        return foundAvailableDates;
    }
    private void FreeDatesCheck_Click(object sender, RoutedEventArgs e)
    {
        List<DateTime> takenDates = accomodationreservationrepository.FindTakenDates(selectedAccommodation.Id);

        _availableDates.Clear();
        DateTime? whileDate = firstDate.SelectedDate;
        DateTime? firstAvailableDate = null;

        while (whileDate != lastDate.SelectedDate.Value.AddDays(1))
        {
            int freeDaysInRowCounter = 0;
            DateTime tempDate = whileDate.Value;
            
            while (tempDate != lastDate.SelectedDate.Value.AddDays(1))
            {
                if (freeDaysInRowCounter == 0)
                    firstAvailableDate = (DateTime)tempDate;

                freeDaysInRowCounter = DateContains(takenDates, tempDate, freeDaysInRowCounter);

                if (freeDaysInRowCounter == Convert.ToInt32(DaysOfStay.Text))
                {
                    SaveFreeDate(tempDate, firstAvailableDate);
                    break;
                }
                tempDate = tempDate.AddDays(1);
            }
            whileDate = whileDate.Value.AddDays(1);            
        }

        if (_availableDates.Count > 0)
            PrepareFreeDatesCheck();
        else
            FreeAlternativeDates();
        FreeDatesCheckButton.IsEnabled = false;
    }

    private void FreeAlternativeDates()
    {
        List<DateTime> takenDates = accomodationreservationrepository.FindTakenDates(selectedAccommodation.Id);

        _availableDates.Clear();
        DateTime? whileDate = firstDate.SelectedDate.Value;
        DateTime? firstAvailableDate = null;
        int foundAvailableDates = 0;

        while (foundAvailableDates!=5)
        {
            int freeDaysInRowCounter = 0;
            DateTime tempDate = whileDate.Value;
            while (tempDate != tempDate.AddDays(Convert.ToInt32(DaysOfStay.Text)))
            {
                if (freeDaysInRowCounter == 0)
                    firstAvailableDate = (DateTime)tempDate;
                freeDaysInRowCounter = DateContains(takenDates, tempDate, freeDaysInRowCounter);
                if (freeDaysInRowCounter == Convert.ToInt32(DaysOfStay.Text))
                {
                    foundAvailableDates = SaveFreeDateAlternarive(tempDate, firstAvailableDate, foundAvailableDates);
                    break;
                }
                tempDate = tempDate.AddDays(1);
            }
            whileDate = whileDate.Value.AddDays(1);
        }
        PrepareFreeAlternative();
    }
    private void SaveFreeDate(DateTime tempDate, DateTime? firstAvailableDate)
    {
        AvailableDates available = new AvailableDates((DateTime)firstAvailableDate, tempDate);
        RepeatCheck(available);
    }

    private int SaveFreeDateAlternarive(DateTime tempDate, DateTime? firstAvailableDate, int foundAvailableDates)
    {
        AvailableDates available = new AvailableDates((DateTime)firstAvailableDate, tempDate);
        foundAvailableDates = RepeatCheckAlternative(available, foundAvailableDates);
        return foundAvailableDates;
    }
    private void PrepareFreeDatesCheck()
    {
        FoundDatesTextBox.Visibility = Visibility.Visible;
        FoundAlternativeDatesTextBox.Visibility = Visibility.Collapsed;
        availableDatesListBox.ItemsSource = _availableDates;
        ConfirmButton.IsEnabled = true;
    }

    private int DateContains(List<DateTime> takenDates, DateTime tempDate, int freeDaysInRowCounter)
    {
        if (takenDates.Contains((DateTime)tempDate))
            freeDaysInRowCounter = 0;
        else
            freeDaysInRowCounter++;
        return freeDaysInRowCounter;
    }
    private void PrepareFreeAlternative()
    {
        availableDatesListBox.ItemsSource = _availableDates;
        FoundDatesTextBox.Visibility = Visibility.Collapsed;
        FoundAlternativeDatesTextBox.Visibility = Visibility.Visible;
        ConfirmButton.IsEnabled = true;
    }

    private void ConfrimReservation_Click(object sender, RoutedEventArgs e)
    {
        string regex = @"(\d{1,2}\.\d{1,2}\.\d{4}\.)\s-\s(\d{1,2}\.\d{1,2}\.\d{4}\.)";
        Match match = Regex.Match(SelectedDate, regex);

        DateTime firstDate;
        DateTime lastDate;

        if (match.Success)
        {
            string startDateString = match.Groups[1].Value;
            string endDateString = match.Groups[2].Value;

            firstDate = DateParser.Parse(startDateString);
            lastDate = DateParser.Parse(endDateString);

            reservation = new AccommodationReservation(_user.Id, selectedAccommodation.Id, Convert.ToInt32(GuestNumber.Text), firstDate, lastDate);
            
            accomodationreservationrepository.Add(reservation);

            ConfirmButton.IsEnabled = false;
            ConfirmedReservationTextBox.Visibility = Visibility.Visible;
            UpcomingReservationsChanged_Invoke();
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
            if (lastDate.SelectedDate.HasValue && lastDate.SelectedDate.Value < firstDate.SelectedDate.Value)
            {
                lastDate.SelectedDate = null;
                FreeDatesCheckButton.IsEnabled = false;
            }
            else if(lastDate.SelectedDate.HasValue)
                FreeDatesCheckButton.IsEnabled = true;

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
