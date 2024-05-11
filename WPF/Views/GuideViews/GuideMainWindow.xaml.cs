using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.Repositories;
using BookingApp.View.PathfinderViews.Componentss;
using BookingApp.WPF.ViewModels.GuideViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for GuideMainWindow.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {


        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedMain { get; set; }

        public GuideMainWindowViewModel mainWindowViewModel { get; set; }
        public GuideMainWindow(User user)
        {
            InitializeComponent();
            mainWindowViewModel = new GuideMainWindowViewModel(this, user);
            DataContext = mainWindowViewModel;
            Update();
        }

        public void ScheduleTourClick(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.ScheduleTourClick(sender, e);
        }

        public void DailyToursClick(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.DailyToursClick(sender, e);
        }

        public int ongoingTourCheck()
        {
            return mainWindowViewModel.ongoingTourCheck();
        }


        public void Update()
        {
            mainWindowViewModel.Update();
        }
        public void ChangeTourStatusOngoing(int tourId, DateTime dateTime)
        {
            mainWindowViewModel.ChangeTourStatusOngoing((int)tourId, dateTime);
        }

        public void ChangeTourStatusPassed(int tourId, DateTime dateTime)
        {
            mainWindowViewModel.ChangeTourStatusPassed((int)tourId, dateTime);
        }

        private void Demographics_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindowViewModel.Demographics_MouseLeftButtonDown(sender, e);
        }

        private void RequestsStatistics_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindowViewModel.RequestsStatistics_MouseLeftButtonDown(sender, e);
        }

        private void Reviews_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindowViewModel.Reviews_MouseLeftButtonDown(sender, e);
        }

        // HANDLERS
        private void DailyToursWindow_SomeEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.DailyToursWindow_SomeEventHandler(sender, e);
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.OnBeginButtonClicked(e);
        }
        private void DailyToursWindow_EndEventHandlerDaily(object sender, BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.DailyToursWindow_EndEventHandlerDaily(sender, e);
        }

        public void OnEndButtonClickedDaily(BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.OnEndButtonClickedDaily(e);
        }

        private void DailyToursWindow_EndEventHandlerMain(object sender, BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.DailyToursWindow_EndEventHandlerMain(sender, e);
        }

        public void OnEndButtonClickedMain(BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.OnEndButtonClickedMain(e);
        }

        private void OngoingTour_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.OngoingTour_Click(sender, e);
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mainWindowViewModel.searchBox_TextChanged(sender, e);
        }

        private void CountryTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mainWindowViewModel.CountryTextBox_SelectionChanged(sender, e);
        }

        

        private void Capacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            mainWindowViewModel.Capacity_TextChanged(sender, e);
        }

        private void CapacityUp_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.CapacityUp_Click(sender,e);
        }

        private void CapacityDown_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.CapacityDown_Click(sender, e);
        }

        private void Duration_TextChanged(object sender, TextChangedEventArgs e)
        {
            mainWindowViewModel.Duration_TextChanged(sender, e);
        }

        private void DurationUp_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.DurationUp_Click(sender, e);
        }

        private void DurationDown_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.DurationDown_Click(sender, e);
        }

        private void CityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mainWindowViewModel.CityTextBox_SelectionChanged(sender, e);
        }

        private void LanguageTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mainWindowViewModel.LanguageTextBox_SelectionChanged(sender, e);
        }

        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.ClearFilters_Click();
        }

        
    }
}