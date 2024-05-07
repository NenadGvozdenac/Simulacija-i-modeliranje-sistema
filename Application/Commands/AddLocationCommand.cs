using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.Application.Commands;

public class AddLocationCommand : ICommand
{
    private EnterLocationViewModel enterLocationViewModel;

    public AddLocationCommand(EnterLocationViewModel enterLocationViewModel)
    {
        this.enterLocationViewModel = enterLocationViewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return !(string.IsNullOrEmpty(enterLocationViewModel.City) || string.IsNullOrEmpty(enterLocationViewModel.Country));
    }

    public void Execute(object? parameter)
    {
        enterLocationViewModel.AddAccommodationViewModel.Location = string.Format("{0}, {1}", enterLocationViewModel.City, enterLocationViewModel.Country);

        Location location = new Location
        {
            City = enterLocationViewModel.City,
            Country = enterLocationViewModel.Country
        };

        LocationService.GetInstance().Add(location);

        enterLocationViewModel.AddAccommodationViewModel.Page.AddLocationModalPanel.Visibility = Visibility.Collapsed;
        Clear();
    }

    private void Clear()
    {
        enterLocationViewModel.City = "";
        enterLocationViewModel.Country = "";
    }
}
