using BookingApp.Model.MutualModels;
using BookingApp.Model.PathfinderModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using Microsoft.Win32;
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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for AddTourWindow.xaml
    /// </summary>
    public partial class AddTourWindow : Window
    {
        private User _user;
        private LocationRepository _locationRepository;
        private TourRepository _tourRepository;
        private TourImageRepository _imageRepository;
        private LanguageRepository _languageRepository;
        private CheckpointRepository _checkpointRepository;
        private TourStartTimeRepository _timeRepository;

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




        public ObservableCollection<TourImage> Images { get; set; }

        public ObservableCollection<TourStartTime> TourDates { get; set; }

        public ObservableCollection<Checkpoint> Checkpoints { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        // The method to invoke the property changed event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        public AddTourWindow(User user, TourRepository tourRepository, TourImageRepository tourImageRepository)
        {
            InitializeComponent();
            _user = user;
            _locationRepository = new LocationRepository();
            _tourRepository = tourRepository;
            _imageRepository = tourImageRepository;
            _languageRepository = new LanguageRepository();
            _checkpointRepository = new CheckpointRepository();
            _timeRepository = new TourStartTimeRepository();
            Images = new ObservableCollection<TourImage>();
            TourDates = new ObservableCollection<TourStartTime>();
            Checkpoints = new ObservableCollection<Checkpoint>();
            DataContext = this;
            datePicker.DisplayDateStart = DateTime.Now;
            LoadCountries();
            LoadLanguages();


        }

        

        private void CountryTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> listOfCities = _locationRepository.GetCitiesByCountry(CountryTextBox.SelectedItem.ToString());
            CityTextBox.ItemsSource = listOfCities;
            CityTextBox.Focus();
            CityTextBox.IsDropDownOpen = true;
            CityTextBox.IsEnabled = true;
        }

        private void LoadCountries()
        {
            List<string> listOfCountries = _locationRepository.GetCountries();
            CountryTextBox.ItemsSource = listOfCountries;
        }

        private void LoadLanguages()
        {
            List<string> listOfLanguages = _languageRepository.GetLanguages();
            LanguageTextBox.ItemsSource = listOfLanguages;
        }

        private void AddURLClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ImageURL))
            {
                return;
            }

            if (ImageAlreadyExists())
            {
                return;
            }

            TourImage image = new();
            image.Path = ImageURL;
            Images.Add(image);
            ImageURLTextBox.Clear();
        }

        private bool ImageAlreadyExists()
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

        private void ImageURLTextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                string destinationFolder = "../../../Resources/Images/TourImages/"; // Update this with your desired destination folder path

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


        private void AddCheckpointClick(object sender, RoutedEventArgs e)
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
            CheckpointTextBox.Clear();
        }

        private bool CheckpointAlreadyExists()
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


        private void AddDateClick(object sender, RoutedEventArgs e){
            DateTime time;
            
              DateTime.TryParse(datePicker.SelectedDate.Value.Date.ToShortDateString() + " " + Hours.ToString() + ":" + Minutes.ToString(), out time);

            if (DateAlreadyExists(time))
            {
                return;
            }

            TourStartTime startTime = new TourStartTime();
            startTime.Time = time;
            startTime.Guests = 0;
            TourDates.Add(startTime);
        }


        private bool DateAlreadyExists(DateTime date)
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

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
           if (!IsDataValid())
            {
                return;
            }

            Tour tour = new();
            tour.Id = _tourRepository.NextId();
            tour.Name = NameTextBox.Text;
            tour.LocationId = _locationRepository.GetLocationByCityAndCountry(City, Country).Id;
            tour.Description = Description;
            tour.Duration = Duration;
            tour.Capacity = Capacity;
            tour.Dates = TourDates.ToList();
            tour.Checkpoints = Checkpoints.ToList();
            tour.LanguageId = _languageRepository.GetLanguageByName(LanguageTextBox.SelectedItem.ToString()).Id;
            tour.Images = Images.ToList();
            _tourRepository.Add(tour);

            foreach (TourImage image in Images)
            {
                image.Id = _imageRepository.NextId();
                image.TourId = tour.Id;
                _imageRepository.Add(image);
            }

            foreach (TourStartTime time in TourDates)
            {
                time.Id = _timeRepository.NextId();
                time.TourId = tour.Id;
                _timeRepository.Add(time);
            }

            foreach (Checkpoint checkpoint in Checkpoints) {
                checkpoint.Id = _checkpointRepository.NextId();
                checkpoint.TourId = tour.Id;
                _checkpointRepository.Add(checkpoint);
            
            }

            Close();
        }

        private bool IsDataValid()
        {
            return !string.IsNullOrEmpty(NameTextBox.Text)
                && !string.IsNullOrEmpty(Country)
                && !string.IsNullOrEmpty(City)
                && !string.IsNullOrEmpty(LanguageTextBox.SelectedItem.ToString())
                && Checkpoints.Count() >= 2;
        }



    }
}
