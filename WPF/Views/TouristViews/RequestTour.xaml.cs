using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
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
    /// Interaction logic for RequestTour.xaml
    /// </summary>
    public partial class RequestTour : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Tourist> _tourists { get; set; }

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
                /*if (_guestAge <= 0 || _guestAge > 150)
                {
                    enterValidAgeMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    enterValidAgeMessage.Visibility = Visibility.Hidden;
                }*/
            }
        }

        public int GuestNumber
        {
            get { return _guestNumber; }
            set
            {
                _guestNumber = value;
                /*if (selectedTour != null && (_guestNumber > selectedTour.Capacity || _guestNumber <= 0))
                {
                    //numberHigherMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    //numberHigherMessage.Visibility = Visibility.Hidden;
                    //enterValidGuestNumber.Visibility = Visibility.Hidden;

                }*/
            }
        }
        public User user {  get; set; }
        public RequestTour(User _user)
        {
            InitializeComponent();
            DataContext = this;
            _tourists = new ObservableCollection<Tourist>();
            user = _user;
            LoadCountries();
            LoadLanguages();
        }

        public void LoadCountries()
        {
            List<string> listOfCountries = LocationService.GetInstance().GetCountries();
            CountryComboBox.ItemsSource = listOfCountries;
        }
        public void LoadLanguages()
        {
            List<string> listOfLanguages = LanguageService.GetInstance().GetLanguages();
            LanguageComboBox.ItemsSource = listOfLanguages;
        }

        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> listOfCities = LocationService.GetInstance().GetCitiesByCountry(CountryComboBox.SelectedItem.ToString());
            CityComboBox.ItemsSource = listOfCities;
            CityComboBox.Focus();
            CityComboBox.IsEnabled = true;
        }

        public void GetTourInfo()
        {
            string selectedCountry = CountryComboBox.SelectedItem?.ToString();
            string selectedCity = CityComboBox.SelectedItem?.ToString();
            string selectedLanguage = LanguageComboBox.SelectedItem?.ToString();
            string description = DescriptionBox.Text;

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddTourist_Click(object sender, RoutedEventArgs e)
        {
            string name = GuestName.Text;
            string surname = GuestSurname.Text;
            int age = GuestAge;

            _tourists.Add(new Tourist { Name = name, Surname = surname, Age = age });
            RefreshTouristDataGrid();
            ClearInputFields();
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
    }
}
