using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.Application.Commands;

public class RemoveImageCommand : ICommand
{
    private AddAccommodationViewModel addAccommodationViewModel;

    public RemoveImageCommand(AddAccommodationViewModel addAccommodationViewModel)
    {
        this.addAccommodationViewModel = addAccommodationViewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return addAccommodationViewModel.Images.Count > 0;
    }

    public void Execute(object? parameter)
    {
        var currentImage = addAccommodationViewModel.Images.FirstOrDefault(image => image.Path == addAccommodationViewModel.ImageURL);
        addAccommodationViewModel.Images.Remove(currentImage);
        addAccommodationViewModel.ImageURL = addAccommodationViewModel.Images.FirstOrDefault()?.Path;
    }
}
