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
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using BookingApp.Application.UseCases;


namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class ToursViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tour> tours { get; set; }

        int minValueGuestNumber = 1,
            maxValueGuestNumber = 30,
            startvalueGuestNumber = 1;
            int startValueDaysOfStay { get; set; }
            int minValueDaysOfStay { get; set; }
            int maxValueDaysOfStay { get; set; }

        public Tours ToursView { get; set;}
        public ToursViewModel(User user, Tours _tours)
        {
            startValueDaysOfStay = 1;
            minValueDaysOfStay = 1;
            maxValueDaysOfStay = 30;

    ToursView = _tours;
            tours = new ObservableCollection<Tour>();
            //Update();
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
            foreach (Tour tour in TourService.GetInstance().GetAll())
            {
                tour.Location = LocationService.GetInstance().GetById(tour.LocationId);
                tour.Images = TourImageService.GetInstance().GetImagesByTourId(tour.Id);
                tour.Language = LanguageService.GetInstance().GetById(tour.LanguageId);
                tours.Add(tour);
            }
            ToursView.DaysOfStay.Text = startValueDaysOfStay.ToString();
            ToursView.GuestNumber.Text = startvalueGuestNumber.ToString();
            LoadCountries();
            LoadLanguages();
            FilteredTours = new ObservableCollection<Tour>(tours);
        }

        public void FilterTours()
        {
            string selectedCountry = ToursView.CountryComboBox.SelectedItem?.ToString();
            string selectedCity = ToursView.CityComboBox.SelectedItem?.ToString();

            int selectedGuestNumber = 0;
            int.TryParse(ToursView.GuestNumber.Text, out selectedGuestNumber);

            int selectedDaysOfStay = 0;
            int.TryParse(ToursView.DaysOfStay.Text, out selectedDaysOfStay);

            string selectedLanguage = ToursView.LanguageComboBox.SelectedItem?.ToString();

            FilteringLogic(selectedCountry, selectedCity, selectedGuestNumber, selectedDaysOfStay, selectedLanguage);

        }

        public void FilteringLogic(string selectedCountry, string selectedCity, int selectedGuestNumber, int selectedDaysOfStay, string selectedLanguage)
        {
            FilteredTours = new ObservableCollection<Tour>(
            tours.Where(tour =>
            IsTourValid(tour, selectedCountry, selectedCity, selectedGuestNumber, selectedDaysOfStay, selectedLanguage)
        )
    );
        }

        public bool IsTourValid(Tour tour, string selectedCountry, string selectedCity, int selectedGuestNumber, int selectedDaysOfStay, string selectedLanguage)
        {
            return IsLanguageValid(tour, selectedLanguage) &&
                   IsLocationValid(tour, selectedCountry, selectedCity) &&
                   IsGuestNumberValid(tour, selectedGuestNumber) &&
                   IsDaysOfStayValid(tour, selectedDaysOfStay);
        }
        public bool IsLanguageValid(Tour tour, string selectedLanguage)
        {
            return string.IsNullOrWhiteSpace(selectedLanguage) || LanguageService.GetInstance().GetById(tour.LanguageId).ToString() == selectedLanguage;
        }
        public bool IsLocationValid(Tour tour, string selectedCountry, string selectedCity)
        {
            return string.IsNullOrWhiteSpace(selectedCountry) || tour.Location.Country == selectedCountry &&
                   string.IsNullOrWhiteSpace(selectedCity) || tour.Location.City == selectedCity;
        }
        public bool IsGuestNumberValid(Tour tour, int selectedGuestNumber)
        {
            return selectedGuestNumber == 1 || selectedGuestNumber <= tour.Capacity;
        }

        public bool IsDaysOfStayValid(Tour tour, int selectedDaysOfStay)
        {
            return selectedDaysOfStay == 0 || selectedDaysOfStay <= tour.Duration;
        }

        public void LoadCountries()
        {
            List<string> listOfCountries = LocationService.GetInstance().GetCountries();
            ToursView.CountryComboBox.ItemsSource = listOfCountries;
        }
        public void LoadLanguages()
        {
            List<string> listOfLanguages = LanguageService.GetInstance().GetLanguages();
            ToursView.LanguageComboBox.ItemsSource = listOfLanguages;
        }

        public void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterTours();
        }
        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> listOfCities = LocationService.GetInstance().GetCitiesByCountry(ToursView.CountryComboBox.SelectedItem.ToString());
            ToursView.CityComboBox.ItemsSource = listOfCities;
            ToursView.CityComboBox.Focus();
            ToursView.CityComboBox.IsEnabled = true;
            FilterTours();
        }

        public void GuestNumberUp_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (ToursView.GuestNumber.Text != "") number = Convert.ToInt32(ToursView.GuestNumber.Text);
            else number = 0;
            if (number < maxValueGuestNumber)
                ToursView.GuestNumber.Text = Convert.ToString(number + 1);
        }
        public void GuestNumberDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (ToursView.GuestNumber.Text != "") number = Convert.ToInt32(ToursView.GuestNumber.Text);
            else number = 0;
            if (number > minValueGuestNumber)
                ToursView.GuestNumber.Text = Convert.ToString(number - 1);
        }

        public void GuestNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (ToursView.GuestNumber.Text != "")
                if (!int.TryParse(ToursView.GuestNumber.Text, out number)) ToursView.GuestNumber.Text = startvalueGuestNumber.ToString();
            if (number > maxValueGuestNumber) ToursView.GuestNumber.Text = maxValueGuestNumber.ToString();
            if (number < minValueGuestNumber) ToursView.GuestNumber.Text = minValueGuestNumber.ToString();
            ToursView.GuestNumber.SelectionStart = ToursView.GuestNumber.Text.Length;
            FilterTours();
        }

        public void DaysOfStayUp_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (ToursView.DaysOfStay.Text != "") number = Convert.ToInt32(ToursView.DaysOfStay.Text);
            else number = 0;
            if (number < maxValueDaysOfStay)
                ToursView.DaysOfStay.Text = Convert.ToString(number + 1);
        }
        public void DaysOfStayDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (ToursView.DaysOfStay.Text != "") number = Convert.ToInt32(ToursView.DaysOfStay.Text);
            else number = 0;
            if (number > minValueDaysOfStay)
                ToursView.DaysOfStay.Text = Convert.ToString(number - 1);
        }
        public void DaysOfStay_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (ToursView.DaysOfStay.Text != "")
                if (!int.TryParse(ToursView.DaysOfStay.Text, out number)) ToursView.DaysOfStay.Text = startValueDaysOfStay.ToString();
            if (number > maxValueDaysOfStay) ToursView.DaysOfStay.Text = maxValueDaysOfStay.ToString();
            if (number < minValueDaysOfStay) ToursView.DaysOfStay.Text = minValueDaysOfStay.ToString();
            ToursView.DaysOfStay.SelectionStart = ToursView.DaysOfStay.Text.Length;
            FilterTours();
        }
    }
}
