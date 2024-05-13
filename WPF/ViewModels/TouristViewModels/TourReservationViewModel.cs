using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using BookingApp.Application.UseCases;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TourReservationViewModel : INotifyPropertyChanged
    {
        public Tours ToursUserControl;
        public event EventHandler ReturnRequest;
        public User _user;
        public ObservableCollection<Tourist> _tourists { get; set; }

        public TouristDetails TouristDetailsView { get; set; }

        private int _guestNumber;
        private string name;
        private string surname;
        private int _guestAge;
        public int GuestAge
        {
            get { return _guestAge; }
            set
            {
                _guestAge = value;
                if (_guestAge <= 0 || _guestAge > 150)
                {
                    _tourReservation.enterValidAgeMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    _tourReservation.enterValidAgeMessage.Visibility = Visibility.Hidden;
                }
            }
        }

        public int GuestNumber
        {
            get { return _guestNumber; }
            set
            {
                _guestNumber = value;
                if (selectedTour != null && (_guestNumber > selectedTour.Capacity || _guestNumber <= 0))
                {
                    _tourReservation.numberHigherMessage.Visibility = Visibility.Visible;

                }
                else
                {
                    _tourReservation.numberHigherMessage.Visibility = Visibility.Hidden;
                    _tourReservation.enterValidGuestNumber.Visibility = Visibility.Hidden;

                }
            }
        }

        public Frame TouristWindowFrame;

        public Tour selectedTour { get; set; }
        public TourVoucher Voucher { get; set; }
        public ObservableCollection<TourVoucher> vouchers { get; set; }
        private ObservableCollection<TourVoucher> _vouchers;
        public ObservableCollection<TourVoucher> Vouchers
        {
            get { return _vouchers; }
            set
            {
                if (_vouchers != value)
                {
                    _vouchers = value;
                    OnPropertyChanged();
                }
            }
        }
        public TourReservation _tourReservation {  get; set; }
        public TourReservationViewModel(Tour detailedTour, User user, TourReservation tourReservation)
        {
            _tourReservation = tourReservation;
            _user = user;
            vouchers = new ObservableCollection<TourVoucher>();
            selectedTour = detailedTour;
            ToursUserControl = new Tours(user);
            _tourists = new ObservableCollection<Tourist>();

            FindVouchers();
            HideMessages();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void FindVouchers()
        {
            foreach (TourVoucher voucher in TourVoucherService.GetInstance().GetAll())
            {
                if (voucher.TouristId == _user.Id)
                {
                    vouchers.Add(voucher);
                }
            }
            Vouchers = new ObservableCollection<TourVoucher>(vouchers);

        }
        public void GuestNumber_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(_tourReservation.GuestNumberText.Text, out int number) && _tourReservation.GuestNumberText.Text != "")
            {
                _tourReservation.enterValidGuestNumber.Visibility = Visibility.Visible;
            }
            else
            {
                GuestNumber = number;
                _tourReservation.enterValidGuestNumber.Visibility = Visibility.Hidden;
            }
        }
        public void GuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(_tourReservation.GuestAgeText.Text, out int result) && _tourReservation.GuestAgeText.Text != "")
            {
                _tourReservation.enterValidAgeMessage.Visibility = Visibility.Visible;

            }
            else
            {
                GuestAge = result;
            }
        }

        public void HideMessages()
        {
            _tourReservation.enterAllFieldsMessage.Visibility = Visibility.Hidden;
            _tourReservation.enterValidAgeMessage.Visibility = Visibility.Hidden;
            _tourReservation.numberHigherMessage.Visibility = Visibility.Hidden;
            _tourReservation.increaseNumberText.Visibility = Visibility.Hidden;
            _tourReservation.addTouristMessage.Visibility = Visibility.Hidden;
        }

        public bool areErrorMessagesVisible()
        {
            if (_tourReservation.increaseNumberText.Visibility == Visibility.Visible || _tourReservation.enterValidGuestNumber.Visibility == Visibility.Visible || _tourReservation.numberHigherMessage.Visibility == Visibility.Visible || _tourReservation.enterValidAgeMessage.Visibility == Visibility.Visible)
            {
                return true;
            }
            return false;
        }
        public void AddTourist_Click(object sender, RoutedEventArgs e)
        {
            if (areErrorMessagesVisible()) return;

            HideMessages();

            if (areFieldsEmpty())
            {
                _tourReservation.enterAllFieldsMessage.Visibility = Visibility.Visible;
                return;
            }

            string name = _tourReservation.GuestName.Text;
            string surname = _tourReservation.GuestSurname.Text;
            int age = GuestAge;

            _tourists.Add(new Tourist { Name = name, Surname = surname, Age = age });

            RefreshTouristDataGrid();
            ClearInputFields();

            if (GuestNumber < _tourists.Count() + 1)
            {
                _tourReservation.increaseNumberText.Visibility = Visibility.Visible;
            }
        }

        public void RefreshTouristDataGrid()
        {
            _tourReservation.TouristDataGrid.ItemsSource = null;
            _tourReservation.TouristDataGrid.ItemsSource = _tourists;
        }

        public void ClearInputFields()
        {
            _tourReservation.GuestName.Text = "";
            _tourReservation.GuestSurname.Text = "";
            _tourReservation.GuestAgeText.Text = "";
        }

        public bool areFieldsEmpty()
        {
            return _tourReservation.GuestNumberText.Text == "" || _tourReservation.GuestName.Text == "" || _tourReservation.GuestSurname.Text == "" || _tourReservation.GuestAgeText.Text == "";
        }
        public void ReserveTour_Click(object sender, RoutedEventArgs e)
        {
            if (_tourists.Count() == 0 || _tourists.Count() < GuestNumber)
            {
                _tourReservation.addTouristMessage.Visibility = Visibility.Visible;
                return;
            }

            List<Tourist> tourists = new List<Tourist>();
            foreach (Tourist t in _tourists)
            {
                tourists.Add(t);
            }

            Window parentWindow = Window.GetWindow(_tourReservation);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowTourDates(selectedTour, GuestNumber, tourists, Voucher);
                _tourists.Clear();
            }
        }

        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                Voucher = comboBox.SelectedItem as TourVoucher;
            }
        }
    }
}
