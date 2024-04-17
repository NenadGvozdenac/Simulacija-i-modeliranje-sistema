using BookingApp.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.Application.Commands;

internal class ScheduleRenovationCommand : ICommand
{
    private DetailedScheduleRenovationPage page;

    public event EventHandler? CanExecuteChanged;

    public ScheduleRenovationCommand(DetailedScheduleRenovationPage page)
    {
        this.page = page;
    }

    public bool CanExecute(object? parameter)
    {
        return page.DetailedScheduleRenovationViewModel.SelectedDateSpan != null;
    }

    public void Execute(object? parameter)
    {
        page.DetailedScheduleRenovationViewModel.ScheduleRenovation();

        if (page.NavigationService.CanGoBack)
        {
            page.NavigationService.GoBack();
        }
    }
}
