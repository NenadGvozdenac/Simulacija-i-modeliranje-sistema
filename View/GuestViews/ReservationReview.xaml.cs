using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Services.Mutual;
using BookingApp.ViewModel.GuestViewModels;
using BookingApp.ViewModel.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.GuestViews;

/// <summary>
/// Interaction logic for ReservationReview.xaml
/// </summary>
public partial class ReservationReview : UserControl
{
    public ReservationReviewViewModel ReservationReviewViewModel { get; set; }

    public ReservationReview(User user, AccommodationReservationRepository accommodationReservationRepository, AccommodationRepository accommodationRepository, AccommodationReviewRepository accommodationReviewRepository, ReviewImageRepository reviewImageRepository, int reservationId)
    {
        InitializeComponent();
        ReservationReviewViewModel = new ReservationReviewViewModel(this, user, accommodationReservationRepository, accommodationRepository, accommodationReviewRepository, reviewImageRepository, reservationId);
        DataContext = ReservationReviewViewModel;
    }

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        ReservationReviewViewModel.GoBack_Click();
    }

    private void FinishReview_Click(object sender, RoutedEventArgs e)
    {
        ReservationReviewViewModel.FinishReview_Click();
    }

    private void AddPhoto_Click(object sender, RoutedEventArgs e)
    {
        ReservationReviewViewModel.AddPhoto_Click();
    }
}
