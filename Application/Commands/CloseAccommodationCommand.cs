using BookingApp.Application.UseCases;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Windows.Input;

namespace BookingApp.Application.Commands;

public class CloseAccommodationCommand : ICommand
{
    private DetailedAccommodationViewModel detailedAccommodationViewModel;

    public CloseAccommodationCommand(DetailedAccommodationViewModel detailedAccommodationViewModel)
    {
        this.detailedAccommodationViewModel = detailedAccommodationViewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        if (AccommodationReservationService.GetInstance().GetByAccommodationId(detailedAccommodationViewModel._accommodation.Id).Count > 0)
        {
            return false;
        }

        if (GuestRatingService.GetInstance().GetByAccommodationId(detailedAccommodationViewModel._accommodation.Id).Count > 0)
        {
            return false;
        }

        return true;
    }

    public void Execute(object? parameter)
    {
        AccommodationService.GetInstance().Delete(detailedAccommodationViewModel._accommodation);
        detailedAccommodationViewModel.Page.AccommodationClosedHandler(this, EventArgs.Empty);
        detailedAccommodationViewModel.Page.NavigationService.GoBack();
    }
}