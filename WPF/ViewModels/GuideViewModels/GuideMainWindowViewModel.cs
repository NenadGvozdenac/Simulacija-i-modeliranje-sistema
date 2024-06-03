using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.View.PathfinderViews;
using BookingApp.View.PathfinderViews.Componentss;
using BookingApp.WPF.Views.GuestViews;
using BookingApp.WPF.Views.GuideViews;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class GuideMainWindowViewModel
    {
        public String username { get; set; }
        public  User _user {  get; set; }
        
        private DailyTourCard dailyTourCard;

        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedMain { get; set; }

        

        public GuideMainWindow mainWindow { get; set; }

        public DailyToursControl tours {  get; set; }

        public ICommand ScheduleTourCommand { get; }

        public ICommand DailyToursCommand { get; }

        public ICommand ReviewsCommand { get; }

        public ICommand DemographicsCommand { get; }
        public GuideMainWindowViewModel(GuideMainWindow _mainWindow,User user)
        {
            mainWindow = _mainWindow;
            _user = user;
            username = user.Username;
            tours = new DailyToursControl(_user);
            mainWindow.TourContainer.Children.Add(tours);
            ScheduleTourCommand = new RelayCommand(ScheduleTour_Accelerator);
            DailyToursCommand = new RelayCommand(DailyTours_Accelerator);
            ReviewsCommand = new RelayCommand(Reviews_Accelerator);
            DemographicsCommand = new RelayCommand(Demographics_Accelerator);
            LoadCountries();
            LoadLanguages();
            Update();
        }

        
        public void LoadCountries()
        {
            List<string> listOfCountries = LocationService.GetInstance().GetCountries();
            mainWindow.CountryTextBox.ItemsSource = listOfCountries;
        }
        public void LoadLanguages()
        {
            List<string> listOfLanguages = LanguageService.GetInstance().GetLanguages();
            mainWindow.LanguageTextBox.ItemsSource = listOfLanguages;
        }

        public void CityTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterContent();
        }

        public void LanguageTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterContent();
        }

        public void CountryTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainWindow.CountryTextBox.SelectedIndex != -1)
            {
                List<string> listOfCities = LocationService.GetInstance().GetCitiesByCountry(mainWindow.CountryTextBox.SelectedItem.ToString());
                mainWindow.CityTextBox.ItemsSource = listOfCities;
                mainWindow.CityTextBox.Focus();
                mainWindow.CityTextBox.IsDropDownOpen = true;
                mainWindow.CityTextBox.IsEnabled = true;
            }

            filterContent();
        }

        public void Capacity_TextChanged(object sender, TextChangedEventArgs e) 
        {
            filterContent();
        }

        public void CapacityUp_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (mainWindow.Capacity.Text != "") number = Convert.ToInt32(mainWindow.Capacity.Text);
            else number = 0;

            mainWindow.Capacity.Text = Convert.ToString(number + 1);
        }

        public void CapacityDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (mainWindow.Capacity.Text != "") number = Convert.ToInt32(mainWindow.Capacity.Text);
            else number = 0;

            if (number != 0)
            {
                mainWindow.Capacity.Text = Convert.ToString(number - 1);
            }
            else number = 0;

        }


        public void Duration_TextChanged(object sender, TextChangedEventArgs e)
        {
            filterContent();
        }

        public void DurationUp_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (mainWindow.Duration.Text != "") number = Convert.ToInt32(mainWindow.Duration.Text);
            else number = 0;

            mainWindow.Duration.Text = Convert.ToString(number + 1);
        }

        public void DurationDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (mainWindow.Duration.Text != "") number = Convert.ToInt32(mainWindow.Duration.Text);
            else number = 0;

            if (number != 0)
            {
                mainWindow.Duration.Text = Convert.ToString(number - 1);
            }
            else number = 0;
        }


        public void ScheduleTourClick(object sender, RoutedEventArgs e)
        {
            AddTourWindow tourWindow = new AddTourWindow(_user);
            tourWindow.ShowDialog();

        }

        public void ScheduleTour_Accelerator()
        {
            AddTourWindow tourWindow = new AddTourWindow(_user);
            tourWindow.ShowDialog();
        }

        public void DailyToursClick(object sender, RoutedEventArgs e)
        {


            DailyToursWindow dailyWindow = new DailyToursWindow(_user);

            if (ongoingTourCheck() == 0)
            {

                dailyWindow.dailyToursWindowViewModel.BeginButtonClickedWindow += (s, e) => DailyToursWindow_SomeEventHandler(s, e);
                dailyWindow.dailyToursWindowViewModel.EndButtonClickedWindow += (s, e) => DailyToursWindow_EndEventHandlerDaily(s, e);
                dailyWindow.ShowDialog();
            }
        }

        public void DailyTours_Accelerator()
        {
            DailyToursWindow dailyWindow = new DailyToursWindow(_user);

            if (ongoingTourCheck() == 0)
            {

                dailyWindow.dailyToursWindowViewModel.BeginButtonClickedWindow += (s, e) => DailyToursWindow_SomeEventHandler(s, e);
                dailyWindow.dailyToursWindowViewModel.EndButtonClickedWindow += (s, e) => DailyToursWindow_EndEventHandlerDaily(s, e);
                dailyWindow.ShowDialog();
            }
        }

        public int ongoingTourCheck()
        {
            foreach (TourStartTime time in TourStartTimeService.GetInstance().GetAll())
            {
                if (time.Status == "ongoing")
                {
                    CheckpointsView checkpointsWindow = new CheckpointsView(time.TourId, time.Time);
                    checkpointsWindow.checkpointsViewModel.EndButtonClickedMain += (s, e) => DailyToursWindow_EndEventHandlerMain(s, e);

                    checkpointsWindow.ShowDialog();
                    return 1;
                }
            }
            return 0;
        }

        public void OngoingTour_Click(object sender, RoutedEventArgs e)
        {
            if(ongoingTourCheck() == 0)
            {
                MessageBox.Show("There are no ongoing tours currently");
            }
        }

        public void Update()
        {
            GuideService.GetInstance().FindDominantLanguage(_user.Id);
        }



        /* private void DailyTourCard_BeginButtonClicked(object sender, BeginButtonClickedEventArgs e)
         {
             ChangeTourStatus(e.TourId, e.StartTime);
         }*/

        public void ChangeTourStatusOngoing(int tourId, DateTime dateTime)
        {
            TourStartTime startTime = new TourStartTime();
            startTime = TourStartTimeService.GetInstance().GetByTourStartTimeAndId(dateTime, tourId);
            startTime.Status = "ongoing";
            TourStartTimeService.GetInstance().Update(startTime);
        }

        public void ChangeTourStatusPassed(int tourId, DateTime dateTime)
        {
            TourStartTime startTime = new TourStartTime();
            startTime = TourStartTimeService.GetInstance().GetByTourStartTimeAndId(dateTime, tourId);
            startTime.Status = "passed";
            TourStartTimeService.GetInstance().Update(startTime);
        }

        public void Demographics_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TourDemographics tourDemographics = new TourDemographics();
            tourDemographics.Show();
        }

        internal void RequestsStatistics_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RequestStatistics requestStatistics = new RequestStatistics();
            requestStatistics.Show();
        }


        public void Demographics_Accelerator()
        {
            TourDemographics tourDemographics = new TourDemographics();
            tourDemographics.Show();
        }
        public void Reviews_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReviewsWindow reviewsWindow = new ReviewsWindow(_user);
            reviewsWindow.Show();
        }

        public void Reviews_Accelerator()
        {
            ReviewsWindow reviewsWindow = new ReviewsWindow(_user);
            reviewsWindow.Show();
        }

        // HANDLERS
        public  void DailyToursWindow_SomeEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            OnBeginButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            ChangeTourStatusOngoing(e.TourId, e.StartTime);
        }



        public void DailyToursWindow_EndEventHandlerDaily(object sender, BeginButtonClickedEventArgs e)
        {
            OnEndButtonClickedDaily(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnEndButtonClickedDaily(BeginButtonClickedEventArgs e)
        {
            ChangeTourStatusPassed(e.TourId, e.StartTime);
        }

        public void DailyToursWindow_EndEventHandlerMain(object sender, BeginButtonClickedEventArgs e)
        {
            ChangeTourStatusPassed(e.TourId, e.StartTime);
            tours.dailyToursControlViewModel.DailyTourCard_EndButtonClicked(sender, e);
        }

        public void OnEndButtonClickedMain(BeginButtonClickedEventArgs e)
        {
           
        }

        public void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            filterContent();
        }

       public void ClearFilters_Click()
        {

            mainWindow.CityTextBox.SelectedIndex = -1;
            mainWindow.CountryTextBox.SelectedIndex = -1;
            mainWindow.LanguageTextBox.Text = "";
            mainWindow.Capacity.Text = "0";
            mainWindow.Duration.Text = "0";

        }

        internal void TourRequest_Click()
        {
            TourRequestsWindow tourRequestsWindow = new TourRequestsWindow(_user);
            tourRequestsWindow.Show();

   
        }

        internal void infoClick()
        {
           InfoWindow infoWindow = new InfoWindow(_user);
           infoWindow.Show();
           infoWindow.infoWindowViewModel.quitEvent += (s, e) => Quit_EventHandler(s, e);
        }

        private void Quit_EventHandler(object s, EventArgs e)
        {
            mainWindow.Close();
        }

        public void filterContent()
        {
            string country = "";
            if (mainWindow.CountryTextBox.SelectedItem != null)
            {
                country = mainWindow.CountryTextBox.SelectedItem.ToString();
            }

            string city = "";
            if (mainWindow.CityTextBox.SelectedItem != null)
            {
                city = mainWindow.CityTextBox.SelectedItem.ToString();
            }

            string language = " ";
            if (mainWindow.LanguageTextBox.SelectedItem != null)
            {
                language = mainWindow.LanguageTextBox.SelectedItem.ToString();
            }

            int capacity = 0;
            if (!String.IsNullOrWhiteSpace(mainWindow.Capacity.Text) && IsTextAllowed(mainWindow.Capacity.Text))
            {
                capacity = Convert.ToInt32(mainWindow.Capacity.Text);
            }
            int duration = 0;
            if (!String.IsNullOrWhiteSpace(mainWindow.Duration.Text) && IsTextAllowed(mainWindow.Duration.Text))
            {
                duration = Convert.ToInt32(mainWindow.Duration.Text);
            }
            string search = mainWindow.searchBox.Text;
            tours.dailyToursControlViewModel.SearchByCriteria(country, city, language, capacity, duration, search);

        }

        private bool IsTextAllowed(string text)
        {
            // Only allow numeric input
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }

        internal void CapacityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
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

        internal void CapacityTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; 
            }
        }
    }
}
