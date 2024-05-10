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
        private DateTime _beginDate;
        private DateTime _endDate;

        public DateTime BeginDate
        {
            get { return _beginDate; }
            set
            {
                _beginDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
        private string _selectedCountry;

        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged();
            }
        }
        private string _selectedCity;
        public string SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged();
            }
        }
        public List<Tourist> Tourists { get; set; }
        public string description {  get; set; }
        public User user {  get; set; }
        public RequestTour(User _user)
        {
            InitializeComponent();
            DataContext = this;
            _tourists = new ObservableCollection<Tourist>();
            Tourists = new List<Tourist>();
            user = _user;
            LoadCountries();
            LoadLanguages();
            BeginDate = DateTime.Now;
            EndDate = DateTime.Now;
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
            SelectedCountry = CountryComboBox.SelectedItem?.ToString();

        }
        public void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedCity = CityComboBox.SelectedItem?.ToString();
        }
        public void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedLanguage = LanguageComboBox.SelectedItem?.ToString();
        }

        public void GetTourInfo()
        {
            //selectedCountry = CountryComboBox.SelectedItem?.ToString();
            //selectedCity = CityComboBox.SelectedItem?.ToString();
            //selectedLanguage = LanguageComboBox.SelectedItem?.ToString();
            description = DescriptionBox.Text;

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
            foreach(Tourist t in _tourists)
            {
                Tourists.Add(t);
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

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            GetTourInfo();

            //TourRequest tourRequest = new TourRequest(id, user.Id, LocationService.GetInstance().GetLocationByCityAndCountry(SelectedCity, SelectedCountry).Id, description, LanguageService.GetInstance().GetLanguageByName(SelectedLanguage).Id, BeginDate, EndDate);
            TourRequest tourRequest = new TourRequest();
            tourRequest.Id = TourRequestService.GetInstance().NextId();
            tourRequest.UserId = user.Id;
            tourRequest.LocationId = LocationService.GetInstance().GetLocationByCityAndCountry(SelectedCity, SelectedCountry).Id;
            tourRequest.Description = description;
            tourRequest.LanguageId = LanguageService.GetInstance().GetLanguageByName(SelectedLanguage).Id;
            tourRequest.BeginDate = BeginDate;
            tourRequest.EndDate = EndDate;
            tourRequest.Tourists = Tourists;
            tourRequest.Status = "Pending";

            TourRequestService.GetInstance().Add(tourRequest);
            MessageBox.Show("Uspesno");
        }
    }
}
