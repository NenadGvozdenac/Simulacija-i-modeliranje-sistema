using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BookingApp.WPF.Views.GuestViews;
using BookingApp.Domain.RepositoryInterfaces;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TouristDetailsViewModel
    {
        public Tours ToursUserControl;
        public event EventHandler ReturnRequest;
        public User _user;
        public LocationRepository locationRepository;
        public ObservableCollection<Tourist> _tourists { get; set; }

        public TouristRepository touristRepository;
        public TouristDetails TouristDetailsView {  get; set; }

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
                    TouristDetailsView.enterValidAgeMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    TouristDetailsView.enterValidAgeMessage.Visibility = Visibility.Hidden;
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
                    TouristDetailsView.numberHigherMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    TouristDetailsView.numberHigherMessage.Visibility = Visibility.Hidden;
                    TouristDetailsView.enterValidGuestNumber.Visibility = Visibility.Hidden;

                }
            }
        }

        public Frame TouristWindowFrame;

        public Tour selectedTour { get; set; }
        public TourVoucher Voucher {  get; set; }
        public ObservableCollection<TourVoucher> vouchers { get; set; }
        public TourVoucherRepository tourVoucherRepository { get; set; }
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
        public TouristDetailsViewModel(Tour detailedTour, User user, TouristDetails touristDetails, Frame touristWindowFrame)
        {
            _user = user;
            tourVoucherRepository = new TourVoucherRepository();
            vouchers = new ObservableCollection<TourVoucher>();
            TouristDetailsView = touristDetails;
            TouristWindowFrame = touristWindowFrame;
            locationRepository = new LocationRepository();
            selectedTour = detailedTour;
            ToursUserControl = new Tours(user);
            _tourists = new ObservableCollection<Tourist>();
            touristRepository = new TouristRepository();

            TouristDetailsView.tourNameTextBlock.Text = selectedTour.Name;
            TouristDetailsView.tourCountryTextBlock.Text = locationRepository.GetById(selectedTour.LocationId).Country;
            TouristDetailsView.tourCityTextBlock.Text = locationRepository.GetById(selectedTour.LocationId).City;

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
            foreach (TourVoucher voucher in tourVoucherRepository.GetAll())
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
            if (!int.TryParse(TouristDetailsView.GuestNumberText.Text, out int number) && TouristDetailsView.GuestNumberText.Text != "")
            {
                TouristDetailsView.enterValidGuestNumber.Visibility = Visibility.Visible;
            }
            else
            {
                GuestNumber = number;
                TouristDetailsView.enterValidGuestNumber.Visibility = Visibility.Hidden;
            }
        }

        /*private void Return_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.TouristWindowFrame.Content = mainWindow.ToursUserControl;
            }
            (Window.GetWindow(TouristDetailsView) as TouristMainWindow).Tours_Click(sender, e);

        }*/

        public void GuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(TouristDetailsView.GuestAgeText.Text, out int result) && TouristDetailsView.GuestAgeText.Text != "")
            {
                TouristDetailsView.enterValidAgeMessage.Visibility = Visibility.Visible;

            }
            else
            {
                GuestAge = result;
            }
        }

        public void HideMessages()
        {
            TouristDetailsView.enterAllFieldsMessage.Visibility = Visibility.Hidden;
            TouristDetailsView.enterValidAgeMessage.Visibility = Visibility.Hidden;
            TouristDetailsView.numberHigherMessage.Visibility = Visibility.Hidden;
            TouristDetailsView.increaseNumberText.Visibility = Visibility.Hidden;
            TouristDetailsView.addTouristMessage.Visibility = Visibility.Hidden;
            TouristDetailsView.enterValidAgeMessage.Visibility = Visibility.Hidden;

        }

        public bool areErrorMessagesVisible()
        {
            if (TouristDetailsView.increaseNumberText.Visibility == Visibility.Visible || TouristDetailsView.enterValidGuestNumber.Visibility == Visibility.Visible || TouristDetailsView.numberHigherMessage.Visibility == Visibility.Visible || TouristDetailsView.enterValidAgeMessage.Visibility == Visibility.Visible)
            {
                return true;
            }
            return false;
        }
        public void AddTourist_Click(object sender, RoutedEventArgs e)
        {
            if (areErrorMessagesVisible())
            {
                return;
            }
            HideMessages();
            if (!areFieldsEmpty())
            {
                name = TouristDetailsView.GuestName.Text;
                surname = TouristDetailsView.GuestSurname.Text;

                Tourist tourist = new Tourist
                {
                    Name = name,
                    Surname = surname,
                    Age = GuestAge
                };

                _tourists.Add(tourist);
                TouristDetailsView.TouristDataGrid.ItemsSource = null;
                TouristDetailsView.TouristDataGrid.ItemsSource = _tourists;
                
                TouristDetailsView.GuestName.Text = "";
                TouristDetailsView.GuestSurname.Text = "";
                TouristDetailsView.GuestAgeText.Text = "";
            }
            else
            {
                TouristDetailsView.enterAllFieldsMessage.Visibility = Visibility.Visible;
                return;
            }
            if (GuestNumber < _tourists.Count() + 1)
            {
                TouristDetailsView.increaseNumberText.Visibility = Visibility.Visible;
                return;
            }
        }

        public bool areFieldsEmpty()
        {
            return TouristDetailsView.GuestNumberText.Text == "" || TouristDetailsView.GuestName.Text == "" || TouristDetailsView.GuestSurname.Text == "" || TouristDetailsView.GuestAgeText.Text == "";
        }
        public void ReserveTour_Click(object sender, RoutedEventArgs e)
        {
            if (_tourists.Count() == 0 || _tourists.Count() < GuestNumber)
            {
                TouristDetailsView.addTouristMessage.Visibility = Visibility.Visible;
                return;
            }

            /*if (_tourists.Count() < GuestNumber)
            {
                TouristDetailsView.addTouristMessage.Visibility = Visibility.Visible;
                return;
            }*/
            List<Tourist> tourists = new List<Tourist>();
            foreach (Tourist t in _tourists)
            {
                tourists.Add(t);
            }
            //(Window.GetWindow(TouristDetailsView) as TouristMainWindow).Accommodations_Click(sender, e);

            Window parentWindow = Window.GetWindow(TouristDetailsView);

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
