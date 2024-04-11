using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using BookingApp.WPF.ViewModels.GuestViewModels;

namespace BookingApp.WPF.Views.GuestViews;

public partial class AccommodationDetails : UserControl
{
    public AccommodationDetailsViewModel AccommodationDetailsViewModel { get; set; }
    public AccommodationDetails(Accommodation detailedaccomodation, User user)
    {
        InitializeComponent();
        AccommodationDetailsViewModel = new AccommodationDetailsViewModel(this, detailedaccomodation, user);
        DataContext = AccommodationDetailsViewModel;
    }

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        AccommodationDetailsViewModel.GoBack_Click(sender, e);
    }
    private void FreeDatesCheck_Click(object sender, RoutedEventArgs e)
    {
        AccommodationDetailsViewModel.FreeDatesCheck_Click();
    }
    private void ConfrimReservation_Click(object sender, RoutedEventArgs e)
    {
        AccommodationDetailsViewModel.ConfrimReservation_Click();
    }
    //Design Functions
    private void DatePickerCantWrite(object sender, KeyEventArgs e)
    {
        AccommodationDetailsViewModel.DatePickerCantWrite(sender,e);
    }

    private void FirstDateChanged(object sender, SelectionChangedEventArgs e)
    {
        AccommodationDetailsViewModel.FirstDateChanged();
    }

    private void LastDateChanged(object sender, SelectionChangedEventArgs e)
    {
        AccommodationDetailsViewModel.LastDateChanged();
    }

    private void DaysOfStayUp_Click(object sender, RoutedEventArgs e)
    {
        AccommodationDetailsViewModel.DaysOfStayUp_Click();
    }

    private void DaysOfStayDown_Click(object sender, RoutedEventArgs e)
    {
        AccommodationDetailsViewModel.DaysOfStayDown_Click();
    }

    private void DaysOfStay_TextChanged(object sender, TextChangedEventArgs e)
    {
        AccommodationDetailsViewModel?.DaysOfStay_TextChanged();
    }

    private void GuestNumberUp_Click(object sender, RoutedEventArgs e)
    {
        AccommodationDetailsViewModel.GuestNumberUp_Click();
    }

    private void GuestNumberDown_Click(object sender, RoutedEventArgs e)
    {
        AccommodationDetailsViewModel.GuestNumberDown_Click();
    }

    private void GuestNumber_TextChanged(object sender, TextChangedEventArgs e)
    {
        AccommodationDetailsViewModel?.GuestNumber_TextChanged();
    }
}
