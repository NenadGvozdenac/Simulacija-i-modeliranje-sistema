using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class TourRequestWindowViewModel
    {

        public TourRequestsWindow tourRequestsWindow { get; set; }

        public User user { get; set; }

        public TourRequestControl tourRequestControl { get; set; }
        public TourRequestWindowViewModel(TourRequestsWindow _tourRequestsWindow, User _user)
        {
            tourRequestsWindow = _tourRequestsWindow;
            user = _user;
            tourRequestControl = new TourRequestControl(user);
            tourRequestsWindow.RequestsContainer.Children.Add(tourRequestControl);
            LoadCountries();
            LoadLanguages();
        }

        public void LoadCountries()
        {
            List<string> listOfCountries = LocationService.GetInstance().GetCountries();
            tourRequestsWindow.CountryBox.ItemsSource = listOfCountries;
        }
        public void LoadLanguages()
        {
            List<string> listOfLanguages = LanguageService.GetInstance().GetLanguages();
            tourRequestsWindow.LanguageBox.ItemsSource = listOfLanguages;
        }


        public void Country_SelectionChanged()
        {
            if (tourRequestsWindow.CountryBox.SelectedIndex != -1)
            {
                List<string> listOfCities = LocationService.GetInstance().GetCitiesByCountry(tourRequestsWindow.CountryBox.SelectedItem.ToString());
                tourRequestsWindow.CityBox.ItemsSource = listOfCities;
                tourRequestsWindow.CityBox.Focus();
                tourRequestsWindow.CityBox.IsDropDownOpen = true;
                tourRequestsWindow.CityBox.IsEnabled = true;
            }
        }

        public  void City_SelectionChanged()
        {
            string city = null;
            if (tourRequestsWindow.CityBox.SelectedIndex != -1)
            {
                 city = tourRequestsWindow.CityBox.SelectedItem.ToString();
            }

            string language = null;
            if (tourRequestsWindow.LanguageBox.SelectedIndex != -1)
            {
                 language = tourRequestsWindow.LanguageBox.SelectedItem.ToString();
            }
            
            int capacity = 0;
            if(tourRequestsWindow.Capacity.Text != "")
            {
                capacity = Convert.ToInt32(tourRequestsWindow.Capacity.Text);
            }
           
            DateTime date1 = new DateTime();
            if (tourRequestsWindow.Picker1.SelectedDate != null)
            {
                 date1 = (DateTime)tourRequestsWindow.Picker1.SelectedDate;
            }

            DateTime date2 = new DateTime();
            if (tourRequestsWindow.Picker2.SelectedDate != null)
            {
                 date2 = (DateTime)tourRequestsWindow.Picker2.SelectedDate;
            }

            SearchByCriteria(city,language,capacity,date1,date2);
        }


        public void SearchByCriteria(string city, string language, int guests, DateTime start, DateTime end)
        {
            tourRequestControl.tourRequestControlViewModel.SearchByCriterias(city,language,guests,start,end);
        }

        public void CapacityUp_Click()
        {
            int number;
            if (tourRequestsWindow.Capacity.Text != "") number = Convert.ToInt32(tourRequestsWindow.Capacity.Text);
            else number = 0;

            tourRequestsWindow.Capacity.Text = Convert.ToString(number + 1);
        }

        public void CapacityDown_Click()
        {
            int number;
            if (tourRequestsWindow.Capacity.Text != "") number = Convert.ToInt32(tourRequestsWindow.Capacity.Text);
            else number = 0;

            if (tourRequestsWindow.Capacity.Text == "0" || tourRequestsWindow.Capacity.Text == "")
            {
                tourRequestsWindow.Capacity.Text = "0";
            }
            else
            {
                tourRequestsWindow.Capacity.Text = Convert.ToString(number - 1);
            }
        }

        internal void ResetFilters_Click()
        {
            tourRequestsWindow.CityBox.SelectedIndex = -1;
            tourRequestsWindow.CountryBox.SelectedIndex = -1;
            tourRequestsWindow.LanguageBox.SelectedIndex = -1;
            tourRequestsWindow.Capacity.Text = "0";
            tourRequestsWindow.Picker1.SelectedDate = null;
            tourRequestsWindow.Picker2.SelectedDate = null;
        }
    }
}
