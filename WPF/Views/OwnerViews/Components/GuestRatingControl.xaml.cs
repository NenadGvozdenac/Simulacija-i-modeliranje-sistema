using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.OwnerViewModels;
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

public partial class GuestRatingControl : UserControl
{
    private DetailedGuestRatingViewModel _detailedGuestRatingViewModel;
    public GuestRatingControl(User user, GuestRating guestRating)
    {
        InitializeComponent();

        _detailedGuestRatingViewModel = new(guestRating);
        DataContext = _detailedGuestRatingViewModel;

        HoverAnimation hoverAnimation = new HoverAnimation();
        hoverAnimation.AnimateHover(this.Border);
    }

    private void EyeButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        _detailedGuestRatingViewModel.EyeButtonClicked(this);
    }
}
