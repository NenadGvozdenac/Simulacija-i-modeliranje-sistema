﻿using BookingApp.Model.MutualModels;
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

namespace BookingApp.View;

/// <summary>
/// Interaction logic for OwnerMainWindow.xaml
/// </summary>
public partial class OwnerMainWindow : Window
{
    private readonly User _user;
    public OwnerMainWindow(User user)
    {
        InitializeComponent();
        _user = user;
        SetUsername();
    }

    private void SetUsername()
    {
        Username.Content = _user.Username;
    }

    private void ExitApplication(object sender, MouseButtonEventArgs e)
    {
        Close();
    }
}
