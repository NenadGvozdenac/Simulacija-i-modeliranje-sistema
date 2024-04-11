using BookingApp.Domain.Models;
using BookingApp.Repositories;
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

/// <summary>
/// Interaction logic for RescheduleAccommodation.xaml
/// </summary>
public partial class RescheduleAccommodation : UserControl
{
    public RescheduleAccommodationViewModel RescheduleAccommodationViewModel { get; set; }

    public event EventHandler ChangedMind;
    public event EventHandler SendRequestRefresh;
    public RescheduleAccommodation(AccommodationReservation _selectedReservation, AccommodationRepository accommodationRepository, AccommodationReservationMovingRepository accommodationReservationMovingRepository)
    {
        InitializeComponent();
        RescheduleAccommodationViewModel = new RescheduleAccommodationViewModel(this, _selectedReservation, accommodationRepository, accommodationReservationMovingRepository);
        DataContext = RescheduleAccommodationViewModel;
    }
    private void ChangedMind_Click(object sender, RoutedEventArgs e)
    {
        RescheduleAccommodationViewModel.ChangedMind_Click();
    }

    private void SendRequest_Click(object sender, RoutedEventArgs e)
    {
        RescheduleAccommodationViewModel.SendRequest_Click();
    }

    private void FirstDateChanged(object sender, SelectionChangedEventArgs e)
    {
        RescheduleAccommodationViewModel.FirstDateChanged();
    }
    private void LastDateChanged(object sender, SelectionChangedEventArgs e)
    {
        RescheduleAccommodationViewModel.LastDateChanged();
    }

    private void DatePickerCantWrite(object sender, KeyEventArgs e)
    {
        RescheduleAccommodationViewModel.DatePickerCantWrite(sender, e);
    }
}
