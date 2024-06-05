using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
using BookingApp.View.PathfinderViews;
using BookingApp.WPF.ViewModels.TouristViewModels;
using BookingApp.WPF.Views.TouristViews;


namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for TouristDetails.xaml
    /// </summary>
    public partial class TouristDetails : UserControl
        {
            
            public Tour selectedTour { get; set; }
            public TouristDetailsViewModel touristDetailsViewModel {  get; set; }
            public TouristDetails(Tour detailedTour, User user)
            {
                InitializeComponent();
                touristDetailsViewModel = new TouristDetailsViewModel(detailedTour, user, this, TouristWindowFrame);
                DataContext = touristDetailsViewModel;
            }
        public void Reserve_Click(object sender, RoutedEventArgs e)
        {
            touristDetailsViewModel.Reserve_Click(sender, e);
        }
    }

    }
