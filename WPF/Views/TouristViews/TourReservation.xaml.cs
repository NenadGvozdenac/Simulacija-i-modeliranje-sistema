using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
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
    public partial class TourReservation : UserControl
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
                    enterValidAgeMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    enterValidAgeMessage.Visibility = Visibility.Hidden;
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
                    numberHigherMessage.Visibility = Visibility.Visible;

                }
                else
                {
                    numberHigherMessage.Visibility = Visibility.Hidden;
                    enterValidGuestNumber.Visibility = Visibility.Hidden;

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
        public TourReservation(Tour detailedTour, User user)
        {
            InitializeComponent();
            DataContext = this;
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
            if (!int.TryParse(GuestNumberText.Text, out int number) && GuestNumberText.Text != "")
            {
                enterValidGuestNumber.Visibility = Visibility.Visible;
            }
            else
            {
                GuestNumber = number;
                enterValidGuestNumber.Visibility = Visibility.Hidden;
            }
        }
        public void GuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(GuestAgeText.Text, out int result) && GuestAgeText.Text != "")
            {
                enterValidAgeMessage.Visibility = Visibility.Visible;

            }
            else
            {
                GuestAge = result;
            }
        }

        public void HideMessages()
        {
            enterAllFieldsMessage.Visibility = Visibility.Hidden;
            enterValidAgeMessage.Visibility = Visibility.Hidden;
            numberHigherMessage.Visibility = Visibility.Hidden;
            increaseNumberText.Visibility = Visibility.Hidden;
            addTouristMessage.Visibility = Visibility.Hidden;
        }

        public bool areErrorMessagesVisible()
        {
            if (increaseNumberText.Visibility == Visibility.Visible || enterValidGuestNumber.Visibility == Visibility.Visible || numberHigherMessage.Visibility == Visibility.Visible || enterValidAgeMessage.Visibility == Visibility.Visible)
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
                enterAllFieldsMessage.Visibility = Visibility.Visible;
                return;
            }

            string name = GuestName.Text;
            string surname = GuestSurname.Text;
            int age = GuestAge;

            _tourists.Add(new Tourist { Name = name, Surname = surname, Age = age });

            RefreshTouristDataGrid();
            ClearInputFields();

            if (GuestNumber < _tourists.Count() + 1)
            {
                increaseNumberText.Visibility = Visibility.Visible;
            }
        }

        private void RefreshTouristDataGrid()
        {
            TouristDataGrid.ItemsSource = null;
            TouristDataGrid.ItemsSource = _tourists;
        }

        private void ClearInputFields()
        {
            GuestName.Text = "";
            GuestSurname.Text = "";
            GuestAgeText.Text = "";
        }

        public bool areFieldsEmpty()
        {
            return GuestNumberText.Text == "" || GuestName.Text == "" || GuestSurname.Text == "" || GuestAgeText.Text == "";
        }
        public void ReserveTour_Click(object sender, RoutedEventArgs e)
        {
            if (_tourists.Count() == 0 || _tourists.Count() < GuestNumber)
            {
                addTouristMessage.Visibility = Visibility.Visible;
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

            Window parentWindow = Window.GetWindow(this);

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
