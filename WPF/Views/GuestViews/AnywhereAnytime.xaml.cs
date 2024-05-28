using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.GuestViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AnywhereAnytime.xaml
    /// </summary>
    public partial class AnywhereAnytime : UserControl, INotifyPropertyChanged
    {
        public AnywhereAnytimeViewModel AnywhereAnytimeViewModel { get; set; }

        public AnywhereAnytime(User user)
        {
            InitializeComponent();
            AnywhereAnytimeViewModel = new AnywhereAnytimeViewModel(this, user);
            DataContext = AnywhereAnytimeViewModel;
            lastDate.IsEnabled = false;
            firstDate.DisplayDateStart = DateTime.Now;
        }

        public void DatePickerCantWrite(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void DaysOfStayTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Regular expression to check if input is a digit
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void GuestNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Regular expression to check if input is a digit
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void FirstDateChanged(object sender, SelectionChangedEventArgs e)
        {
                DateTime selectedDate = firstDate.SelectedDate.Value;
                lastDate.DisplayDateStart = selectedDate.AddDays(1);
                lastDate.IsEnabled = true;
        }

        private void FindPlaces_Click(object sender, RoutedEventArgs e)
        {
            AnywhereAnytimeViewModel.FindPlaces_Click();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
