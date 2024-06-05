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
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindowViewModel infoWindowViewModel { get; set; }

        public User user { get; set; }
        public InfoWindow(User _user)
        {
            InitializeComponent();
            user = _user;
            infoWindowViewModel = new InfoWindowViewModel(this,user);
            DataContext = infoWindowViewModel;
        }

        private void Quit_click(object sender, RoutedEventArgs e)
        {
            infoWindowViewModel.quit_Click();
        }

        private void GeneratePdfReport_Click(object sender, RoutedEventArgs e)
        {
            infoWindowViewModel.GeneratePdfReport_Click(startPicker.SelectedDate, endPicker.SelectedDate);
        }
    }
}
