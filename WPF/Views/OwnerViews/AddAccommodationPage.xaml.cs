﻿using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using System.Linq;
using System.Collections.Generic;

namespace BookingApp.WPF.Views.OwnerViews;

public partial class AddAccommodationPage : Page
{
    public AddAccommodationViewModel _addAccommodationViewModel;

    public AddAccommodationPage(User user)
    {
        _addAccommodationViewModel = new AddAccommodationViewModel(this, user);
        DataContext = _addAccommodationViewModel;

        InitializeComponent();
    }

    private void LocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _addAccommodationViewModel.LocationTextBox_TextChanged();
    }

    private void LocationTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        _addAccommodationViewModel.LocationTextBox_PreviewKeyDown(e.Key);
    }

    private void BackButton_Click(object sender, MouseButtonEventArgs e)
    {
        _addAccommodationViewModel.CancelCommand.Execute(null);
    }

    private void LeftArrow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        _addAccommodationViewModel.LeftArrowClick();
    }
    private void RightArrow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        _addAccommodationViewModel.RightArrowClick();
    }
}