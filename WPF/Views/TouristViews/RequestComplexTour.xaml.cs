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
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using BookingApp.Application.UseCases;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using BookingApp.WPF.ViewModels.TouristViewModels;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for RequestComplexTour.xaml
    /// </summary>
    public partial class RequestComplexTour : UserControl, INotifyPropertyChanged
    {
        public RequestComplexTourViewModel RequestComplexTourViewModel { get; set; }
        public RequestComplexTour(User _user)
        {
            InitializeComponent();
            RequestComplexTourViewModel = new RequestComplexTourViewModel(_user, this);
            DataContext = RequestComplexTourViewModel;

        }
        public void LoadCountries()
        {
            RequestComplexTourViewModel.LoadCountries();
        }
        public void LoadLanguages()
        {
            RequestComplexTourViewModel.LoadLanguages();
        }

        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RequestComplexTourViewModel.CountryComboBox_SelectionChanged(sender, e);
        }
        public void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RequestComplexTourViewModel.CityComboBox_SelectionChanged(sender, e);
        }
        public void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RequestComplexTourViewModel.LanguageComboBox_SelectionChanged(sender, e);
        }

        public void GetTourInfo()
        {
            RequestComplexTourViewModel.GetTourInfo();

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            RequestComplexTourViewModel.GuestAge_TextChanged(sender, e);
        }
        public void AddTourist_Click(object sender, RoutedEventArgs e)
        {
            RequestComplexTourViewModel.AddTourist_Click(sender, e);
        }
        public void RefreshTouristDataGrid()
        {
            RequestComplexTourViewModel.RefreshTouristDataGrid();
        }

        public void ClearInputFields()
        {
            RequestComplexTourViewModel.ClearInputFields();
        }

        public void CheckAllFields()
        {
            RequestComplexTourViewModel.CheckAllFields();
        }

        public void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            RequestComplexTourViewModel.AddRequest_Click(sender, e);
        }

        public void Finish_Click(object sender, RoutedEventArgs e)
        {
            RequestComplexTourViewModel.Finish_Click(sender, e);
        }
    }
}
