using BookingApp.Domain.Models;
using BookingApp.Repositories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BookingApp.View.PathfinderViews;
using BookingApp.Application.UseCases;
using BookingApp.Application.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using BookingApp.WPF.Views.TouristViews;
using System.Text.RegularExpressions;
using Notifications.Wpf;


namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class AddTourWindowViewModel : INotifyPropertyChanged
    {
        private User _user;
        

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string country;
        public string Country
        {
            get => country;
            set
            {
                if (value != country)
                {
                    country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string city;
        public string City
        {
            get => city;
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }

        private int capacity;
        public int Capacity
        {
            get => capacity;
            set
            {
                if (value != capacity)
                {
                    capacity = value;
                    OnPropertyChanged();
                }
            }
        }

        private int duration;
        public int Duration
        {
            get => duration;
            set
            {
                if (value != duration)
                {
                    duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string language;
        public string Language
        {
            get => language;
            set
            {
                if (value != language)
                {
                    language = value;
                    OnPropertyChanged();
                }
            }
        }

        private string imageURL;
        public string ImageURL
        {
            get => imageURL;
            set
            {
                if (value != imageURL)
                {
                    imageURL = value;
                    OnPropertyChanged();
                }
            }
        }

        private string checkpoint;
        public string Checkpoint
        {
            get => checkpoint;
            set
            {
                if (value != checkpoint)
                {
                    checkpoint = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime date;
        public string Date
        {
            get => Convert.ToString(date);
            set
            {
                if (value != checkpoint)
                {
                    checkpoint = value;
                    OnPropertyChanged();
                }
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string hours;
        public string Hours
        {
            get => hours;
            set
            {
                if (value != hours)
                {
                    hours = value;
                    OnPropertyChanged();
                }
            }
        }

        private string minutes;
        public string Minutes
        {
            get => minutes;
            set
            {
                if (value != minutes)
                {
                    minutes = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location location;

        public Location Location
        {
            get => location;
            set
            {
                if(value != location)
                {
                    location = value;
                    OnPropertyChanged();
                }
            }

        }

        private Language languageRec;
        public Language LanguageRec
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

        public ObservableCollection<TourImage> Images { get; set; }

        public ObservableCollection<TourStartTime> TourDates { get; set; }

        public ObservableCollection<Checkpoint> Checkpoints { get; set; }

        public ICommand AddImageCommand => new AddTourImageCommand(this);
        public ICommand RemoveImageCommand => new RemoveTourImageCommand(this);


        public event PropertyChangedEventHandler PropertyChanged;

        // The method to invoke the property changed event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddTourWindow addTourWindow { get; set; }

        public int RequestId { get; set; }

        public int TouristId { get; set; }

       

        public AddTourWindowViewModel(AddTourWindow _addTourWindow, User user)
        {
            addTourWindow = _addTourWindow;
            _user = user;
            RequestId = -1;
            Images = new ObservableCollection<TourImage>();
            TourDates = new ObservableCollection<TourStartTime>();
            Checkpoints = new ObservableCollection<Checkpoint>();
            addTourWindow.DataContext = this;
            addTourWindow.datePicker.DisplayDateStart = DateTime.Now.AddDays(1);
            Location = FindMostRequestedLocation();
            LanguageRec = FindMostRequestedLanguage();
            LoadCountries();
            LoadLanguages();
            

        }

        public Location FindMostRequestedLocation()
        {
            List<TourRequest> requests = new List<TourRequest>();
            requests = TourRequestService.GetInstance().GetAll();
            List<Location> locations = new List<Location>();
            locations = LocationService.GetInstance().GetAll();
            int numberOfRequests = 0;
            int numberOfRequests_temp = 0;
            Location location = locations[0];

            foreach(Location l in locations)
            {
                foreach (TourRequest request in requests)
                {
                    if(l.Id == request.LocationId)
                    {
                        numberOfRequests_temp++;
                    }
                }
                if(numberOfRequests_temp > numberOfRequests)
                {
                    numberOfRequests = numberOfRequests_temp;
                    location = l;
                }
                numberOfRequests_temp = 0;
            }

            return location;
        }

        public Language FindMostRequestedLanguage()
        {
            List<TourRequest> requests = new List<TourRequest>();
            requests = TourRequestService.GetInstance().GetAll();
            List<Language> languages = new List<Language>();
            languages = LanguageService.GetInstance().GetAll();
            int numberOfRequests = 0;
            int numberOfRequests_temp = 0;
            Language language = languages[0];

            foreach (Language l in languages)
            {
                foreach (TourRequest request in requests)
                {
                    if (l.Id == request.LanguageId)
                    {
                        numberOfRequests_temp++;
                    }
                }
                if (numberOfRequests_temp > numberOfRequests)
                {
                    numberOfRequests = numberOfRequests_temp;
                    language = l;
                }
                numberOfRequests_temp = 0;
            }

            return language;
        }




        public void CountryTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> listOfCities = LocationService.GetInstance().GetCitiesByCountry(addTourWindow.CountryTextBox.SelectedItem.ToString());
            addTourWindow.CityTextBox.ItemsSource = listOfCities;
            addTourWindow.CityTextBox.Focus();
            addTourWindow.CityTextBox.IsDropDownOpen = true;
            addTourWindow.CityTextBox.IsEnabled = true;
        }

        public void LoadCountries()
        {
            List<string> listOfCountries = LocationService.GetInstance().GetCountries();
            addTourWindow.CountryTextBox.ItemsSource = listOfCountries;
        }

        public void LoadLanguages()
        {
            List<string> listOfLanguages = LanguageService.GetInstance().GetLanguages();
            addTourWindow.LanguageTextBox.ItemsSource = listOfLanguages;
        }

        

        public bool ImageAlreadyExists()
        {
            foreach (TourImage image in Images)
            {
                if (image.Path.Equals(ImageURL))
                {
                    return true;
                }
            }

            return false;
        }

        

        public void DeleteImageClick()
        {
            var currentImage = Images.FirstOrDefault(image => image.Path == ImageURL);
            Images.Remove(currentImage);
            ImageURL = Images.FirstOrDefault()?.Path;
        }

        public void AddCheckpointClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Checkpoint))
            {
                return;
            }

            if (CheckpointAlreadyExists())
            {
                return;
            }

            Checkpoint checkpoint = new();
            checkpoint.Name = Checkpoint;
            Checkpoints.Add(checkpoint);
            addTourWindow.CheckpointTextBox.Clear();
        }

        public bool CheckpointAlreadyExists()
        {
            foreach (Checkpoint checkpoint in Checkpoints)
            {
                if (checkpoint.Name.Equals(Checkpoint))
                {
                    return true;
                }
            }
            return false;
        }
        public void AddDateClick(object sender, RoutedEventArgs e)
        {
            DateTime time;

            if (string.IsNullOrEmpty(Hours) || string.IsNullOrEmpty(Minutes))
                return;

            DateTime.TryParse(addTourWindow.datePicker.SelectedDate.Value.Date.ToShortDateString() + " " + Hours.ToString() + ":" + Minutes.ToString(), out time);

            if (DateAlreadyExists(time))
            {
                return;
            }

            TourStartTime startTime = new TourStartTime();
            startTime.Time = time;
            startTime.Guests = 0;
            TourDates.Add(startTime);
        }


        public bool DateAlreadyExists(DateTime date)
        {
            foreach (TourStartTime startTime in TourDates)
            {
                if (startTime.Time.Equals(date))
                {
                    return true;
                }
            }

            return false;
        }

        public void ConfirmButtonClick()
        {
            if (!IsDataValid())
            {
                return;
            }

            Tour tour = new();
            tour.Id = TourService.GetInstance().NextId();
            tour.OwnerId = _user.Id;
            tour.Name = addTourWindow.NameTextBox.Text;
            tour.LocationId = LocationService.GetInstance().GetLocationByCityAndCountry(City, Country).Id;
            tour.Description = Description;
            tour.Duration = Duration;
            tour.Capacity = Capacity;
            tour.Dates = TourDates.ToList();
            tour.Checkpoints = Checkpoints.ToList();
            tour.LanguageId = LanguageService.GetInstance().GetLanguageByName(addTourWindow.LanguageTextBox.SelectedItem.ToString()).Id;
            tour.Images = new(Images.ToList());
            TourService.GetInstance().Add(tour);


            SaveImages(Images, tour);

            SaveDates(TourDates, tour);

            SaveCheckpoints(Checkpoints, tour);

            if(RequestId != -1)
            {
                List<RequestedTourist> tourists = new List<RequestedTourist>();
                tourists = RequestedTouristService.GetInstance().GetAll().Where(t => t.RequestId ==  RequestId).ToList();
                foreach(RequestedTourist tourist in tourists)
                {
                    Tourist t = new Tourist();
                    t.Name = tourist.Name;
                    t.Surname = tourist.Surname;
                    t.Age = tourist.Age;
                    t.Id = TouristService.GetInstance().NextId();
                    TouristService.GetInstance().Add(t);

                    Tourist t_temp = new Tourist();
                    TourStartTime time_temp = new TourStartTime();
                    
                    var tourist_list = TouristService.GetInstance().GetAll();
                    var time_list = TourStartTimeService.GetInstance().GetAll();
                    if(tourist_list.Count > 0)
                    {
                        t_temp = tourist_list[tourist_list.Count - 1];
                    }
                    if(time_list.Count > 0)
                    {
                        time_temp = time_list[time_list.Count - TourDates.Count];
                    }
                    MakeReservation(time_temp.Id, t_temp.Id);
                }

            }


            addTourWindow.Close();

            var notificationManager = new NotificationManager();



            notificationManager.Show(new NotificationContent
            {
                Title = "Success",
                Message = "Tour created successfully.",
                Type = NotificationType.Information
            });

        }

        public void MakeReservation(int tourTime, int touristId)
        {
            TouristReservation reservation = new TouristReservation();
            reservation.Id_Tourist = touristId;
            reservation.Id_TourTime = tourTime;
            reservation.Id = TourReservationService.GetInstance().NextId();
            reservation.CheckpointId = -1;
            
            TourReservationService.GetInstance().Add(reservation);
        }

        public void SaveImages(ObservableCollection<TourImage> images, Tour tour)
        {
            foreach (TourImage image in Images)
            {
                image.Id = TourImageService.GetInstance().NextId();
                image.TourId = tour.Id;
                TourImageService.GetInstance().Add(image);
            }
        }

        public void SaveCheckpoints(ObservableCollection<Checkpoint> checkpoints, Tour tour)
        {

            foreach (Checkpoint checkpoint in Checkpoints)
            {
                checkpoint.Id = CheckpointService.GetInstance().NextId();
                checkpoint.TourId = tour.Id;
                CheckpointService.GetInstance().Add(checkpoint);

            }

        }

        public void SaveDates(ObservableCollection<TourStartTime> dates, Tour tour)
        {
            foreach (TourStartTime time in TourDates)
            {
                time.Id = TourStartTimeService.GetInstance().NextId();
                time.TourId = tour.Id;
                time.Status = "scheduled";
                time.Guests = 0;
                time.CurrentCheckpoint = -1;
                TourStartTimeService.GetInstance().Add(time);
            }

        }

        public bool IsDataValid()
        {

            validationMessages();

            return !string.IsNullOrEmpty(addTourWindow.NameTextBox.Text)
                && !string.IsNullOrEmpty(Country)
                && !string.IsNullOrEmpty(City)
                && !string.IsNullOrEmpty(addTourWindow.LanguageTextBox.SelectedItem.ToString())
                && Checkpoints.Count() >= 2;
        }

   

        public void RightArrow_Click()
        {
            var currentImage = Images.FirstOrDefault(image => image.Path == ImageURL);
            var currentIndex = Images.IndexOf(currentImage);

            if (currentIndex == -1) { return; }

            if (currentIndex == Images.Count - 1)
            {
                ImageURL = Images.First().Path;
            }
            else
            {
                ImageURL = Images[currentIndex + 1].Path;
            }
        }

        public void LeftArrow_Click()
        {
            var currentImage = Images.FirstOrDefault(image => image.Path == ImageURL);
            var currentIndex = Images.IndexOf(currentImage);

            if (currentIndex == -1) { return; }

            if (currentIndex == 0)
            {
                ImageURL = Images.Last().Path;
            }
            else
            {
                ImageURL = Images[currentIndex - 1].Path;
            }
        }

        public  void LocationRecomendation_Click()
        {
            Country = Location.Country;
            City = Location.City;

        }

        internal void LanguageRecomendation_Click()
        {
            Language = LanguageRec.Name;
        }

        public void validationMessages()
        {
            if (string.IsNullOrEmpty(addTourWindow.NameTextBox.Text))
            {
                addTourWindow.nameErrorBox.Visibility = Visibility.Visible;
            }
            else
            {
                addTourWindow.nameErrorBox.Visibility = Visibility.Collapsed;
            }

            if (addTourWindow.CountryTextBox.SelectedIndex == -1)
            {
                addTourWindow.locationErrorBox.Visibility = Visibility.Visible;
            }
            else
            {
                addTourWindow.locationErrorBox.Visibility = Visibility.Collapsed;
            }

            if (addTourWindow.CityTextBox.SelectedIndex == -1)
            {
                addTourWindow.locationErrorBox.Visibility = Visibility.Visible;
            }
            else
            {
                addTourWindow.locationErrorBox.Visibility = Visibility.Collapsed;
            }

            if (addTourWindow.LanguageTextBox.SelectedIndex == -1)
            {
                addTourWindow.languageErrorBox.Visibility = Visibility.Visible;
            }
            else
            {
                addTourWindow.languageErrorBox.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(addTourWindow.CapacityTextBox.Text) || Convert.ToInt32(addTourWindow.CapacityTextBox.Text) == 0)
            {
                addTourWindow.capacityErrorBox.Visibility = Visibility.Visible;
            }
            else
            {
                addTourWindow.capacityErrorBox.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(addTourWindow.DurationTextBox.Text) || Convert.ToInt32(addTourWindow.DurationTextBox.Text) == 0)
            {
                addTourWindow.durationErrorBox.Visibility = Visibility.Visible;
            }
            else
            {
                addTourWindow.durationErrorBox.Visibility = Visibility.Collapsed;
            }

            if (addTourWindow.datesGrid.Items.Count == 0)
            {
                addTourWindow.dateErrorBox.Visibility = Visibility.Visible;
            }
            else
            {
                addTourWindow.dateErrorBox.Visibility = Visibility.Collapsed;
            }

            if (addTourWindow.checkpointGrid.Items.Count < 2)
            {
                addTourWindow.checkpointErrorBox.Visibility = Visibility.Visible;
            }
            else
            {
                addTourWindow.checkpointErrorBox.Visibility = Visibility.Collapsed;
            }

        }

        private bool IsTextAllowed(string text)
        {
            // Only allow numeric input
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }

        internal void DurationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        internal void DurationTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        internal void CapacityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        internal void CapacityTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
