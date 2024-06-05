using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.GuestViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.GuestViews
{
    /// <summary>
    /// Interaction logic for Forums.xaml
    /// </summary>
    public partial class Forums : UserControl
    {
        private ForumsViewModel ForumsViewModel;

        public Forums(User user)
        {
            InitializeComponent();
            ForumsViewModel = new ForumsViewModel(this, user);
            DataContext = ForumsViewModel;
            LoadCountries();
        }

        private void LoadCountries()
        {
            List<string> listOfCountries = LocationService.GetInstance().GetCountries();
            CountryComboBox.ItemsSource = listOfCountries;
        }

        private void StartForum_click(object sender, RoutedEventArgs e)
        {
            ForumsViewModel.StartForum_click();
        }

        public void CountryComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            List<string> listOfCities = LocationService.GetInstance().GetCitiesByCountry(CountryComboBox.SelectedItem.ToString());
            CityComboBox.ItemsSource = listOfCities;
            CityComboBox.Focus();
            CityComboBox.IsEnabled = true;;
        }
    }
}
