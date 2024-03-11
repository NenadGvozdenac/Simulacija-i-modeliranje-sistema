﻿using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.View.OwnerViews;
using BookingApp.View.OwnerViews.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        MainFrame.Navigate(new MainPage(_user));
    }
}
