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
using System.Windows.Shapes;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.TouristViewModels;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window
    {
        TouristMainWindowViewModel touristMainWindowViewModel {  get; set; }
        public TouristMainWindow(User user)
        {
            InitializeComponent();
            touristMainWindowViewModel = new TouristMainWindowViewModel(user, this, TouristWindowFrame);
            DataContext = touristMainWindowViewModel;
            Update(user);
        }

        
        public void ShowTourDetails(int tourId)
        {
            touristMainWindowViewModel.ShowTourDetails(tourId);
        }

        public void ShowTourDates(Tour tour, int guestNumber, List<Tourist> tourists, TourVoucher tourVoucher)
        {
            touristMainWindowViewModel.ShowTourDates(tour, guestNumber, tourists, tourVoucher);
        }
        public void ShowAlternativeTours(int locationId, Tour tour)
        {
            touristMainWindowViewModel.ShowAlternativeTours(locationId, tour);
        }
        public void Update(User user)
        {
            touristMainWindowViewModel.Update(user);

        }

        public void MyTours_Click(object sender, RoutedEventArgs e)
        {
            touristMainWindowViewModel.MyTours_Click(sender, e);
        }

        public void RateTour(User user, TouristReservationRepository touristReservationRepository, TourRepository tourRepository, TourReviewRepository tourReviewRepository, TourReviewImageRepository tourReviewImageRepository, int tourId)
        {
            touristMainWindowViewModel.RateTour(user, touristReservationRepository, tourRepository, tourReviewRepository, tourReviewImageRepository, tourId);
        }

        public void Home_Click(object sender, MouseButtonEventArgs e)
        {
            touristMainWindowViewModel.Home_Click(sender, e);
        }

        public void MyVouchers_Click(object sender, RoutedEventArgs e)
        {
            touristMainWindowViewModel.MyVouchers_Click(sender, e);
        }

        internal void ShowTourDates(Tour selectedTour, int guestNumber, List<Tourist> tourists)
        {
            touristMainWindowViewModel.ShowTourDates(selectedTour, guestNumber, tourists);
        }
    }
}
