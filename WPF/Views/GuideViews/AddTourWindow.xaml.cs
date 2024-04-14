using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuideViewModels;
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
        
        public event PropertyChangedEventHandler PropertyChanged;

        // The method to invoke the property changed event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        AddTourWindowViewModel addTourWindowViewModel { get; set; }

        public AddTourWindow(User user)
        {
            InitializeComponent();
            addTourWindowViewModel = new AddTourWindowViewModel(this ,user);
            DataContext = addTourWindowViewModel;
           
        }

        

        private void CountryTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addTourWindowViewModel.CountryTextBox_SelectionChanged(sender, e);
        }

        private void LoadCountries()
        {
            addTourWindowViewModel.LoadCountries();
        }

        private void LoadLanguages()
        {
            addTourWindowViewModel.LoadLanguages();
        }

        private void AddURLClick(object sender, RoutedEventArgs e)
        {
           addTourWindowViewModel.AddURLClick(sender, e);   
        }

        private bool ImageAlreadyExists()
        {
          return addTourWindowViewModel.ImageAlreadyExists();
        }

        private void ImageURLTextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            addTourWindowViewModel.ImageURLTextBox_MouseDown(sender, e);
        }


        private void AddCheckpointClick(object sender, RoutedEventArgs e)
        {
            addTourWindowViewModel.AddCheckpointClick(sender, e);
        }

        private bool CheckpointAlreadyExists()
        {
           return  addTourWindowViewModel.CheckpointAlreadyExists();
        }


        private void AddDateClick(object sender, RoutedEventArgs e){
            addTourWindowViewModel.AddDateClick(sender, e);
        }


        private bool DateAlreadyExists(DateTime date)
        {
            return addTourWindowViewModel.DateAlreadyExists(date);
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            addTourWindowViewModel.ConfirmButtonClick(sender, e);
        }

        public void SaveImages(ObservableCollection<TourImage> images,Tour tour)
        {
            addTourWindowViewModel.SaveImages(images, tour);
        }

        public void SaveCheckpoints(ObservableCollection<Checkpoint> checkpoints, Tour tour) {

            addTourWindowViewModel.SaveCheckpoints(checkpoints, tour);
        }

        public void SaveDates(ObservableCollection<TourStartTime> dates,Tour tour)
        {
            addTourWindowViewModel.SaveDates(dates, tour);

        }

        private bool IsDataValid()
        {
            return addTourWindowViewModel.IsDataValid();
        }



    }
}
