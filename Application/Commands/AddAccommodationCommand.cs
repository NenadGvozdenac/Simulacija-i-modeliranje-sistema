using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.Application.Commands;

public class AddAccommodationCommand : ICommand
{
    private AddAccommodationViewModel _addAccommodationViewModel;

    public event EventHandler? CanExecuteChanged;

    public AddAccommodationCommand(AddAccommodationViewModel addAccommodationViewModel)
    {
        _addAccommodationViewModel = addAccommodationViewModel;
        addAccommodationViewModel.PropertyChanged += (sender, args) => { RaiseCanExecuteChanged(); };
    }

    private void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
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
        return _addAccommodationViewModel.IsDataValid();
    }
}
