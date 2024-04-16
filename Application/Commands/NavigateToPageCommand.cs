using BookingApp.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.Application.Commands;

public class NavigateToPageCommand : ICommand
{
    public Page currentPage;
    public Page wantedPage { get; set; }

    private Action refresh;

    public NavigateToPageCommand(Page currentPage, Page wantedPage)
    {
        this.currentPage = currentPage;
        this.wantedPage = wantedPage;
        this.refresh = null;
    }

    public NavigateToPageCommand(Page currentPage, Page wantedPage, Action refreshAction)
    {
        this.currentPage = currentPage;
        this.wantedPage = wantedPage;
        this.refresh = refreshAction;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        NavigationService.GetNavigationService(currentPage)?.Navigate(wantedPage);
        InvokeRefresh();
    }

    private void InvokeRefresh()
    {
        if (this.refresh == null) return;

        if (this.wantedPage is AddAccommodationPage addAccommodationPage)
        {
            addAccommodationPage.PageClosed += (s, e) => refresh();
        }
        else if (this.wantedPage is ReservationReschedulingPage reservationReschedulingPage)
        {
            reservationReschedulingPage.PageClosed += (s, e) => refresh();
        }
    }
}
