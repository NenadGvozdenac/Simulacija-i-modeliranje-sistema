using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuideViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for DailyToursControl.xaml
    /// </summary>
    public partial class DailyToursControl : UserControl
    {
        

        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedControl { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> EndButtonClickedControl { get; set; }

        private readonly User _user;

        public DailyToursControlViewModel dailyToursControlViewModel { get; set; }

        public DailyToursControl(User user)
        {
            InitializeComponent();
            dailyToursControlViewModel = new DailyToursControlViewModel(this,user);
            DataContext = dailyToursControlViewModel;
           
            
            _user = user;
           
        
        }

        public void Update()
        {
           dailyToursControlViewModel.Update();
        }

       
        public int CheckIfPassed(TourStartTime startTime)
        {
           return dailyToursControlViewModel.CheckIfPassed(startTime);
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DailyTourCard_BeginButtonClicked(object sender, BeginButtonClickedEventArgs e)
        {
            dailyToursControlViewModel.DailyTourCard_BeginButtonClicked(sender, e);
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            dailyToursControlViewModel.OnBeginButtonClicked(e);
        }


        private void DailyTourCard_EndButtonClicked(object sender, BeginButtonClickedEventArgs e)
        {
            dailyToursControlViewModel.DailyTourCard_EndButtonClicked(sender, e);
        }

        public void OnEndButtonClicked(BeginButtonClickedEventArgs e)
        {
            dailyToursControlViewModel.OnEndButtonClicked(e);
        }
    }
}
