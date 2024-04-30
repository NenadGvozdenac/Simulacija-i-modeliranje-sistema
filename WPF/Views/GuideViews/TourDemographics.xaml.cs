using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuideViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using LiveCharts;
using LiveCharts.Wpf;

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for TourDemographics.xaml
    /// </summary>
    public partial class TourDemographics : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TourDemographicsViewModel tourDemographicsViewModel { get; set; }
 

        // The method to invoke the property changed event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public TourDemographics()
        {
            InitializeComponent();
            tourDemographicsViewModel = new TourDemographicsViewModel(this);
            DataContext = tourDemographicsViewModel;
            Update();
        }

        public void Update()
        {

        }

        public Tour FindMostReserved()
        {
           return tourDemographicsViewModel.FindMostReserved();
        }

        public List<Tourist> GroupTourists(Tour t) {

           return tourDemographicsViewModel.GroupTourists(t);
        }


        public int FindSub18(Tour t){
            return tourDemographicsViewModel.FindSub18(t);
        }

        public int FindMiddle(Tour t)
        {
           return tourDemographicsViewModel.FindMiddle(t);
        }

        public int FindAbove50(Tour t)
        {
            return tourDemographicsViewModel.FindAbove50(t);
        }

        public List<int> FindYears()
        {
            return tourDemographicsViewModel.FindYears();
        }

        public void OnStatsButtonClicked_Handler(object sender, BeginButtonClickedEventArgs e)
        {
            tourDemographicsViewModel.OnStatsButtonClicked_Handler(sender, e);
        }

        public void OnStatsButtonClicked(BeginButtonClickedEventArgs e)
        {
            tourDemographicsViewModel.OnStatsButtonClicked(e);
        }

        private void AllTime_Click(object sender, RoutedEventArgs e)
        {
           tourDemographicsViewModel.AllTime_Click(sender, e);
        }

        private void yearSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourDemographicsViewModel.yearSelectionChanged(sender, e);
        }

        private void yearSelectionChanged_Click(object sender, RoutedEventArgs e)
        {
            tourDemographicsViewModel.yearSelectionChanged_Click(sender, e);
        }

        public Tour FindMostReservedForYear(int i)
        {
            return tourDemographicsViewModel.FindMostReservedForYear((int)i);
        }

    }
}
