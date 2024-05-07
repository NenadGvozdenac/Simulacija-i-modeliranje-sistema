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


        public AddTourWindowViewModel(AddTourWindow _addTourWindow, User user)
        {
            addTourWindow = _addTourWindow;
            _user = user;
            Images = new ObservableCollection<TourImage>();
            TourDates = new ObservableCollection<TourStartTime>();
            Checkpoints = new ObservableCollection<Checkpoint>();
            addTourWindow.DataContext = this;
            addTourWindow.datePicker.DisplayDateStart = DateTime.Now.AddDays(1);
            LoadCountries();
            LoadLanguages();
            

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

        /*  public void ImageURLTextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                  addTourWindow.ImageURLTextBox.Text = destinationPath;
              }
          }*/

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

        public void ConfirmButtonClick(object sender, RoutedEventArgs e)
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

            addTourWindow.Close();
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



    }
}
