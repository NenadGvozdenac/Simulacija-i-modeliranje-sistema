using BookingApp.Application.UseCases;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.GuestViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class AnywhereAnytimeViewModel
{
    public AnywhereAnytime AnywhereAnytime { get; set; }
    
    public ObservableCollection<Accommodation> FilteredAccommodations { get; set; }

    public AnywhereAnytimeViewModel(AnywhereAnytime anywhereAnytime, User user)
    {
        AnywhereAnytime = anywhereAnytime;
        FilteredAccommodations = new ObservableCollection<Accommodation>();
    }

    public void FindPlaces_Click()
    {
        FilteredAccommodations.Clear();

        foreach(var accommodation in AccommodationService.GetInstance().GetAll())
        {
            if(AnywhereAnytime.firstDate.SelectedDate != null && AnywhereAnytime.lastDate.SelectedDate != null)
            {
                if(FreeDatesCheck_Click(accommodation.Id))
                    FilteredAccommodations.Add(accommodation);
            }
            else
            {
                if(accommodation.MaxGuestNumber >= int.Parse(AnywhereAnytime.GuestsNumberTextBox.Text) && accommodation.MinReservationDays <= int.Parse(AnywhereAnytime.DaysOfStayTextBox.Text))
                {
                    FilteredAccommodations.Add(accommodation);
                }
            }

        }
    }

    public bool FreeDatesCheck_Click(int accommodation_id)
    {
        List<DateTime> takenDates = AccommodationReservationService.GetInstance().FindTakenDates(accommodation_id);

        DateTime? whileDate = AnywhereAnytime.firstDate.SelectedDate;
        DateTime? firstAvailableDate = null;
        int daysOfStay = 1;
        if (!String.IsNullOrEmpty(AnywhereAnytime.DaysOfStayTextBox.Text))
        {
            daysOfStay = Convert.ToInt32(AnywhereAnytime.DaysOfStayTextBox.Text);
        }    

        while (whileDate != AnywhereAnytime.lastDate.SelectedDate.Value.AddDays(1))
        {
            int freeDaysInRowCounter = 0;
            DateTime tempDate = whileDate.Value;

            while (tempDate != AnywhereAnytime.lastDate.SelectedDate.Value.AddDays(1))
            {
                if (freeDaysInRowCounter == 0)
                    firstAvailableDate = tempDate;

                freeDaysInRowCounter = DateContains(takenDates, tempDate, freeDaysInRowCounter);

                if (freeDaysInRowCounter == daysOfStay)
                {
                    return true;
                }
                tempDate = tempDate.AddDays(1);
            }
            whileDate = whileDate.Value.AddDays(1);
        }
        return false;
    }

    private int DateContains(List<DateTime> takenDates, DateTime tempDate, int freeDaysInRowCounter)
    {
        if (takenDates.Contains(tempDate))
            freeDaysInRowCounter = 0;
        else
            freeDaysInRowCounter++;
        return freeDaysInRowCounter;
    }
}
