using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.Application.Commands;

public class CancelAddingNewLocationCommand : ICommand
{
    private readonly EnterLocationViewModel enterLocationViewModel;

    public event EventHandler? CanExecuteChanged;

    public CancelAddingNewLocationCommand(EnterLocationViewModel enterLocationViewModel)
    {
        this.enterLocationViewModel = enterLocationViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        enterLocationViewModel.AddAccommodationViewModel.Page.AddLocationModalPanel.Visibility = Visibility.Collapsed;
        Clear();
    }

    private void Clear()
    {
        enterLocationViewModel.City = "";
        enterLocationViewModel.Country = "";
    }
}
