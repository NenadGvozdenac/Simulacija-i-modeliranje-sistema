using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Linq;
using System.Windows.Input;

namespace BookingApp.Application.Commands;

internal class AddImageCommand : ICommand
{
    private AddAccommodationViewModel addAccommodationViewModel;

    public AddImageCommand(AddAccommodationViewModel addAccommodationViewModel)
    {
        this.addAccommodationViewModel = addAccommodationViewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return !(string.IsNullOrEmpty(addAccommodationViewModel.ImageURL) || ImageAlreadyExists());
    }

    public bool ImageAlreadyExists()
    {
        return addAccommodationViewModel.Images.Any(image => image.Path.Equals(addAccommodationViewModel.ImageURL));
    }

    public void Execute(object? parameter)
    {
        AccommodationImage image = new();
        image.Path = addAccommodationViewModel.ImageURL;
        addAccommodationViewModel.Images.Add(image);
        addAccommodationViewModel.ImageURL = "";
    }
}