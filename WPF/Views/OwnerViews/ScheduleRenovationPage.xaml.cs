using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
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

namespace BookingApp.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for ScheduleRenovationPage.xaml
    /// </summary>
    public partial class ScheduleRenovationPage : Page
    {
        ScheduleRenovationViewModel _viewModel;
        public ScheduleRenovationPage(User user)
        {
            InitializeComponent();
            _viewModel = new ScheduleRenovationViewModel(this, user);
            DataContext = _viewModel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadData();
        }

        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
