using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BookingApp.WPF.Views.OwnerViews;

public partial class DetailedAccommodationPage : Page
{
    private DetailedAccommodationViewModel _viewModel;
    public DetailedAccommodationPage(Accommodation accommodation)
    {
        InitializeComponent();

        _viewModel = new DetailedAccommodationViewModel(this, accommodation);
        DataContext = _viewModel;
    }

    private void Back_Click(object sender, MouseButtonEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }

    private void LeftArrow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        _viewModel.LeftArrowClick();
    }
    private void RightArrow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        _viewModel.RightArrowClick();
    }
}