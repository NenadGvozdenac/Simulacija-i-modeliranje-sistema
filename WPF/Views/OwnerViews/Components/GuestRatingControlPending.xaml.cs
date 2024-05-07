using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using BookingApp.WPF.Views.OwnerViews.AnimatorHelpers;
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
    private readonly bool isPencilEnabled;
    public PendingGuestRatingCardViewModel _pendingGuestRatingCardViewModel;

    public GuestRatingControlPending(GuestRating guestRating, bool isPencilEnabled)
    {
        this.isPencilEnabled = isPencilEnabled;
        InitializeComponent();

        _pendingGuestRatingCardViewModel = new(this, guestRating);
        DataContext = _pendingGuestRatingCardViewModel;

        if(isPencilEnabled)
        {
            Border.Cursor = Cursors.Hand;
            EyeButton.Visibility = Visibility.Visible;
            HoverAnimation hoverAnimation = new HoverAnimation();
            hoverAnimation.AnimateHover(this.Border);
        } else
        {
            XImage.Visibility = Visibility.Visible;
        }
    }

    private void EyeButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if(isPencilEnabled)
            _pendingGuestRatingCardViewModel.EyeButtonClicked(this);
    }
}