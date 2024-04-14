using BookingApp.Domain.Models;
using BookingApp.Repositories;
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

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for DailyToursWindow.xaml
    /// </summary>
    public partial class DailyToursWindow : Window
    {
        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedWindow { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> EndButtonClickedWindow { get; set; }
       
        public DailyToursWindowViewModel dailyToursWindowViewModel { get; set; }
        
        public DailyToursWindow(User user)
        {
            InitializeComponent();
            dailyToursWindowViewModel = new DailyToursWindowViewModel(this,user);
        }

        private void DailyTours_SomeEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            dailyToursWindowViewModel.DailyTours_SomeEventHandler(sender, e);
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            dailyToursWindowViewModel.OnBeginButtonClicked(e);
        }

        private void DailyTours_EndEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            dailyToursWindowViewModel.DailyTours_EndEventHandler(sender, e);
        }

        public void OnEndButtonClicked(BeginButtonClickedEventArgs e)
        {
            dailyToursWindowViewModel.OnEndButtonClicked(e);
        }

    }
}
