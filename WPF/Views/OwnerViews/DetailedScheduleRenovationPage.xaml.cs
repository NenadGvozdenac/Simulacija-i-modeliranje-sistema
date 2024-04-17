using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.Views.OwnerViews;
public partial class DetailedScheduleRenovationPage : Page
{
    public DetailedScheduleRenovatonViewModel DetailedScheduleRenovationViewModel { get; set; }
    public DetailedScheduleRenovationPage(Accommodation accommodation)
    {
        InitializeComponent();
        DetailedScheduleRenovationViewModel = new(this, accommodation);
        DataContext = DetailedScheduleRenovationViewModel;
    }

    private void Back_Click(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }

    private void DatePicker_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is DatePicker datePicker)
        {
            datePicker.DisplayDateStart = DateTime.Now;
        }
    }

    private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        DetailedScheduleRenovationViewModel.EndDate = DetailedScheduleRenovationViewModel.StartDate;
        EndDatePicker.DisplayDateStart = StartDatePicker.SelectedDate;
    }
}
