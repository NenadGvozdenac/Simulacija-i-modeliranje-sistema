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
using BookingApp.Model.MutualModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using BookingApp.View.PathfinderViews.Componentss;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.TouristViewModels;
using BookingApp.WPF.Views;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for VisitedTours.xaml
    /// </summary>
    public partial class VisitedTours : UserControl, INotifyPropertyChanged
    {
        
        public VisitedToursViewModel visitedToursViewModel { get; set; }
        public VisitedTours(User user)
        {
            InitializeComponent();
            visitedToursViewModel = new VisitedToursViewModel(user, this);
            DataContext = visitedToursViewModel;
            Update();
            
        }

        public void Update()
        {
            visitedToursViewModel.Update();
        }

        public void TourCardWithStar_Loaded(object sender, RoutedEventArgs e)
        {
            visitedToursViewModel.TourCardWithStar_Loaded(sender, e);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
