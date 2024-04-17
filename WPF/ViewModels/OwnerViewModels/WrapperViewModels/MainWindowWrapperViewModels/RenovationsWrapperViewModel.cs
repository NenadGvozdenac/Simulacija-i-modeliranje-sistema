using BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        // TODO: Add
    }

    private void AddUpcomingRenovations()
    {
        // TODO: Add
    }

    private void AddFinishedRenovations()
    {
        // TODO: Add
    }
}
