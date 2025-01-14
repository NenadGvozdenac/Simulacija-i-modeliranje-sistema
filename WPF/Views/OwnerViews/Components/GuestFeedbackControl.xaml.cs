﻿using BookingApp.Domain.Models;
using BookingApp.Domain.Miscellaneous;
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
using BookingApp.WPF.Views.OwnerViews.AnimatorHelpers;

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class GuestFeedbackControl : UserControl
{
    public DetailedGuestFeedbackViewModel DetailedGuestFeedbackViewModel;
    public GuestFeedbackControl(AccommodationReview accommodationReview)
    {
        DetailedGuestFeedbackViewModel = new DetailedGuestFeedbackViewModel(accommodationReview);
        DataContext = DetailedGuestFeedbackViewModel;
        InitializeComponent();
        HoverAnimation hoverAnimation = new HoverAnimation();
        hoverAnimation.AnimateHover(this.Border);
    }

    private void Card_MouseDown(object sender, MouseButtonEventArgs e)
    {
        DetailedGuestFeedbackViewModel.CardClicked(this);
    }
}
