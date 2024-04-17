using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.OwnerViews.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class ScheduleRenovationViewModel
{
    private ScheduleRenovationPage scheduleRenovationPage;
    private readonly User user;

    public ScheduleRenovationViewModel(ScheduleRenovationPage scheduleRenovationPage, User user)
    {
        this.scheduleRenovationPage = scheduleRenovationPage;
        this.user = user;
    }

    public void LoadData()
    {
        scheduleRenovationPage.MainPanel.Children.Clear();

        foreach(var Accommodation in AccommodationService.GetInstance().GetByOwnerId(user.Id))
        {
            ScheduleAccommodationForRenovationControl scheduleAccommodationForRenovationControl = new ScheduleAccommodationForRenovationControl(Accommodation);
            scheduleAccommodationForRenovationControl.Margin = new System.Windows.Thickness(0, 0, 0, 15);

            scheduleRenovationPage.MainPanel.Children.Add(scheduleAccommodationForRenovationControl);
        }
    }
}
