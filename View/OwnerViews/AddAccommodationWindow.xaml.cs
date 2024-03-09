﻿using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for AddAccommodationWindow.xaml
    /// </summary>
    public partial class AddAccommodationWindow : Window
    {
        private User _user;

        private LocationRepository _locationRepository;
        private AccommodationRepository _accommodationRepository;
        private AccommodationImageRepository _imageRepository;

        private string name;
        public string AccommodationName
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

        private string type;
        public string Type
        {
            get => type;
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }

        private int maximumNumberOfGuests;
        public int MaximumNumberOfGuests
        {
            get => maximumNumberOfGuests;
            set
            {
                if (value != maximumNumberOfGuests)
                {
                    maximumNumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private int minimumNumberOfDaysForReservation;
        public int MinimumNumberOfDaysForReservation
        {
            get => minimumNumberOfDaysForReservation;
            set
            {
                if (value != minimumNumberOfDaysForReservation)
                {
                    minimumNumberOfDaysForReservation = value;
                    OnPropertyChanged();
                }
            }
        }

        private int daysBeforeReservationIsFinal;
        public int DaysBeforeReservationIsFinal
        {
            get => daysBeforeReservationIsFinal;
            set
            {
                if (value != daysBeforeReservationIsFinal)
                {
                    daysBeforeReservationIsFinal = value;
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

        public ObservableCollection<AccommodationImage> Images { get; set; }

        // The event handler for the property changed event
        public event PropertyChangedEventHandler PropertyChanged;

        // The method to invoke the property changed event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddAccommodationWindow(User user, AccommodationRepository accommodationRepository, AccommodationImageRepository accommodationImageRepository)
        {
            _user = user;
            _locationRepository = new LocationRepository();
            _accommodationRepository = accommodationRepository;
            _imageRepository = accommodationImageRepository;
            Images = new ObservableCollection<AccommodationImage>();
            DataContext = this;
            InitializeComponent();
            LoadCountries();
            LoadTypesOfAccommodations();
        }

        private void LoadTypesOfAccommodations()
        {
            List<string> listOfTypes = new List<string>(Enum.GetNames(typeof(AccommodationType)));
            AccommodationType.ItemsSource = listOfTypes;
        }

        private void LoadCountries()
        {
            List<string> listOfCountries = _locationRepository.GetCountries();
            CountryTextBox.ItemsSource = listOfCountries;
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if(!IsDataValid())
            {
                return;
            }

            Accommodation accommodation = new();
            accommodation.Id = _accommodationRepository.NextId();
            accommodation.Name = AccommodationName;
            accommodation.LocationId = _locationRepository.GetLocationByCityAndCountry(City, Country).Id;
            accommodation.Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), Type);
            accommodation.MaxGuestNumber = MaximumNumberOfGuests;
            accommodation.MinReservationDays = MinimumNumberOfDaysForReservation;
            accommodation.CancellationPeriodDays = DaysBeforeReservationIsFinal;
            accommodation.OwnerId = _user.Id;
            accommodation.Images = Images.ToList();
            _accommodationRepository.Add(accommodation);

            foreach (AccommodationImage image in Images)
            {
                image.Id = _imageRepository.NextId();
                image.AccommodationId = accommodation.Id;
                _imageRepository.Add(image);
            }
            
            Close();
        }

        private bool IsDataValid()
        {
            return !string.IsNullOrEmpty(AccommodationName)
                && !string.IsNullOrEmpty(Country)
                && !string.IsNullOrEmpty(City)
                && !string.IsNullOrEmpty(Type)
                && MaximumNumberOfGuests > 0
                && MinimumNumberOfDaysForReservation > 0
                && DaysBeforeReservationIsFinal > 0;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CountryTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> listOfCities = _locationRepository.GetCitiesByCountry(CountryTextBox.SelectedItem.ToString());
            CityTextBox.ItemsSource = listOfCities;
            CityTextBox.Focus();
            CityTextBox.IsDropDownOpen = true;
            CityTextBox.IsEnabled = true;
        }

        private void AddURLClick(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(ImageURL))
            {
                return;
            }

            if(ImageAlreadyExists())
            {
                return;
            }

            AccommodationImage image = new();
            image.Path = ImageURL;
            Images.Add(image);
            ImageURLTextBox.Clear();
        }

        private bool ImageAlreadyExists()
        {
            foreach(AccommodationImage image in Images)
            {
                if(image.Path.Equals(ImageURL))
                {
                    return true;
                }
            }

            return false;
        }

        private void ImageURLTextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                string destinationFolder = "../../../Resources/Images/AccommodationImages/"; // Update this with your desired destination folder path

                // Extracting filename from the full path
                string fileName = System.IO.Path.GetFileName(selectedImagePath);

                // Constructing destination path
                string destinationPath = System.IO.Path.Combine(destinationFolder, fileName);

                // Copy the selected image file to the destination folder
                System.IO.File.Copy(selectedImagePath, destinationPath, true);

                // Update the text box with the destination path
                ImageURLTextBox.Text = destinationPath;
            }
        }
    }
}