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
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.TouristViewModels;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for RateTour.xaml
    /// </summary>
    public partial class RateTour : UserControl
    {
        public RateTourViewModel rateTourViewModel {  get; set; }
        public RateTour(User user, TouristReservationRepository touristReservationRepository, TourRepository tourRepository, TourReviewRepository tourReviewRepository, TourReviewImageRepository tourReviewImageRepository, int tourId)
        {
            InitializeComponent();
            rateTourViewModel = new RateTourViewModel(user, touristReservationRepository, tourRepository, tourReviewRepository, tourReviewImageRepository, tourId, this);
            DataContext = rateTourViewModel;
        }


        private void Star_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rateTourViewModel.Star_MouseLeftButtonDown(sender, e);
        }

        private void UpdateStarRatingUI(string starPrefix, int rating)
        {
            rateTourViewModel.UpdateStarRatingUI(starPrefix, rating);
        }
        private void Feedback_TextChanged(object sender, TextChangedEventArgs e)
        {
            rateTourViewModel.Feedback_TextChanged(sender, e);
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            rateTourViewModel.AddPhoto_Click(sender, e);
        }
        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            rateTourViewModel.Finish_Click(sender, e);
        }

        
    }
}
