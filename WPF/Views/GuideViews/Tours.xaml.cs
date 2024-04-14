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
using BookingApp.View.TouristViews;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuideViewModels;

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for Tours.xaml
    /// </summary>
    public partial class Tours : UserControl
    {
      

        public ToursViewModel toursViewModel { get; set; }  

        public Tours()
        {
            InitializeComponent();
            toursViewModel = new ToursViewModel(this);
            DataContext = toursViewModel;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            toursViewModel.Update();
            
        }

        private void tourcard_CancelTourClicked(object sender,  BeginButtonClickedEventArgs e){
            toursViewModel.tourcard_CancelTourClicked(sender, e);
        }
   
    
    
    
    
    }
}
