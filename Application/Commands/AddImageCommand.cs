using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Windows.Input;

namespace BookingApp.Application.Commands;

internal class AddImageCommand : ICommand
{
    private AddAccommodationViewModel addAccommodationViewModel;

    public AddImageCommand(AddAccommodationViewModel addAccommodationViewModel)
    {
        this.addAccommodationViewModel = addAccommodationViewModel;
        addAccommodationViewModel.PropertyChanged += (sender, args) => { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return addAccommodationViewModel.IsImageValid();
    }

    public void Execute(object? parameter)
    {
        AccommodationImage image = new();
        image.Path = addAccommodationViewModel.ImageURL;
        addAccommodationViewModel.Images.Add(image);
        addAccommodationViewModel.ImageURL = "";
    }
}