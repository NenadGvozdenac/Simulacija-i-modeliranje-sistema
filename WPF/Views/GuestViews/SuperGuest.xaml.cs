﻿using BookingApp.Domain.Models;
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

/// <summary>
/// Interaction logic for SuperGuest.xaml
/// </summary>
public partial class SuperGuest : UserControl
{
    public SuperGuest(User _user)
    {
        InitializeComponent();
    }
}