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
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.TouristViewModels;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for Tours.xaml
    /// </summary>
    public partial class Tours : UserControl, INotifyPropertyChanged
    {
        public ToursViewModel toursViewModel {  get; set; }

        public Tours(User user)
        {
            InitializeComponent();
            toursViewModel = new ToursViewModel(user, this);
            DataContext = toursViewModel;
            Update();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Update()
        {
            toursViewModel.Update();
        }

        public void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            toursViewModel.LanguageComboBox_SelectionChanged(sender, e);
        }
        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            toursViewModel.CountryComboBox_SelectionChanged(sender, e);
        }

        public void GuestNumberUp_Click(object sender, RoutedEventArgs e)
        {
            toursViewModel.GuestNumberUp_Click(sender, e);
        }
        public void GuestNumberDown_Click(object sender, RoutedEventArgs e)
        {
            toursViewModel.GuestNumberDown_Click(sender, e);
        }

        public void GuestNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            toursViewModel.GuestNumber_TextChanged(sender, e);
        }

        public void DaysOfStayUp_Click(object sender, RoutedEventArgs e)
        {
            toursViewModel.DaysOfStayUp_Click(sender, e);
        }
        public void DaysOfStayDown_Click(object sender, RoutedEventArgs e)
        {
            toursViewModel.DaysOfStayDown_Click(sender, e);
        }
        public void DaysOfStay_TextChanged(object sender, TextChangedEventArgs e)
        {
            toursViewModel.DaysOfStay_TextChanged(sender, e);
        }

        public void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            toursViewModel.ClearFilter_Click();
        }

    }
}
