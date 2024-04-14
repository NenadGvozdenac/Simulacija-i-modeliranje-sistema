using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuideViewModels;
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

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for DemographicsControl.xaml
    /// </summary>
    public partial class DemographicsControl : UserControl
    {
        public EventHandler<BeginButtonClickedEventArgs> StatsButtonClickedControl { get; set; }

        public DemographicsControlViewModel demographicsControlViewModel { get; set; }

        public DemographicsControl()
        {
            InitializeComponent();
            demographicsControlViewModel = new DemographicsControlViewModel(this);
            DataContext = demographicsControlViewModel;
            Update();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            demographicsControlViewModel.Update();
        }

        private void demographicscard_TourStatsClicked(object sender, BeginButtonClickedEventArgs e) 
        {
            demographicsControlViewModel.demographicscard_TourStatsClicked(sender, e);
        }

        private void onStatsButtonClicked(BeginButtonClickedEventArgs e)
        {
            demographicsControlViewModel?.onStatsButtonClicked(e);
        }

    }
}
