using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Resources.Types;
using BookingApp.WPF.Views.OwnerViews.Components;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.OwnerViews;

public partial class ReservationReschedulingPage : Page
{

    public event EventHandler PageClosed;

    public ReservationReschedulingViewModel ReservationReschedulingViewModel { get; set; }
    public ReservationReschedulingPage(User user)
    {
        InitializeComponent();
        ReservationReschedulingViewModel = new ReservationReschedulingViewModel(this, user);
    }

    private void InvokePageClosed()
    {
        PageClosed?.Invoke(this, EventArgs.Empty);
    }

    private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            InvokePageClosed();
            NavigationService.GoBack();
        }
    }
}
