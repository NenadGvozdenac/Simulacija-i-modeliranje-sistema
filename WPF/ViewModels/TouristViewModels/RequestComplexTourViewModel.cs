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
    public class RequestComplexTourViewModel
    {
        public ObservableCollection<RequestedTourist> _requestedTourists { get; set; }

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
        private int _numOfRequests;
        public int NumOfRequests
        {
            get { return _numOfRequests; }
            set
            {
                _numOfRequests = value;
                OnPropertyChanged();
            }
        }
        public List<Tourist> Tourists { get; set; }
        public string description { get; set; }
        public ObservableCollection<Tourist> _tourists { get; set; }
        public User user { get; set; }
        public RequestComplexTour requestTour { get; set; }

        public List<RequestedTourist> requestedTourists { get; set; }
        public List<TourRequest> tourRequests {  get; set; }
        public RequestComplexTourViewModel(User _user, RequestComplexTour _requestTour)
        {
            requestTour = _requestTour;
            _tourists = new ObservableCollection<Tourist>();
            _requestedTourists = new ObservableCollection<RequestedTourist>();
            Tourists = new List<Tourist>();
            user = _user;
            LoadCountries();
            LoadLanguages();
            BeginDate = DateTime.Now;
            EndDate = DateTime.Now;
            requestedTourists = new List<RequestedTourist>();
            tourRequests = new List<TourRequest>();
            NumOfRequests = 0;
        }

        public void LoadCountries()
        {
            List<string> listOfCountries = LocationService.GetInstance().GetCountries();
            requestTour.CountryComboBox.ItemsSource = listOfCountries;
        }
        public void LoadLanguages()
        {
            List<string> listOfLanguages = LanguageService.GetInstance().GetLanguages();
            requestTour.LanguageComboBox.ItemsSource = listOfLanguages;
        }

        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> listOfCities = LocationService.GetInstance().GetCitiesByCountry(requestTour.CountryComboBox.SelectedItem.ToString());
            requestTour.CityComboBox.ItemsSource = listOfCities;
            requestTour.CityComboBox.Focus();
            requestTour.CityComboBox.IsEnabled = true;
            SelectedCountry = requestTour.CountryComboBox.SelectedItem?.ToString();

        }
        public void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedCity = requestTour.CityComboBox.SelectedItem?.ToString();
        }
        public void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedLanguage = requestTour.LanguageComboBox.SelectedItem?.ToString();
        }

        public void GetTourInfo()
        {
            //selectedCountry = CountryComboBox.SelectedItem?.ToString();
            //selectedCity = CityComboBox.SelectedItem?.ToString();
            //selectedLanguage = LanguageComboBox.SelectedItem?.ToString();
            description = requestTour.DescriptionBox.Text;

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(requestTour.GuestAgeText.Text, out int result) && requestTour.GuestAgeText.Text != "")
            {
                MessageBox.Show("Enter valid age");
            }
            else
            {
                GuestAge = result;
            }
        }
        public void AddTourist_Click(object sender, RoutedEventArgs e)
        {
            string name = requestTour.GuestName.Text;
            string surname = requestTour.GuestSurname.Text;
            int age = (int)GuestAge;

            _tourists.Add(new Tourist { Name = name, Surname = surname, Age = age });
            RefreshTouristDataGrid();
            ClearInputFields();
            foreach (Tourist t in _tourists)
            {
                Tourists.Add(t);
            }
        }
        public void RefreshTouristDataGrid()
        {
            requestTour.TouristDataGrid.ItemsSource = null;
            requestTour.TouristDataGrid.ItemsSource = _tourists;
        }

        public void ClearInputFields()
        {
            requestTour.GuestName.Text = "";
            requestTour.GuestSurname.Text = "";
            requestTour.GuestAgeText.Text = "";
        }

        public void CheckAllFields()
        {
            if (SelectedCountry == null || SelectedCity == null || description == "" || SelectedLanguage == null || Tourists.Count == 0)
            {
                requestTour.enterAllFieldsMessage.Visibility = Visibility.Visible;
            }
        }

        public void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            GetTourInfo();
            CheckAllFields();
                        if (requestTour.enterAllFieldsMessage.Visibility != Visibility.Visible)
                        {
                            TourRequest tourRequest = new TourRequest();
                            tourRequest.Id = TourRequestService.GetInstance().NextId();
                            foreach (Tourist t in Tourists)
                            {
                                RequestedTourist rt = new RequestedTourist();
                                rt.Id = RequestedTouristService.GetInstance().NextId();
                                rt.Name = t.Name;
                                rt.Age = t.Age;
                                rt.Surname = t.Surname;
                                rt.RequestId = tourRequest.Id;
                                requestedTourists.Add(rt);  
                            }
                            tourRequest.UserId = user.Id;
                            tourRequest.LocationId = LocationService.GetInstance().GetLocationByCityAndCountry(SelectedCity, SelectedCountry).Id;
                            tourRequest.Description = description;
                            tourRequest.LanguageId = LanguageService.GetInstance().GetLanguageByName(SelectedLanguage).Id;
                            tourRequest.BeginDate = BeginDate;
                            tourRequest.EndDate = EndDate;
                            tourRequest.RequestDate = DateTime.Now;
                            tourRequest.Status = "Pending";
                tourRequest.ComplexRequestId = -1;
                            tourRequests.Add(tourRequest);
                MessageBox.Show("Success!");
                NumOfRequests++;
            }
            else
                        {
                            return;
                        }
        }
        public void Finish_Click(object sender, RoutedEventArgs e)
        {
            if(tourRequests.Count > 1)
            {
                ComplexTourRequest complexTourRequest = new ComplexTourRequest();
                int id = ComplexTourRequestService.GetInstance().NextId();
                complexTourRequest.Id = id;
                complexTourRequest.UserId = user.Id;
                complexTourRequest.Status = "Pending";
                ComplexTourRequestService.GetInstance().Add(complexTourRequest);
                foreach (TourRequest tr in tourRequests)
                {
                    foreach(RequestedTourist rt in requestedTourists){
                        RequestedTouristService.GetInstance().Add(rt);
                    }
                    TourRequestService.GetInstance().Add(tr);

                    tr.ComplexRequestId = id;
                    TourRequestService.GetInstance().Update(tr);
                }
                MessageBox.Show("RequestCreated!");
                
            }else
            {
                MessageBox.Show("Add requests!");
            }
        }
    }
}
