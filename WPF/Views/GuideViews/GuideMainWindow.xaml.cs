using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.Repositories;
using BookingApp.View.PathfinderViews.Componentss;
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
    /// Interaction logic for GuideMainWindow.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {
        

        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedMain { get; set; }

        public GuideMainWindowViewModel mainWindowViewModel { get; set; }
        public GuideMainWindow(User user)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
            mainWindowViewModel = new GuideMainWindowViewModel(this,user);
            Update();
        }

        public void ScheduleTourClick(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.ScheduleTourClick(sender,e);
        }

        public void DailyToursClick(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.DailyToursClick(sender,e);
        }

        public int ongoingTourCheck()
        {
            return mainWindowViewModel.ongoingTourCheck();
        }


        public void Update()
        {
            mainWindowViewModel.Update();
        }
        public void ChangeTourStatusOngoing(int tourId, DateTime dateTime)
        {
            mainWindowViewModel.ChangeTourStatusOngoing((int)tourId, dateTime);
        }

        public void ChangeTourStatusPassed(int tourId, DateTime dateTime)
        {
            mainWindowViewModel.ChangeTourStatusPassed((int)tourId, dateTime);
        }

        private void Demographics_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindowViewModel.Demographics_MouseLeftButtonDown(sender, e);
        }
        
       
        // HANDLERS
        private void DailyToursWindow_SomeEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.DailyToursWindow_SomeEventHandler(sender, e);
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.OnBeginButtonClicked(e);
        }
        private void DailyToursWindow_EndEventHandlerDaily(object sender, BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.DailyToursWindow_EndEventHandlerDaily(sender, e);
        }

        public void OnEndButtonClickedDaily(BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.OnEndButtonClickedDaily(e);
        }

        private void DailyToursWindow_EndEventHandlerMain(object sender, BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.DailyToursWindow_EndEventHandlerMain(sender, e);
        }

        public void OnEndButtonClickedMain(BeginButtonClickedEventArgs e)
        {
            mainWindowViewModel.OnEndButtonClickedMain(e);
        }


    }
}