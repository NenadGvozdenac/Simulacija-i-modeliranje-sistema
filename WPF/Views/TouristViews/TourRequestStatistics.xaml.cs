using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using BookingApp.WPF.ViewModels.TouristViewModels;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for TourRequestStatistics.xaml
    /// </summary>
    public partial class TourRequestStatistics : UserControl, INotifyPropertyChanged
    {
        public TourRequestStatisticsViewModel tourRequestStatisticsViewModel {  get; set; }
        public TourRequestStatistics()
        {
            InitializeComponent();
            tourRequestStatisticsViewModel = new TourRequestStatisticsViewModel(this);
            DataContext = tourRequestStatisticsViewModel;

            //GetTourRequestNumber();

            //GetTourRequestNumberForLocation();

            //GetPercentages();
            //PopulateYearsComboBox();
            //GetAverageNumOfGuests();
        }

        public void GetTourRequestNumber()
        {
            tourRequestStatisticsViewModel.GetTourRequestNumber();
        }
        public void GetTourRequestNumberForLocation()
        {
            tourRequestStatisticsViewModel.GetTourRequestNumberForLocation();
        }

        public string[] GetLocations()
        {
            return tourRequestStatisticsViewModel.GetLocations();
        }
        public string[] GetLanguages()
        {
            return tourRequestStatisticsViewModel.GetLanguages();
        }
        public void GetAverageNumOfGuests()
        {
            tourRequestStatisticsViewModel.GetAverageNumOfGuests();
        }
        public void GetAverageNumOfGuestsForYear(int year)
        {
            tourRequestStatisticsViewModel.GetAverageNumOfGuestsForYear(year);
        }
        public void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourRequestStatisticsViewModel.ComboBox2_SelectionChanged(sender, e);
        }
        public void GetPercentages()
        {
            tourRequestStatisticsViewModel.GetPercentages();
        }

        public void PopulateYearsComboBox()
        {
            tourRequestStatisticsViewModel.PopulateYearsComboBox();
        }
        public List<int> GetYears()
        {
            return tourRequestStatisticsViewModel.GetYears();
        }
        public List<int> GetYearsWithValidRequests()
        {
            return tourRequestStatisticsViewModel.GetYearsWithValidRequests();
        }
        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourRequestStatisticsViewModel.ComboBox_SelectionChanged(sender, e);
        }

        public void GetPercentagesForYear(int year)
        {
            tourRequestStatisticsViewModel.GetPercentagesForYear(year);

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
