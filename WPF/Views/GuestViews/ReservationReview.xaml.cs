using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuestViewModels;
using BookingApp.WPF.ViewModels.OwnerViewModels;
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

namespace BookingApp.WPF.Views.GuestViews;

/// <summary>
/// Interaction logic for ReservationReview.xaml
/// </summary>
public partial class ReservationReview : UserControl
{
    public ReservationReviewViewModel ReservationReviewViewModel { get; set; }

    public ReservationReview(User user, int reservationId)
    {
        InitializeComponent();
        ReservationReviewViewModel = new ReservationReviewViewModel(this, user, reservationId);
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
