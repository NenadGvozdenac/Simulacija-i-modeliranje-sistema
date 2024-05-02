using BookingApp.Application.Localization;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
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

        string location = _addAccommodationViewModel.Location;
        string city = location.Split(", ")[0];
        string country = location.Split(", ")[1];

        accommodation.LocationId = LocationService.GetInstance().GetLocationByCityAndCountry(city, country).Id;
        accommodation.Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), TranslationSource.Instance[_addAccommodationViewModel.Type]);
        accommodation.MaxGuestNumber = _addAccommodationViewModel.MaximumNumberOfGuests;
        accommodation.MinReservationDays = _addAccommodationViewModel.MinimumNumberOfDaysForReservation;
        accommodation.CancellationPeriodDays = _addAccommodationViewModel.DaysBeforeReservationIsFinal;
        accommodation.Price = _addAccommodationViewModel.AccommodationPrice;
        accommodation.OwnerId = _addAccommodationViewModel.User.Id;
        accommodation.Images = _addAccommodationViewModel.Images.ToList();

        AccommodationService.GetInstance().Add(accommodation);

        _addAccommodationViewModel.CancelCommand.Execute(null);
    }

    public bool CanExecute(object parameter)
    {
        return AreAllStringsFilled() && AreAllNumbersOK() && IsLocationValid();
    }

    public bool IsLocationValid()
    {
        string location = _addAccommodationViewModel.Location;
        string city = location.Split(", ")[0];
        string country = location.Split(", ")[1];
    
        return LocationService.GetInstance().GetLocationByCityAndCountry(city, country) != null;
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
            && !string.IsNullOrEmpty(_addAccommodationViewModel.Location)
            && !string.IsNullOrEmpty(_addAccommodationViewModel.Type);
    }
}
