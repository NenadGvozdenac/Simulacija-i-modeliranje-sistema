using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
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

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class GuestRatingControlPending : UserControl
{
    public EventHandler RefreshPage { get; internal set; }
    public PendingGuestRatingCardViewModel _pendingGuestRatingCardViewModel;

    public GuestRatingControlPending(GuestRating guestRating, bool isPencilEnabled)
    {
        InitializeComponent();

        _pendingGuestRatingCardViewModel = new(this, guestRating, isPencilEnabled);
        DataContext = _pendingGuestRatingCardViewModel;
    }

    private void EyeButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        _pendingGuestRatingCardViewModel.EyeButtonClicked(this);
    }

    public void Refresh()
    {
        RefreshPage.Invoke(this, EventArgs.Empty);
    }
}