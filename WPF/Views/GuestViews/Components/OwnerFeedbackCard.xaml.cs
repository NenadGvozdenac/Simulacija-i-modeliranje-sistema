using BookingApp.Application.UseCases;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.GuestDTOs;
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

namespace BookingApp.WPF.Views.GuestViews.Components;
public partial class OwnerFeedbackCard : UserControl
{
    public event EventHandler<int> ReviewHandler;
    public OwnerFeedbackCard()
    {
        InitializeComponent();
    }
    private void SetUpCard(object sender, DependencyPropertyChangedEventArgs e)
    {
        GuestRating review = (GuestRating)DataContext;

        if (AccommodationReviewService.GetInstance().GetAll().Any(reservation => reservation.ReservationId == review.ReservationId)){

            LeaveReviewGrid.Visibility = Visibility.Collapsed;
            FeedbackGrid.Visibility = Visibility.Visible;
            AccommodationReservation reservation = AccommodationReservationService.GetInstance().GetById(review.ReservationId);
            AccommodationName.Text = AccommodationService.GetInstance().GetById(review.AccommodationId).Name;
            AvailableDates date = new AvailableDates(reservation.FirstDateOfStaying, reservation.LastDateOfStaying);
            ReservationDate.Text = "Date of reservation: " + date;
            Cleanliness.Text = "cleanliness: " + review.Cleanliness + "/5";
            Respectfulness.Text = "respectfulness: " + review.Respectfulness + "/5";
        }
        else
        {

        }
    }

    private void LeaveReview_Click(object sender, RoutedEventArgs e)
    {
        GuestRating review = (GuestRating)DataContext;
        ReviewHandler?.Invoke(this, review.ReservationId);
    }
}
