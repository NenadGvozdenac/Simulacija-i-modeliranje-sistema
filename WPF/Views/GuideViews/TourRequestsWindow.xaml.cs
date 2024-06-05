using BookingApp.Domain.Models;
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

namespace BookingApp.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for TourRequestsWindow.xaml
    /// </summary>
    public partial class TourRequestsWindow : Window
    {
        public TourRequestWindowViewModel tourRequestsWindowViewModel { get; set; }

        public User user { get; set; }
        public TourRequestsWindow(User _user)
        {
            InitializeComponent();
            user = _user;
            tourRequestsWindowViewModel = new TourRequestWindowViewModel(this,user);
            
        }

        private void Country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourRequestsWindowViewModel.Country_SelectionChanged();
        }

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourRequestsWindowViewModel.City_SelectionChanged();
        }

        private void Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourRequestsWindowViewModel.City_SelectionChanged();
        }

        private void CapacityUp_Click(object sender, RoutedEventArgs e)
        {
            tourRequestsWindowViewModel.CapacityUp_Click();
        }

        private void CapacityDown_Click(object sender, RoutedEventArgs e)
        {
            tourRequestsWindowViewModel.CapacityDown_Click();
        }

        private void Capacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            tourRequestsWindowViewModel.City_SelectionChanged();
        }

        private void Picker1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourRequestsWindowViewModel.City_SelectionChanged();
        }

        private void Picker2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourRequestsWindowViewModel.City_SelectionChanged();
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            tourRequestsWindowViewModel.ResetFilters_Click();
        }

        private void CapacityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            tourRequestsWindowViewModel.CapacityTextBox_PreviewTextInput(sender, e);
        }

        private void CapacityTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            tourRequestsWindowViewModel.CapacityTextBox_PreviewKeyDown(sender, e);
        }
    }
}
