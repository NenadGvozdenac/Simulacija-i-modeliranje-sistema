using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;
using Microsoft.Extensions.DependencyInjection;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;

namespace BookingApp.WPF.Views.OwnerViews;

public partial class MainPage : Page
{
    private MainPageViewModel _mainPageViewModel;

    public MainPage(User user)
    {
        InitializeComponent();

        _mainPageViewModel = new MainPageViewModel(this, user);
        DataContext = _mainPageViewModel;
    }

    private void HamburgerMenuClick(object sender, MouseButtonEventArgs e)
    {
        _mainPageViewModel.ToggleNavbar(LeftNavbar);
    }

    private void ThreeDotsClick(object sender, MouseButtonEventArgs e)
    {
        _mainPageViewModel.ToggleNavbar(RightNavbar);
    }

    private void ClickHere_TextBlockClick(object sender, MouseButtonEventArgs e)
    {
        _mainPageViewModel.ClickHere();
    }
    private void AccommodationsButton_Click(object sender, RoutedEventArgs e)
    {
        _mainPageViewModel.AccommodationsClicked();
    }

    private void ReservationsButton_Click(object sender, RoutedEventArgs e)
    {
        _mainPageViewModel.ReservationsClicked();
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        _mainPageViewModel.Logout();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        _mainPageViewModel.Refresh();
    }
}
