using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.TouristViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for TourReservation.xaml
    /// </summary>
    public partial class TourReservation : UserControl, INotifyPropertyChanged
    {
        public TourReservationViewModel tourReservationViewModel {  get; set; }
        public TourReservation(Tour detailedTour, User user)
        {
            InitializeComponent();
            tourReservationViewModel = new TourReservationViewModel(detailedTour, user, this);
            DataContext = tourReservationViewModel;

            //FindVouchers();
            //HideMessages();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void FindVouchers()
        {
            tourReservationViewModel.FindVouchers();
        }
        public void GuestNumber_TextChanged(object sender, RoutedEventArgs e)
        {
            tourReservationViewModel.GuestNumber_TextChanged(sender, e);
        }
        public void GuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            tourReservationViewModel.GuestAge_TextChanged(sender, e);
        }

        public void HideMessages()
        {
            tourReservationViewModel.HideMessages();
        }

        public bool areErrorMessagesVisible()
        {
            return tourReservationViewModel.areErrorMessagesVisible();
        }
        public void AddTourist_Click(object sender, RoutedEventArgs e)
        {
            tourReservationViewModel.AddTourist_Click(sender, e);
        }

        public void RefreshTouristDataGrid()
        {
            tourReservationViewModel.RefreshTouristDataGrid();
        }

        public void ClearInputFields()
        {
            tourReservationViewModel.ClearInputFields();
        }

        public bool areFieldsEmpty()
        {
            return tourReservationViewModel.areFieldsEmpty();
        }
        public void ReserveTour_Click(object sender, RoutedEventArgs e)
        {
            tourReservationViewModel.ReserveTour_Click(sender, e);
        }

        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourReservationViewModel.ComboBox_SelectionChanged(sender, e);
        }
    }
}
