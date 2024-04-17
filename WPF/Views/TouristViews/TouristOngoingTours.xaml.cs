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
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.TouristViewModels;
using BookingApp.WPF.Views.TouristViews.Components;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for TouristOngoingTours.xaml
    /// </summary>
    public partial class TouristOngoingTours : UserControl
    {
        TouristOngoingToursViewModel TouristOngoingToursViewModel { get; set; }
        public TouristOngoingTours(User user)
        {
            InitializeComponent();
            TouristOngoingToursViewModel = new TouristOngoingToursViewModel(user, this);
            DataContext = TouristOngoingToursViewModel;
            Update();
        }

        public void Update()
        {
            TouristOngoingToursViewModel.Update();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Ongoing_Loaded(object sender, RoutedEventArgs e)
        {
            TouristOngoingToursViewModel.Ongoing_Loaded(sender, e);
        }
        
    }
}
