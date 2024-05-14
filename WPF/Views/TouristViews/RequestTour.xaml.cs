using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.WPF.ViewModels.TouristViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for RequestTour.xaml
    /// </summary>
    public partial class RequestTour : UserControl, INotifyPropertyChanged
    {
        public RequestTourViewModel requestTourViewModel {  get; set; }
        public RequestTour(User _user)
        {
            InitializeComponent();
            requestTourViewModel = new RequestTourViewModel(_user, this);
            DataContext = requestTourViewModel;
            //LoadCountries();
            //LoadLanguages();
        }

        public void LoadCountries()
        {
            requestTourViewModel.LoadCountries();
        }
        public void LoadLanguages()
        {
            requestTourViewModel.LoadLanguages();
        }

        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            requestTourViewModel.CountryComboBox_SelectionChanged(sender, e);
        }
        public void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            requestTourViewModel.CityComboBox_SelectionChanged(sender, e);
        }
        public void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            requestTourViewModel.LanguageComboBox_SelectionChanged(sender, e);
        }

        public void GetTourInfo()
        {
            requestTourViewModel.GetTourInfo();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            requestTourViewModel.GuestAge_TextChanged(sender,e);
        }
        public void AddTourist_Click(object sender, RoutedEventArgs e)
        {
            requestTourViewModel.AddTourist_Click(sender, e);
        }
        public void RefreshTouristDataGrid()
        {
            requestTourViewModel.RefreshTouristDataGrid();
        }

        private void ClearInputFields()
        {
            requestTourViewModel.ClearInputFields();
        }

        public void CheckAllFields()
        {
            requestTourViewModel.CheckAllFields();
        }
        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            requestTourViewModel.Finish_Click(sender, e);
        }
    }
}
