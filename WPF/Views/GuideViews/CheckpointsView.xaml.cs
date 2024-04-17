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
using System.Windows.Shapes;

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for CheckpointsView.xaml
    /// </summary>
    public partial class CheckpointsView : Window, INotifyPropertyChanged
    {
       

        public EventHandler<BeginButtonClickedEventArgs> EndButtonClicked { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> EndButtonClickedMain { get; set; }

        public CheckpointsViewModel checkpointsViewModel {  get; set; } 

        public string tourName {  get; set; } 

       public int tourTimeId { get; set; }

       public int tourId {  get; set; }

       public DateTime _currentDate { get; set; }

        public CheckpointsView(int TourId, DateTime currentDate)
        {
            InitializeComponent();
            checkpointsViewModel = new CheckpointsViewModel(this, TourId, currentDate);
            DataContext = checkpointsViewModel;
        }

        public void Update(int TourId, DateTime currentDate)
        {
            checkpointsViewModel.Update(TourId, currentDate);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CheckpointsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            checkpointsViewModel.CheckpointsDataGrid_Loaded(sender, e);
        }

        private void CheckpointCheckboxBox_Checked(object sender, RoutedEventArgs e)
        {
            checkpointsViewModel.CheckpointCheckboxBox_Checked(sender, e);
        }

        
        private int CheckIfLast(CheckBox checkbox)
        {
           return checkpointsViewModel.CheckIfLast(checkbox);
        }

        private int GoBackToCheckbox(CheckBox checkbox)
        {
            return checkpointsViewModel.GoBackToCheckbox(checkbox);
        }

        private void TouristCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            checkpointsViewModel.TouristCheckBox_Checked(sender, e);
        }

        public void EndTour_Click(object sender, RoutedEventArgs e)
        {
            checkpointsViewModel.EndTour_Click(sender, e);
        }


        public void OnEndButtonClicked(BeginButtonClickedEventArgs e)
        {
            checkpointsViewModel.OnEndButtonClicked(e);
        }

        public void OnEndButtonClickedMain(BeginButtonClickedEventArgs e)
        {
            checkpointsViewModel.OnEndButtonClickedMain(e);
        }



    }
}
