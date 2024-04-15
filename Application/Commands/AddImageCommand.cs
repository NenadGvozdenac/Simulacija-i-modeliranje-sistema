using BookingApp.Application.UseCases;
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
        return true;
    }

    public void Execute(object? parameter)
    {
        var path = ImageService.GetInstance().GetImageFromUser();
        if (path != null)
        {
            if(!ImageAlreadyAdded(path))
            {
                addAccommodationViewModel.Images.Add(new AccommodationImage(path));
                addAccommodationViewModel.ImageURL = path;
            }
        }
    }

    public bool ImageAlreadyAdded(string path)
    {
        return addAccommodationViewModel.Images.Any(image => image.Path == path);
    }
}