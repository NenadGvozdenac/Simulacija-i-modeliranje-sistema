using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TourDatesUserControlViewModel
    {
        private Tour selectedTour { get; set; }
        private int guestNumber { get; set; }
        public List<TourStartTime> tourStartTimes { get; set; }
        public TourStartTime selectedTourStartTime { get; set; }
        public List<Tourist> tourists { get; set; }
        public User user { get; set; }
        public TourVoucher tourVoucher { get; set; }
        public TourDatesUserControl TourDatesUserControl { get; set; }
        public TourDatesUserControlViewModel(User user, Tour detailedTour, int guestNumber, List<Tourist> tourists, TourVoucher voucher, TourDatesUserControl tourDates)
        {
            TourDatesUserControl = tourDates;
            this.user = user;
            selectedTour = detailedTour;
            tourStartTimes = new List<TourStartTime>();
            selectedTourStartTime = new TourStartTime();
            this.guestNumber = guestNumber;
            this.tourists = tourists;
            tourVoucher = voucher;
            TourDatesUserControl.touristsAddedMessage.Visibility = Visibility.Hidden;


            findTourDates();
            TourDatesUserControl.tourDatesDataGrid.ItemsSource = tourStartTimes;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void findTourDates()
        {
            foreach (TourStartTime tourStartTime in TourStartTimeService.GetInstance().GetAll())
            {
                if (tourStartTime.TourId == selectedTour.Id)
                {
                    tourStartTimes.Add(tourStartTime);
                }
            }
        }

        public void TourDatesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TourDatesUserControl.tourDatesDataGrid.SelectedItem != null)
            {
                TourDatesUserControl.noFreeSpacesMessage.Visibility = Visibility.Hidden;
                TourDatesUserControl.freeSpaces.Visibility = Visibility.Hidden;
                TourDatesUserControl.alternativeTours.Visibility = Visibility.Hidden;

                if (TourDatesUserControl.touristsAddedMessage.Visibility == Visibility.Visible)
                {
                    return;
                }
                selectedTourStartTime = (TourStartTime)TourDatesUserControl.tourDatesDataGrid.SelectedItem;
                TourReservation(selectedTourStartTime);

            }
        }

        public void TourReservation(TourStartTime tourStartTime)
        {
            if (!isTourGuestNumberValid())
            {
                return;
            }
            Window parentWindow = Window.GetWindow(TourDatesUserControl);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowReservationReview(user, selectedTour, guestNumber, tourists, tourVoucher, tourStartTime);
            }
            /*List<Tourist> touristList = new List<Tourist>();
            foreach (Tourist t in tourists)
            {
                touristList.Add(t);
            }
            foreach (Tourist tourist in touristList)
            {
                tourist.Id = TouristService.GetInstance().NextId();
                AddTourist(tourist);

                CreateReservation(tourist, tourStartTime);

                UpdateTourStartTime(tourStartTime);
            }
            TourDatesUserControl.touristsAddedMessage.Visibility = Visibility.Visible;
            return;*/
        }

        public bool isTourGuestNumberValid()
        {
            if (tourists.Count() >= (selectedTour.Capacity - selectedTourStartTime.Guests) + 1)
            {
                TourDatesUserControl.noFreeSpacesMessage.Visibility = Visibility.Visible;
                TourDatesUserControl.freeSpaces.Visibility = Visibility.Visible;
                if (selectedTour.Capacity - selectedTourStartTime.Guests == 0)
                {
                    TourDatesUserControl.alternativeTours.Visibility = Visibility.Visible;

                }
                TourDatesUserControl.touristsAddedMessage.Visibility = Visibility.Hidden;
                TourDatesUserControl.freeSpaces.Text = (selectedTour.Capacity - selectedTourStartTime.Guests).ToString();
                return false;

            }
            return true;
        }
        public void UpdateTourStartTime(TourStartTime tourStartTime)
        {
            tourStartTime.Guests += tourists.Count();
            TourStartTimeService.GetInstance().Update(tourStartTime);
            tourists.Clear();
        }
        public void CreateReservation(Tourist tourist, TourStartTime tourStartTime)
        {
            TouristReservation touristReservation = new TouristReservation();
            touristReservation.Id = TourReservationService.GetInstance().NextId();
            touristReservation.Id_Tourist = tourist.Id;
            touristReservation.Id_TourTime = tourStartTime.Id;
            touristReservation.CheckpointId = -1;
            touristReservation.UserId = user.Id;
            TourReservationService.GetInstance().Add(touristReservation);
            if (tourVoucher != null)
            {
                UseVoucher();
            }
        }

        public void UseVoucher()
        {
            TourVoucherService.GetInstance().Delete(tourVoucher.Id);
        }
        public void AddTourist(Tourist tourist)
        {
            foreach (Tourist t in TouristService.GetInstance().GetAll())
            {
                if (t.Id == tourist.Id)
                {
                    return;
                }
            }
            TouristService.GetInstance().Add(tourist);
        }
        public void BackToTouristDetails_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(TourDatesUserControl);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowTourDetails(selectedTour.Id);
            }
        }

        public void AlternativeTours_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(TourDatesUserControl);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowAlternativeTours(selectedTour.LocationId, selectedTour);

            }
        }
    }
}
