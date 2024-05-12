using BookingApp.Domain.Miscellaneous;
using BookingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using BookingApp.WPF.Views.GuestViews;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class AccommodationDetailsViewModel
{
    public Accommodation selectedAccommodation { get; set; }

    public AccommodationReservation reservation;
    public ObservableCollection<string> _availableDates;
    public ObservableCollection<AccommodationReview> _reviews { get; set; }
    public AccommodationDetails AccommodationDetails { get; set; }

    public event EventHandler UpcomingReservationsChanged;
    public string SelectedDate { get; set; }

    int minvalueDaysOfStay = -1,
    maxvalueDaysOfStay = 100,
    startvalueDaysOfStay = 1;

    int minvalueGuestNumber = 1,
    maxvalueGuestNumber = 30,
    startvalueGuestNumber = 1;
    public User _user { get; set; }
    public AccommodationDetailsViewModel(AccommodationDetails _AccommodationDetails, Accommodation detailedaccomodation, User user)
    {
        AccommodationDetails = _AccommodationDetails;
        _reviews = new ObservableCollection<AccommodationReview>();
        SetAccommodation(detailedaccomodation, user);
    }

    public void UpcomingReservationsChanged_Invoke()
    {
        UpcomingReservationsChanged.Invoke(this, EventArgs.Empty);
    }

    public void SetAccommodation(Accommodation accommodation, User user)
    {
        _availableDates = new ObservableCollection<string>();
        reservation = new AccommodationReservation();
        selectedAccommodation = accommodation;
        _user = user;
        AccommodationDetails.username.Content = _user.Username;

        if (GuestService.GetInstance().GetByGuestId(user.Id).IsSuperGuest)
        {
            AccommodationDetails.crownImage.Visibility = Visibility.Visible;
        }
        else
        {
            AccommodationDetails.crownImage.Visibility = Visibility.Hidden;
        }

        HideElements();
        SetDefaultValues(accommodation);
        LoadReviews();
    }

    private void HideElements()
    {
        AccommodationDetails.firstDate.Text = AccommodationDetails.lastDate.Text = string.Empty;
        AccommodationDetails.lastDate.IsEnabled = AccommodationDetails.FreeDatesCheckButton.IsEnabled = false;
        AccommodationDetails.ConfirmButton.IsEnabled = false;
    }

    private void SetDefaultValues(Accommodation accommodation)
    {
        AccommodationDetails.firstDate.DisplayDateStart = DateTime.Now;
        minvalueDaysOfStay = accommodation.MinReservationDays;
        AccommodationDetails.DaysOfStay.Text = accommodation.MinReservationDays.ToString();
        AccommodationDetails.GuestNumber.Text = "1";
        maxvalueGuestNumber = accommodation.MaxGuestNumber;
        AccommodationDetails.accommodationName.Text = accommodation.Name;
        AccommodationDetails.accomodationAverageReviewScore.Text = $"{accommodation.AverageReviewScore}/10";
    }

    private void LoadReviews()
    {
        _reviews.Clear();
        foreach (AccommodationReview review in AccommodationReviewService.GetInstance().GetAll())
        {
            if (review.AccommodationId == selectedAccommodation.Id)
            {
                _reviews.Add(review);
            }
        }
    }

    public void GoBack_Click(object sender, RoutedEventArgs e)
    {
        (Window.GetWindow(AccommodationDetails) as GuestMainWindow).Accommodations_Click(sender, e);
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
    public void FreeDatesCheck_Click()
    {
        List<DateTime> takenDates = AccommodationReservationService.GetInstance().FindTakenDates(selectedAccommodation.Id);
        AccommodationDetails.PdfButton.Visibility = Visibility.Collapsed;
        AccommodationDetails.ConfirmedReservationTextBox.Visibility = Visibility.Collapsed;

        _availableDates.Clear();
        DateTime? whileDate = AccommodationDetails.firstDate.SelectedDate;
        DateTime? firstAvailableDate = null;

        while (whileDate != AccommodationDetails.lastDate.SelectedDate.Value.AddDays(1))
        {
            int freeDaysInRowCounter = 0;
            DateTime tempDate = whileDate.Value;

            while (tempDate != AccommodationDetails.lastDate.SelectedDate.Value.AddDays(1))
            {
                if (freeDaysInRowCounter == 0)
                    firstAvailableDate = tempDate;

                freeDaysInRowCounter = DateContains(takenDates, tempDate, freeDaysInRowCounter);

                if (freeDaysInRowCounter == Convert.ToInt32(AccommodationDetails.DaysOfStay.Text))
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
        AccommodationDetails.FreeDatesCheckButton.IsEnabled = false;
    }

    private void FreeAlternativeDates()
    {
        List<DateTime> takenDates = AccommodationReservationService.GetInstance().FindTakenDates(selectedAccommodation.Id);

        _availableDates.Clear();
        DateTime? whileDate = AccommodationDetails.firstDate.SelectedDate.Value;
        DateTime? firstAvailableDate = null;
        int foundAvailableDates = 0;

        while (foundAvailableDates != 5)
        {
            int freeDaysInRowCounter = 0;
            DateTime tempDate = whileDate.Value;
            while (tempDate != tempDate.AddDays(Convert.ToInt32(AccommodationDetails.DaysOfStay.Text)))
            {
                if (freeDaysInRowCounter == 0)
                    firstAvailableDate = tempDate;
                freeDaysInRowCounter = DateContains(takenDates, tempDate, freeDaysInRowCounter);
                if (freeDaysInRowCounter == Convert.ToInt32(AccommodationDetails.DaysOfStay.Text))
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
        AccommodationDetails.FoundDatesTextBox.Visibility = Visibility.Visible;
        AccommodationDetails.FoundAlternativeDatesTextBox.Visibility = Visibility.Collapsed;
        AccommodationDetails.availableDatesListBox.ItemsSource = _availableDates;
        AccommodationDetails.ConfirmButton.IsEnabled = true;
    }

    private int DateContains(List<DateTime> takenDates, DateTime tempDate, int freeDaysInRowCounter)
    {
        if (takenDates.Contains(tempDate))
            freeDaysInRowCounter = 0;
        else
            freeDaysInRowCounter++;
        return freeDaysInRowCounter;
    }
    private void PrepareFreeAlternative()
    {
        AccommodationDetails.availableDatesListBox.ItemsSource = _availableDates;
        AccommodationDetails.FoundDatesTextBox.Visibility = Visibility.Collapsed;
        AccommodationDetails.FoundAlternativeDatesTextBox.Visibility = Visibility.Visible;
        AccommodationDetails.ConfirmButton.IsEnabled = true;
    }

    public void ConfrimReservation_Click()
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

            reservation = new AccommodationReservation(_user.Id, selectedAccommodation.Id, Convert.ToInt32(AccommodationDetails.GuestNumber.Text), firstDate, lastDate);

            AccommodationReservationService.GetInstance().Add(reservation);

            //take one point if super guest
            GuestInfo guestInfo = GuestService.GetInstance().GetByGuestId(_user.Id);
            if (guestInfo.IsSuperGuest && guestInfo.NumberOfPoints > 0)
            {
                guestInfo.NumberOfPoints--;
                GuestService.GetInstance().UpdateGuestInfo(guestInfo);
            }

            AccommodationDetails.PdfButton.Visibility = Visibility.Visible;
            AccommodationDetails.ConfirmButton.IsEnabled = false;
            AccommodationDetails.ConfirmedReservationTextBox.Visibility = Visibility.Visible;
            UpcomingReservationsChanged_Invoke();
        }
    }



    //Design Functions
    public void DatePickerCantWrite(object sender, KeyEventArgs e)
    {
        e.Handled = true;
    }

    public void FirstDateChanged()
    {
        DateTime? endDate = AccommodationDetails.firstDate.SelectedDate;

        if (endDate.HasValue)
        {
            AccommodationDetails.lastDate.DisplayDateStart = endDate.Value.AddDays(1);
            if (AccommodationDetails.lastDate.SelectedDate.HasValue && AccommodationDetails.lastDate.SelectedDate.Value < AccommodationDetails.firstDate.SelectedDate.Value)
            {
                AccommodationDetails.lastDate.SelectedDate = null;
                AccommodationDetails.FreeDatesCheckButton.IsEnabled = false;
            }
            else if (AccommodationDetails.lastDate.SelectedDate.HasValue)
                AccommodationDetails.FreeDatesCheckButton.IsEnabled = true;

            AccommodationDetails.lastDate.IsEnabled = true;
        }
    }

    public void LastDateChanged()
    {
        AccommodationDetails.FreeDatesCheckButton.IsEnabled = true;
    }

    public void DaysOfStayUp_Click()
    {
        int number;
        if (AccommodationDetails.DaysOfStay.Text != "") number = Convert.ToInt32(AccommodationDetails.DaysOfStay.Text);
        else number = 0;
        if (number < maxvalueDaysOfStay)
            AccommodationDetails.DaysOfStay.Text = Convert.ToString(number + 1);
    }

    public void DaysOfStayDown_Click()
    {
        int number;
        if (AccommodationDetails.DaysOfStay.Text != "") number = Convert.ToInt32(AccommodationDetails.DaysOfStay.Text);
        else number = 0;
        if (number > minvalueDaysOfStay)
            AccommodationDetails.DaysOfStay.Text = Convert.ToString(number - 1);
    }



    public void DaysOfStay_TextChanged()
    {
        int number = 0;
        if (AccommodationDetails.DaysOfStay.Text != "")
            if (!int.TryParse(AccommodationDetails.DaysOfStay.Text, out number)) AccommodationDetails.DaysOfStay.Text = startvalueDaysOfStay.ToString();
        if (number > maxvalueDaysOfStay) AccommodationDetails.DaysOfStay.Text = maxvalueDaysOfStay.ToString();
        if (number < minvalueDaysOfStay) AccommodationDetails.DaysOfStay.Text = minvalueDaysOfStay.ToString();
        AccommodationDetails.DaysOfStay.SelectionStart = AccommodationDetails.DaysOfStay.Text.Length;
    }

    public void GuestNumberUp_Click()
    {
        int number;
        if (AccommodationDetails.GuestNumber.Text != "") number = Convert.ToInt32(AccommodationDetails.GuestNumber.Text);
        else number = 0;
        if (number < maxvalueGuestNumber)
            AccommodationDetails.GuestNumber.Text = Convert.ToString(number + 1);
    }

    public void GuestNumberDown_Click()
    {
        int number;
        if (AccommodationDetails.GuestNumber.Text != "") number = Convert.ToInt32(AccommodationDetails.GuestNumber.Text);
        else number = 0;
        if (number > minvalueGuestNumber)
            AccommodationDetails.GuestNumber.Text = Convert.ToString(number - 1);
    }

    public void GuestNumber_TextChanged()
    {
        int number = 0;
        if (AccommodationDetails.GuestNumber.Text != "")
            if (!int.TryParse(AccommodationDetails.GuestNumber.Text, out number)) AccommodationDetails.GuestNumber.Text = startvalueGuestNumber.ToString();
        if (number > maxvalueGuestNumber) AccommodationDetails.GuestNumber.Text = maxvalueGuestNumber.ToString();
        if (number < minvalueGuestNumber) AccommodationDetails.GuestNumber.Text = minvalueGuestNumber.ToString();
        AccommodationDetails.GuestNumber.SelectionStart = AccommodationDetails.GuestNumber.Text.Length;
    }
}
