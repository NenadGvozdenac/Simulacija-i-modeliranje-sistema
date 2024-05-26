using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuestViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

public partial class Accommodations : UserControl
{ 
    public AccommodationsViewModel AccommodationsViewModel { get; set; }
    public Accommodations(User user)
    {
        InitializeComponent();
        AccommodationsViewModel = new AccommodationsViewModel(this, user);
        DataContext = AccommodationsViewModel;
        Overlay.MouseLeftButtonDown += Overlay_MouseLeftButtonDown;
    }

    private async void StartDemoButton_Click(object sender, RoutedEventArgs e)
    {
        await AccommodationsViewModel.StartDemoMode();
    }

    private void Overlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AccommodationsViewModel.Overlay_MouseLeftButtonDown();
    }

    private void ResetFilters_Click(object sender, RoutedEventArgs e)
    {
        AccommodationsViewModel.ResetFilters_Click();
    }

    public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        AccommodationsViewModel.CountryComboBox_SelectionChanged();
    }

    //Accommodation Types CheckBoxes
    public void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        AccommodationsViewModel.FilterAccommodations();
    }
    public void CheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        AccommodationsViewModel.FilterAccommodations();
    }

    //GuestNumber NumPicker
    public void GuestNumberUp_Click(object sender, RoutedEventArgs e)
    {
        AccommodationsViewModel.GuestNumberUp_Click();
    }
    public void GuestNumberDown_Click(object sender, RoutedEventArgs e)
    {
        AccommodationsViewModel.GuestNumberDown_Click();
    }
    public void GuestNumber_TextChanged(object sender, TextChangedEventArgs e)
    {
        AccommodationsViewModel?.GuestNumber_TextChanged();
    }

    //DaysOfStay NumPicker
    public void DaysOfStayUp_Click(object sender, RoutedEventArgs e)
    {
        AccommodationsViewModel.DaysOfStayUp_Click();
    }
    public void DaysOfStayDown_Click(object sender, RoutedEventArgs e)
    {
        AccommodationsViewModel.DaysOfStayDown_Click();
    }
    public void DaysOfStay_TextChanged(object sender, TextChangedEventArgs e)
    {
        AccommodationsViewModel?.DaysOfStay_TextChanged();
    }
}
