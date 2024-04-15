using System;
using System.Collections.Generic;
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
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.View.PathfinderViews;
using BookingApp.WPF.ViewModels.TouristViewModels;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for TourDatesUserControl.xaml
    /// </summary>
    public partial class TourDatesUserControl : UserControl
    {
        public TourDatesUserControlViewModel tourDatesViewModel {  get; set; }
        public TourDatesUserControl(User user, Tour detailedTour, int guestNumber, List<Tourist> tourists, TouristRepository touristRepository, TouristReservationRepository touristReservationRepository, TourStartTimeRepository tourStartTimeRepo, TourVoucher voucher, TourVoucherRepository tourVoucherRepository)
        {
            InitializeComponent();
            tourDatesViewModel = new TourDatesUserControlViewModel(user, detailedTour, guestNumber, tourists, touristRepository, touristReservationRepository, tourStartTimeRepo, voucher, tourVoucherRepository, this);
            DataContext = tourDatesViewModel;            


            findTourDates();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void findTourDates()
        {
            tourDatesViewModel.findTourDates();
        }

        private void TourDatesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourDatesViewModel.TourDatesDataGrid_SelectionChanged(sender, e);
        }

        private void TourReservation(TourStartTime tourStartTime)
        {
            tourDatesViewModel.TourReservation(tourStartTime);
        }

        private bool isTourGuestNumberValid()
        {
            return tourDatesViewModel.isTourGuestNumberValid();
        }
        private void UpdateTourStartTime(TourStartTime tourStartTime)
        {
            tourDatesViewModel.UpdateTourStartTime(tourStartTime);
        }
        private void CreateReservation(Tourist tourist, TourStartTime tourStartTime)
        {
            tourDatesViewModel.CreateReservation(tourist, tourStartTime);
        }

        private void UseVoucher()
        {
            tourDatesViewModel.UseVoucher();
        }
        private void AddTourist(Tourist tourist)
        {
            tourDatesViewModel.AddTourist(tourist);
        }
        private void BackToTouristDetails_Click(object sender, RoutedEventArgs e)
        {
            tourDatesViewModel.BackToTouristDetails_Click(sender, e);
        }

        private void AlternativeTours_Click(object sender, RoutedEventArgs e)
        {
            tourDatesViewModel.AlternativeTours_Click(sender, e);
        }
    }
}
