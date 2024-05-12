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
    }
}
