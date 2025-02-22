﻿using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.GuestViewModels;
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

namespace BookingApp.WPF.Views.GuestViews;
public partial class OwnerFeedback : UserControl
{
    public OwnerFeedbackViewModel OwnerFeedbackViewModel { get; set; }
    public OwnerFeedback(User _user)
    {
        InitializeComponent();
        OwnerFeedbackViewModel = new OwnerFeedbackViewModel(this, _user);
        DataContext = OwnerFeedbackViewModel;
    }

    private void ReviewHandling(object sender, int reservationId)
    {
        OwnerFeedbackViewModel.ReviewHandling(sender, reservationId);
    }

}
