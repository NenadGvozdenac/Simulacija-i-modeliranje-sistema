using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.TouristViewModels;
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
    /// Interaction logic for TouristNotifications.xaml
    /// </summary>
    public partial class TouristNotifications : UserControl
    {

        public TouristNotificationsViewModel TouristNotificationsViewModel { get; set; }
        public TouristNotifications(User user)
        {
            InitializeComponent();
            TouristNotificationsViewModel = new TouristNotificationsViewModel(user, this);
            DataContext = TouristNotificationsViewModel;
            Update();
        }
        public void Update()
        {
            TouristNotificationsViewModel.Update();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
