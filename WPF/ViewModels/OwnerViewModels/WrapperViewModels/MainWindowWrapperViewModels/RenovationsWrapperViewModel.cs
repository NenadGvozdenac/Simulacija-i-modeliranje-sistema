using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews.Components;
using BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.WrapperViewModels.MainWindowWrapperViewModels;

public class RenovationsWrapperViewModel
{
    private readonly AccommodationRenovationWrapper accommodationRenovationWrapper;
    private readonly MainPageViewModel mainPageViewModel;

    public RenovationsWrapperViewModel(AccommodationRenovationWrapper accommodationRenovationWrapper, MainPageViewModel mainPageViewModel)
    {
        this.accommodationRenovationWrapper = accommodationRenovationWrapper;
        this.mainPageViewModel = mainPageViewModel;

        AddRenovations();
    }

    public void Refresh()
    {
        mainPageViewModel.Load();
        AddRenovations();
    }

    private void AddRenovations()
    {
        ClearRenovations();
        AddOngoingRenovations();
        AddUpcomingRenovations();
        AddFinishedRenovations();
    }

    private void ClearRenovations()
    {
        accommodationRenovationWrapper.Renovations.Children.Clear();
    }

    private void AddOngoingRenovations()
    {
        foreach(AccommodationRenovation renovation in mainPageViewModel.AccommodationRenovations)
        {
            if(renovation.DateSpan.Start <= DateTime.Now && renovation.DateSpan.End >= DateTime.Now)
            {
                RenovationControl component = new RenovationControl(accommodationRenovationWrapper, renovation);
                component.Margin = new Thickness(15);

                accommodationRenovationWrapper.Renovations.Children.Add(component);
            }
        }
    }

    private void AddUpcomingRenovations()
    {
        foreach(AccommodationRenovation renovation in mainPageViewModel.AccommodationRenovations)
        {
            if(renovation.DateSpan.Start > DateTime.Now)
            {
                RenovationControl component = new RenovationControl(accommodationRenovationWrapper, renovation);
                component.Margin = new Thickness(15);

                accommodationRenovationWrapper.Renovations.Children.Add(component);
            }
        }
    }

    private void AddFinishedRenovations()
    {
        foreach(AccommodationRenovation renovation in mainPageViewModel.AccommodationRenovations)
        {
            if(renovation.DateSpan.End < DateTime.Now)
            {
                RenovationControl component = new RenovationControl(accommodationRenovationWrapper, renovation);
                component.Margin = new Thickness(15);

                accommodationRenovationWrapper.Renovations.Children.Add(component);
            }
        }
    }
}
