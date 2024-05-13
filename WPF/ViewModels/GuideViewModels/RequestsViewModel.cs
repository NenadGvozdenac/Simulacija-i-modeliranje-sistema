using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.View.PathfinderViews;
using BookingApp.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class RequestsViewModel : INotifyPropertyChanged
    {
        private int location;

        public int Location
        {
            get => location;
            set
            {
                if (value != location)
                {
                    location = value;
                    OnPropertyChanged();
                }
            }

        }

        private int languageRec;
        public int LanguageRec
        {
            get => languageRec;
            set
            {
                if (value != languageRec)
                {
                    languageRec = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // The method to invoke the property changed event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        public RequestStatistics requestStatisticsWindow { get; set; }

        public RequestsViewModel(RequestStatistics requestStatistics) 
        {
            requestStatisticsWindow = requestStatistics;    
            requestStatisticsWindow.DataContext = this;
            Location = 0;
            LanguageRec = 0;

            LoadCountries();
            LoadLanguages();
            LoadYears();
            LoadMonths();
        
        }

        public void LoadCountries()
        {
            List<string> listOfCountries = LocationService.GetInstance().GetCountries();
            requestStatisticsWindow.CountryCombobox.ItemsSource = listOfCountries;
        }

        public void LoadLanguages()
        {
            List<string> listOfLanguages = LanguageService.GetInstance().GetLanguages();
            requestStatisticsWindow.LanguageCombobox.ItemsSource = listOfLanguages;
        }

        public void LoadYears()
        {
            List<int> years = new List<int>();
            List<TourRequest> requests = new List<TourRequest>();
            requests = TourRequestService.GetInstance().GetAll();

            foreach(TourRequest request in requests)
            {
                if (!years.Contains(request.RequestDate.Year))
                    years.Add(request.RequestDate.Year);
            }

            requestStatisticsWindow.YearCombobox.ItemsSource = years;
        }

        public void LoadMonths()
        {
            List<int> months = new List<int>();
            for (int i = 1; i <=12; i++)
            {
                months.Add(i);
            }
            requestStatisticsWindow.MonthCombobox.ItemsSource = months;
        }


        public void Country_SelectionChanged()
        {
            
                List<string> listOfCities = LocationService.GetInstance().GetCitiesByCountry(requestStatisticsWindow.CountryCombobox.SelectedItem.ToString());
                requestStatisticsWindow.CityCombobox.ItemsSource = listOfCities;
                requestStatisticsWindow.CityCombobox.Focus();
                requestStatisticsWindow.CityCombobox.IsDropDownOpen = true;
                requestStatisticsWindow.CityCombobox.IsEnabled = true;
            
        }

        public void City_SelectionChanged()
        {
            int number = 0;
            
            if (requestStatisticsWindow.CityCombobox.SelectedItem != null)
            {
                string city = requestStatisticsWindow.CityCombobox.SelectedItem.ToString();
                List<TourRequest> requests = new List<TourRequest>();
                requests = TourRequestService.GetInstance().GetAll();
                foreach (TourRequest request in requests)
                {
                    if (city == LocationService.GetInstance().GetById(request.LocationId).City)
                        number++;
                }
            }

            Location = number;

            requestStatisticsWindow.YearCombobox.IsEnabled = true;
            requestStatisticsWindow.MonthCombobox.IsEnabled = true;
            
        }

        internal void Language_SelectionChanged()
        {
            int number = 0;

            if (requestStatisticsWindow.LanguageCombobox.SelectedItem != null)
            {
                string language = requestStatisticsWindow.LanguageCombobox.SelectedItem.ToString();
                List<TourRequest> requests = new List<TourRequest>();
                requests = TourRequestService.GetInstance().GetAll();
                foreach (TourRequest request in requests)
                {
                    if (language == LanguageService.GetInstance().GetById(request.LanguageId).Name)
                        number++;
                }
            }

            LanguageRec = number;
            requestStatisticsWindow.YearCombobox.IsEnabled = true;
            requestStatisticsWindow.MonthCombobox.IsEnabled = true;
            
        }

        public void Year_SelectionChanged()
        {
            int number_1 = 0;
            int number_2 = 0;
            City_SelectionChanged();
            Language_SelectionChanged();
            if (requestStatisticsWindow.YearCombobox.SelectedIndex != -1)
            {
                List<TourRequest> requests = new List<TourRequest>();
                requests = TourRequestService.GetInstance().GetAll();

                if (requestStatisticsWindow.CityCombobox.SelectedIndex != -1)
                {
                    foreach(TourRequest request in requests)
                    {
                        Location location = LocationService.GetInstance().GetById(request.LocationId);
                        if(location.City == requestStatisticsWindow.CityCombobox.Text && request.RequestDate.Year != Convert.ToInt32(requestStatisticsWindow.YearCombobox.SelectedItem))
                            number_1++;
                    }
                    Location -= number_1;
                }
                
                if (requestStatisticsWindow.LanguageCombobox.SelectedIndex != -1)
                {
                    foreach (TourRequest request in requests)
                    {
                        Language language = LanguageService.GetInstance().GetById(request.LanguageId);
                        if (language.Name == requestStatisticsWindow.LanguageCombobox.Text && request.RequestDate.Year != Convert.ToInt32(requestStatisticsWindow.YearCombobox.SelectedItem))
                            number_2++;
                    }
                    LanguageRec -= number_2;
                }
                number_1 = 0;
                number_2 = 0;
            }
        }

        public void Month_SelectionChanged()
        {
            int number_1 = 0;
            int number_2 = 0;
            City_SelectionChanged();
            Language_SelectionChanged();
            Year_SelectionChanged();
            if (requestStatisticsWindow.MonthCombobox.SelectedIndex != -1)
            {
                List<TourRequest> requests = new List<TourRequest>();
                requests = TourRequestService.GetInstance().GetAll();

                if (requestStatisticsWindow.CityCombobox.SelectedIndex != -1)
                {
                    foreach (TourRequest request in requests)
                    {
                        Location location = LocationService.GetInstance().GetById(request.LocationId);
                        if (location.City == requestStatisticsWindow.CityCombobox.Text && request.RequestDate.Month != Convert.ToInt32(requestStatisticsWindow.MonthCombobox.SelectedItem))
                            number_1++;
                    }
                    Location -= number_1;
                }

                if (requestStatisticsWindow.LanguageCombobox.SelectedIndex != -1)
                {
                    foreach (TourRequest request in requests)
                    {
                        Language language = LanguageService.GetInstance().GetById(request.LanguageId);
                        if (language.Name == requestStatisticsWindow.LanguageCombobox.Text && request.RequestDate.Month != Convert.ToInt32(requestStatisticsWindow.MonthCombobox.SelectedItem))
                            number_2++;
                    }
                    LanguageRec -= number_2;
                }
                number_1 = 0;
                number_2 = 0;
            }

        }

        public void Reset_click()
        {
            requestStatisticsWindow.YearCombobox.SelectedIndex = -1;
            requestStatisticsWindow.MonthCombobox.SelectedIndex = -1;
        }

        
    }
}
