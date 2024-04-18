using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.Application.Commands;

internal class CancelRenovationCommand : ICommand
{
    private readonly DetailedRenovationView page;

    public event EventHandler? CanExecuteChanged;

    public CancelRenovationCommand(DetailedRenovationView page)
    {
        this.page = page;
    }

    public bool CanExecute(object? parameter)
    {
        return page.viewModel.RenovationDTO.LastCancellationDay > DateTime.Now;
    }

    public void Execute(object? parameter)
    {
        AccommodationRenovation renovation = page.viewModel.Renovation;
        AccommodationRenovationService.GetInstance().DeleteAccommodationRenovation(renovation);
        NavigationService.GetNavigationService(page).GoBack();
    }
}
