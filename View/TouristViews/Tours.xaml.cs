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
using BookingApp.Domain.Models;
using BookingApp.Repositories;

namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for Tours.xaml
    /// </summary>
    public partial class Tours : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Tour> tours { get; set; }

        public TourRepository tourRepository { get; set; }
        public LocationRepository locationRepository { get; set; }
        public TourImageRepository tourImageRepository { get; set; }

        public LanguageRepository languageRepository { get; set; }

        int minValueGuestNumber = 1,
            maxValueGuestNumber = 30,
            startvalueGuestNumber = 1;

        int startvalueDaysOfStay = 0,
            minValueDaysOfStay = 1,
            maxValueDaysOfStay = 30;


        public Tours(User user)
        {
            InitializeComponent();
            DataContext = this;

            tours = new ObservableCollection<Tour>();
            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();
            tourImageRepository = new TourImageRepository();
            languageRepository = new LanguageRepository();
            Update();
        }

        private ObservableCollection<Tour> _filteredTours;
        public ObservableCollection<Tour> FilteredTours
        {
            get { return _filteredTours; }
            set
            {
                if (_filteredTours != value)
                {
                    _filteredTours = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Update()
        {
            foreach (Tour tour in tourRepository.GetAll())
            {
                tour.Location = locationRepository.GetById(tour.LocationId);
                tour.Images = tourImageRepository.GetImagesByTourId(tour.Id);
                tour.Language = languageRepository.GetById(tour.LanguageId);
                tours.Add(tour);
            }
            DaysOfStay.Text = startvalueDaysOfStay.ToString();
            GuestNumber.Text = startvalueGuestNumber.ToString();
            LoadCountries();
            LoadLanguages();
            FilteredTours = new ObservableCollection<Tour>(tours);
        }

        private void FilterTours()
        {
            string selectedCountry = CountryComboBox.SelectedItem?.ToString();
            string selectedCity = CityComboBox.SelectedItem?.ToString();

            int selectedGuestNumber = 0;
            int.TryParse(GuestNumber.Text, out selectedGuestNumber);

            int selectedDaysOfStay = 0;
            int.TryParse(DaysOfStay.Text, out selectedDaysOfStay);

            string selectedLanguage = LanguageComboBox.SelectedItem?.ToString();

            FilteringLogic(selectedCountry, selectedCity, selectedGuestNumber, selectedDaysOfStay, selectedLanguage);

        }

        private void FilteringLogic(string selectedCountry, string selectedCity, int selectedGuestNumber, int selectedDaysOfStay, string selectedLanguage)
        {
            FilteredTours = new ObservableCollection<Tour>(
            tours.Where(tour =>
            IsTourValid(tour, selectedCountry, selectedCity, selectedGuestNumber, selectedDaysOfStay, selectedLanguage)
        )
    );
        }

        private bool IsTourValid(Tour tour, string selectedCountry, string selectedCity, int selectedGuestNumber, int selectedDaysOfStay, string selectedLanguage)
        {
            return IsLanguageValid(tour, selectedLanguage) &&
                   IsLocationValid(tour, selectedCountry, selectedCity) &&
                   IsGuestNumberValid(tour, selectedGuestNumber) &&
                   IsDaysOfStayValid(tour, selectedDaysOfStay);
        }
        private bool IsLanguageValid(Tour tour, string selectedLanguage)
        {
            return string.IsNullOrWhiteSpace(selectedLanguage) || languageRepository.GetById(tour.LanguageId).ToString() == selectedLanguage;
        }
        private bool IsLocationValid(Tour tour, string selectedCountry, string selectedCity)
        {
            return string.IsNullOrWhiteSpace(selectedCountry) || tour.Location.Country == selectedCountry &&
                   string.IsNullOrWhiteSpace(selectedCity) || tour.Location.City == selectedCity;
        }
        private bool IsGuestNumberValid(Tour tour, int selectedGuestNumber)
        {
            return selectedGuestNumber == 1 || selectedGuestNumber <= tour.Capacity;
        }

        private bool IsDaysOfStayValid(Tour tour, int selectedDaysOfStay)
        {
            return selectedDaysOfStay == 0 ||selectedDaysOfStay <= tour.Duration;
        }

        private void LoadCountries()
        {
            List<string> listOfCountries = locationRepository.GetCountries();
            CountryComboBox.ItemsSource = listOfCountries;
        }
        private void LoadLanguages()
        {
            List<string> listOfLanguages = languageRepository.GetLanguages();
            LanguageComboBox.ItemsSource = listOfLanguages;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterTours();
        }
        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> listOfCities = locationRepository.GetCitiesByCountry(CountryComboBox.SelectedItem.ToString());
            CityComboBox.ItemsSource = listOfCities;
            CityComboBox.Focus();
            CityComboBox.IsEnabled = true;
            FilterTours();
        }

        private void GuestNumberUp_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (GuestNumber.Text != "") number = Convert.ToInt32(GuestNumber.Text);
            else number = 0;
            if (number < maxValueGuestNumber)
                GuestNumber.Text = Convert.ToString(number + 1);
        }
        private void GuestNumberDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (GuestNumber.Text != "") number = Convert.ToInt32(GuestNumber.Text);
            else number = 0;
            if (number > minValueGuestNumber)
                GuestNumber.Text = Convert.ToString(number - 1);
        }

        private void GuestNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (GuestNumber.Text != "")
                if (!int.TryParse(GuestNumber.Text, out number)) GuestNumber.Text = startvalueGuestNumber.ToString();
            if (number > maxValueGuestNumber) GuestNumber.Text = maxValueGuestNumber.ToString();
            if (number < minValueGuestNumber) GuestNumber.Text = minValueGuestNumber.ToString();
            GuestNumber.SelectionStart = GuestNumber.Text.Length;
            FilterTours();
        }

        private void DaysOfStayUp_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (DaysOfStay.Text != "") number = Convert.ToInt32(DaysOfStay.Text);
            else number = 0;
            if (number < maxValueDaysOfStay)
                DaysOfStay.Text = Convert.ToString(number + 1);
        }
        private void DaysOfStayDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (DaysOfStay.Text != "") number = Convert.ToInt32(DaysOfStay.Text);
            else number = 0;
            if (number > minValueDaysOfStay)
                DaysOfStay.Text = Convert.ToString(number - 1);
        }
        private void DaysOfStay_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (DaysOfStay.Text != "")
                if (!int.TryParse(DaysOfStay.Text, out number)) DaysOfStay.Text = startvalueDaysOfStay.ToString();
            if (number > maxValueDaysOfStay) DaysOfStay.Text = maxValueDaysOfStay.ToString();
            if (number < minValueDaysOfStay) DaysOfStay.Text = minValueDaysOfStay.ToString();
            DaysOfStay.SelectionStart = DaysOfStay.Text.Length;
            FilterTours();
        }

    }
}
