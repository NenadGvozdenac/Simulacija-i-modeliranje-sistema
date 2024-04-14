using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.TouristViewModels;
using BookingApp.WPF.Views.TouristViews;
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
    /// Interaction logic for AlternativeTours.xaml
    /// </summary>
    public partial class AlternativeTours : UserControl, INotifyPropertyChanged
    {
        public AlternativeToursViewModel alternativeToursViewModel {  get; set; }

        public AlternativeTours(int locationId, Tour tour)
        {
            InitializeComponent();
            alternativeToursViewModel = new AlternativeToursViewModel(locationId, tour, this);
            DataContext = alternativeToursViewModel;
            Update();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update()
        {
            alternativeToursViewModel.Update();
        }

        private void FindToursWithLocation()
        {
            alternativeToursViewModel.FindToursWithLocation();

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            alternativeToursViewModel.Return_Click(sender, e);
        }
    }
}
