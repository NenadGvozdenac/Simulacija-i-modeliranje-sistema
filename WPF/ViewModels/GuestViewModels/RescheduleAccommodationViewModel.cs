using BookingApp.Domain.Models;
using BookingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using BookingApp.WPF.Views.GuestViews;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class RescheduleAccommodationViewModel
{
    public AccommodationReservation selectedReservation;
    public RescheduleAccommodation RescheduleAccommodation { get; set; }

    public event EventHandler ChangedMind;
    public event EventHandler SendRequestRefresh;
    public RescheduleAccommodationViewModel(RescheduleAccommodation _RescheduleAcommodation, AccommodationReservation _selectedReservation)
    {
        RescheduleAccommodation = _RescheduleAcommodation;
        selectedReservation = _selectedReservation;
        Update();
    }

    private void Update()
    {
        Accommodation accommodation = AccommodationService.GetInstance().GetById(selectedReservation.AccommodationId);
        RescheduleAccommodation.NameOfTheAccommodation_TextBlock.Text = accommodation.Name;
        AvailableDates temp = new AvailableDates(selectedReservation.FirstDateOfStaying, selectedReservation.LastDateOfStaying);
        RescheduleAccommodation.OriginalCheckInDate_TextBlock.Text = "Original Check-In Date: " + temp.ToString();
        RescheduleAccommodation.firstDate.DisplayDateStart = DateTime.Now;
        HideElements();
    }

    public void ChangedMind_Click()
    {
        ChangedMind?.Invoke(this, EventArgs.Empty);
    }

    public void SendRequest_Click()
    {

        AccommodationReservationService.GetInstance().AddMoving(new AccommodationReservationMoving(selectedReservation.AccommodationId, selectedReservation.Id, selectedReservation.UserId, selectedReservation.FirstDateOfStaying, selectedReservation.LastDateOfStaying, RescheduleAccommodation.firstDate.SelectedDate.Value, RescheduleAccommodation.lastDate.SelectedDate.Value));
        SendRequestRefresh?.Invoke(this, EventArgs.Empty);
        RescheduleAccommodation.NoButton.IsEnabled = false;
        RescheduleAccommodation.YesButton.IsEnabled = false;
        RescheduleAccommodation.SoneBorder.Visibility = Visibility.Visible;
    }

    public void DatePickerCantWrite(object sender, KeyEventArgs e)
    {
        e.Handled = true;
    }

    public void FirstDateChanged()
    {
        DateTime? endDate = RescheduleAccommodation.firstDate.SelectedDate;

        if (endDate.HasValue)
        {
            RescheduleAccommodation.lastDate.DisplayDateStart = endDate.Value.AddDays(1);
            if (RescheduleAccommodation.lastDate.SelectedDate.HasValue && RescheduleAccommodation.lastDate.SelectedDate.Value < RescheduleAccommodation.firstDate.SelectedDate.Value)
            {
                RescheduleAccommodation.lastDate.SelectedDate = null;
                RescheduleAccommodation.YesButton.IsEnabled = false;
            }
            else if (RescheduleAccommodation.lastDate.SelectedDate.HasValue)
                RescheduleAccommodation.YesButton.IsEnabled = true;

            RescheduleAccommodation.lastDate.IsEnabled = true;
        }
    }

    public void LastDateChanged()
    {
        RescheduleAccommodation.YesButton.IsEnabled = true;
    }

    private void HideElements()
    {
        RescheduleAccommodation.firstDate.Text = RescheduleAccommodation.lastDate.Text = string.Empty;
        RescheduleAccommodation.lastDate.IsEnabled = RescheduleAccommodation.YesButton.IsEnabled = false;
    }
}

