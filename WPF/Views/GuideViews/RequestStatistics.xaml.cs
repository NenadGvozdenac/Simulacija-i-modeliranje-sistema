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
    /// Interaction logic for RequestStatistics.xaml
    /// </summary>
    public partial class RequestStatistics : Window
    {
        public RequestsViewModel requestsViewModel {  get; set; }

        public RequestStatistics()
        {
            InitializeComponent();
            requestsViewModel = new RequestsViewModel(this);
            DataContext = requestsViewModel;
        }

        public void Country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            requestsViewModel.Country_SelectionChanged();
        }

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            requestsViewModel.City_SelectionChanged();
        }

        private void Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            requestsViewModel.Language_SelectionChanged();
        }
    }
}
