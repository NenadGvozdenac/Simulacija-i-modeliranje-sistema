using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.TouristViewModels;
using BookingApp.WPF.Views.TouristViews.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for TourRequests.xaml
    /// </summary>
    public partial class TourRequests : UserControl
    {
        public TourRequestsViewModel tourRequestsViewModel { get; set; }
        public TourRequests(User _user)
        {
            InitializeComponent();
            tourRequestsViewModel = new TourRequestsViewModel(_user, this);
            DataContext = tourRequestsViewModel;
            Update();
        }
        public void Update()
        {
            tourRequestsViewModel.Update();
        }

        public void Add_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tourRequestsViewModel.Add_MouseLeftButtonDown(sender, e);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Statistics_Click(object sender, RoutedEventArgs e)
        {
            tourRequestsViewModel.Statistics_Click(sender, e);
        }
    }
}
