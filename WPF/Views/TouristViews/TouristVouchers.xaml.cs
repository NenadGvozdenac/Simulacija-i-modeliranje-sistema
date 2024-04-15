using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.Repositories;
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
    /// Interaction logic for TouristVouchers.xaml
    /// </summary>
    public partial class TouristVouchers : UserControl
    {
        public TouristVouchersViewModel touristVouchersViewModel { get; set; }
        public TouristVouchers(int userId)
        {
            InitializeComponent();
            touristVouchersViewModel = new TouristVouchersViewModel(userId, this);
            DataContext = touristVouchersViewModel;
            Update();
        }
       
        public void Update()
        {
            touristVouchersViewModel.Update();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
