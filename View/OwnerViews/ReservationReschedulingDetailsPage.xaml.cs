using BookingApp.Miscellaneous;
using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Resources.Types;
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

namespace BookingApp.View.OwnerViews;

public partial class ReservationReschedulingDetailsPage : Page
{
    public AccommodationReservationMoving AccommodationReservationMoving { get; set; }
    public int DaysOfReservation { get; set; }
    public string Comment { get; set; }
    public string StatusOfWantedTimespan { get; set; }
    public string DayBeforeCancellationIsFinal { get; set; }
    public EventHandler ReservationReschedulingDetailsPageClosed { get; internal set; }

    public ReservationReschedulingDetailsPage(AccommodationReservationMoving accommodationReservationMoving)
    {
        InitializeComponent();
        DataContext = this;
        this.AccommodationReservationMoving = accommodationReservationMoving;
        this.DaysOfReservation = (accommodationReservationMoving.CurrentReservationTimespan.End - accommodationReservationMoving.CurrentReservationTimespan.Start).Days;
        this.DayBeforeCancellationIsFinal = DateParser.ToString(accommodationReservationMoving.CurrentReservationTimespan.Start.AddDays(-accommodationReservationMoving.Accommodation.CancellationPeriodDays));
        CalculateStatus();
    }

    private void CalculateStatus()
    {
        if(AccommodationReservationRepository.GetInstance().IsTimespanFree(AccommodationReservationMoving.WantedReservationTimespan, AccommodationReservationMoving.Accommodation))
        {
            StatusOfWantedTimespan = "Reservation can be moved to wanted timespan";
            StatusLabel.Foreground = Brushes.Green;
        } else
        {
            StatusOfWantedTimespan = "Reservation timespan is already reserved.";
            StatusLabel.Foreground = Brushes.Red;
        }
    }

    private void Back_Click(object sender, MouseButtonEventArgs e)
    {
        NavigateBack();
    }

    private void NavigateBack()
    {
        if (NavigationService.CanGoBack)
        {
            InvokePageClosed();
            NavigationService.GoBack();
        }
    }

    private void InvokePageClosed()
    {
        ReservationReschedulingDetailsPageClosed?.Invoke(this, EventArgs.Empty);
    }

    private void Reject_Click(object sender, RoutedEventArgs e)
    {
        this.AccommodationReservationMoving.Comment = Comment;
        this.AccommodationReservationMoving.Status = ReschedulingStatus.Rejected;

        AccommodationReservationMovingRepository.GetInstance().Update(this.AccommodationReservationMoving);

        NavigateBack();
    }

    private void Accept_Click(object sender, RoutedEventArgs e)
    {
        this.AccommodationReservationMoving.Status = ReschedulingStatus.Accepted;

        AccommodationReservationMovingRepository.GetInstance().Update(this.AccommodationReservationMoving);

        NavigateBack();
    }

    private void CheckBox_Checked_Change(object sender, RoutedEventArgs e)
    {
        CommentTextbox.IsEnabled = (sender as CheckBox)?.IsChecked == true;
    }
}
