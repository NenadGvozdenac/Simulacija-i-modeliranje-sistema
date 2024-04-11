using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.Application.Commands;

public class NavigateToPreviousPageCommand : ICommand
{
    private Page _page;
    public NavigateToPreviousPageCommand(Page page)
    {
        _page = page;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (_page.NavigationService.CanGoBack)
        {
            _page.NavigationService.GoBack();
        }
    }
}