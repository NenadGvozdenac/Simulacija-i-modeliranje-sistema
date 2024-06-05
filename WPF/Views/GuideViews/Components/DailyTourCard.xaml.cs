using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.OwnerViews.AnimatorHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.View.PathfinderViews.Componentss
{
    /// <summary>
    /// Interaction logic for DailyTourCard.xaml
    /// </summary>
    public partial class DailyTourCard : UserControl, INotifyPropertyChanged
    {
        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClicked { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> EndButtonClicked { get; set; }


        public TourStartTimeRepository _timeRepository {  get; set; }

        private bool ongoing;

        public bool Ongoing
        {
            get { return ongoing; }
            set
            {
                if (ongoing != value)
                {
                    ongoing = value;
                    OnPropertyChanged(nameof(Ongoing));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DailyTourCard()
        {
            InitializeComponent();
            Update();
            _timeRepository = new TourStartTimeRepository();
            
        }


      public void BeginButton_Click  (object sender, RoutedEventArgs e) 
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to start this tour", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                OnBeginButtonClicked(new BeginButtonClickedEventArgs(Convert.ToInt32(IdTextBlock.Text), Convert.ToDateTime(DateTextBlock.Text)));
                CheckpointsView checkpointsView = new CheckpointsView(Convert.ToInt32(IdTextBlock.Text), Convert.ToDateTime(DateTextBlock.Text));
                checkpointsView.checkpointsViewModel.EndButtonClicked += (s, e) => CheckpointsWindow_SomeEventHandler(s, e);
                checkpointsView.ShowDialog();
            }

        }    

       public void Update()
        {
            if (TourStartTimeService.GetInstance().GetAll().Find(a => a.Status == "ongoing") != null)
            {
                BeginButton.IsEnabled = false;
                BeginButton.Opacity = 0.5;
            }
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            BeginButtonClicked?.Invoke(this, e);
            BeginButton.IsEnabled = false;
            BeginButton.Opacity = 0.5;
        }


        public void CheckpointsWindow_SomeEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            OnEndButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }




        public void OnEndButtonClicked(BeginButtonClickedEventArgs e)
        {
            EndButtonClicked?.Invoke(this, e);
            
       }




    }
}
