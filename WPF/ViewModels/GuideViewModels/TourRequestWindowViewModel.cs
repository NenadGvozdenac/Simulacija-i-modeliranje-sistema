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

        public TourRequestWindowViewModel(TourRequestsWindow _tourRequestsWindow, User _user)
        {
            tourRequestsWindow = _tourRequestsWindow;
            user = _user;
            var tourRequestControl = new TourRequestControl(user);
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
            List<string> listOfCities = LocationService.GetInstance().GetCitiesByCountry(tourRequestsWindow.CountryBox.SelectedItem.ToString());
            tourRequestsWindow.CityBox.ItemsSource = listOfCities;
            tourRequestsWindow.CityBox.Focus();
            tourRequestsWindow.CityBox.IsDropDownOpen = true;
            tourRequestsWindow.CityBox.IsEnabled = true;
        }
    }
}
