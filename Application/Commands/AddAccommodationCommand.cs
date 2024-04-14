using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Linq;
using System.Windows.Input;

namespace BookingApp.Application.Commands;

public class AddAccommodationCommand : ICommand
{
    private AddAccommodationViewModel _addAccommodationViewModel;

    public event EventHandler? CanExecuteChanged;

    public AddAccommodationCommand(AddAccommodationViewModel addAccommodationViewModel)
    {
        _addAccommodationViewModel = addAccommodationViewModel;
    }

    public void Execute(object parameter)
    {
        Accommodation accommodation = new();
        accommodation.Name = _addAccommodationViewModel.AccommodationName;
        accommodation.LocationId = LocationService.GetInstance().GetLocationByCityAndCountry(_addAccommodationViewModel.City, _addAccommodationViewModel.Country).Id;
        accommodation.Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), _addAccommodationViewModel.Type);
        accommodation.MaxGuestNumber = _addAccommodationViewModel.MaximumNumberOfGuests;
        accommodation.MinReservationDays = _addAccommodationViewModel.MinimumNumberOfDaysForReservation;
        accommodation.CancellationPeriodDays = _addAccommodationViewModel.DaysBeforeReservationIsFinal;
        accommodation.Price = _addAccommodationViewModel.AccommodationPrice;
        accommodation.OwnerId = _addAccommodationViewModel.User.Id;
        accommodation.Images = _addAccommodationViewModel.Images.ToList();

        AccommodationService.GetInstance().Add(accommodation);

        _addAccommodationViewModel.CancelCommand.Execute(null);
        _addAccommodationViewModel.ClearPage();
    }

    public bool CanExecute(object parameter)
    {
        return AreAllStringsFilled() && AreAllNumbersOK();
    }

    public bool AreAllNumbersOK()
    {
        return _addAccommodationViewModel.MaximumNumberOfGuests > 0
            && _addAccommodationViewModel.MinimumNumberOfDaysForReservation > 0
            && _addAccommodationViewModel.DaysBeforeReservationIsFinal > 0;
    }

    public bool AreAllStringsFilled()
    {
        return !string.IsNullOrEmpty(_addAccommodationViewModel.AccommodationName)
            && !string.IsNullOrEmpty(_addAccommodationViewModel.Country)
            && !string.IsNullOrEmpty(_addAccommodationViewModel.City)
            && !string.IsNullOrEmpty(_addAccommodationViewModel.Type);
    }
}
