﻿using BookingApp.Domain.Models;
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


        public AddTourWindowViewModel addTourWindowViewModel { get; set; }

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

        

        private bool ImageAlreadyExists()
        {
          return addTourWindowViewModel.ImageAlreadyExists();
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
            addTourWindowViewModel.ConfirmButtonClick();
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

        private void RightArrow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            addTourWindowViewModel.RightArrow_Click();
        }

        private void LeftArrow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            addTourWindowViewModel.LeftArrow_Click();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveImage_Click(object sender, RoutedEventArgs e)
        {
            addTourWindowViewModel.DeleteImageClick();
        }

        private void LocationRecomendation_Click(object sender, MouseButtonEventArgs e)
        {
            addTourWindowViewModel.LocationRecomendation_Click();
        }

        private void LanguageRecomendation_Click(object sender, MouseButtonEventArgs e)
        {
            addTourWindowViewModel.LanguageRecomendation_Click();
        }

        private void DurationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            addTourWindowViewModel.DurationTextBox_PreviewTextInput(sender, e);
        }

        private void DurationTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            addTourWindowViewModel.DurationTextBox_PreviewKeyDown(sender, e);
        }

        private void CapacityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            addTourWindowViewModel.CapacityTextBox_PreviewTextInput(sender, e);
        }

        private void CapacityTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            addTourWindowViewModel.CapacityTextBox_PreviewKeyDown(sender,e);
        }

        private void RemoveCheckpoint_Click(object sender, RoutedEventArgs e)
        {
            addTourWindowViewModel.RemoveCheckpoint_Click();
        }

        private void RemoveDate_Click(object sender, RoutedEventArgs e)
        {
            addTourWindowViewModel.RemoveDate_Click();
        }
    }
}
