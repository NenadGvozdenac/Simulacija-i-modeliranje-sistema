using BookingApp.WPF.Views.OwnerViews;
using System;
using System.Windows.Input;

namespace BookingApp.Application.Commands;

internal class FindAvailableTimespanForRenovationCommand : ICommand
{
    private DetailedScheduleRenovationPage detailedScheduleRenovationPage;

    public event EventHandler? CanExecuteChanged;

    public FindAvailableTimespanForRenovationCommand(DetailedScheduleRenovationPage detailedScheduleRenovationPage)
    {
        this.detailedScheduleRenovationPage = detailedScheduleRenovationPage;
    }

    public bool CanExecute(object? parameter)
    {
        return detailedScheduleRenovationPage.DetailedScheduleRenovationViewModel.StartDate != null
            && detailedScheduleRenovationPage.DetailedScheduleRenovationViewModel.EndDate != null
            && !string.IsNullOrEmpty(detailedScheduleRenovationPage.DetailedScheduleRenovationViewModel.TimePeriod)
            && int.TryParse(detailedScheduleRenovationPage.DetailedScheduleRenovationViewModel.TimePeriod, out _)
            && int.Parse(detailedScheduleRenovationPage.DetailedScheduleRenovationViewModel.TimePeriod) > 0
            && detailedScheduleRenovationPage.DetailedScheduleRenovationViewModel.StartDate.AddDays(int.Parse(detailedScheduleRenovationPage.DetailedScheduleRenovationViewModel.TimePeriod)) < detailedScheduleRenovationPage.DetailedScheduleRenovationViewModel.EndDate;
    }

    public void Execute(object? parameter)
    {
        detailedScheduleRenovationPage.DetailedScheduleRenovationViewModel.PopulateTable();
    }
}
