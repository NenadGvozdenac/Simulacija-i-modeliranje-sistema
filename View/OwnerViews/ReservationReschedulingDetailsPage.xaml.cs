using BookingApp.DTOs.OwnerDTOs;
using BookingApp.Miscellaneous;
using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Resources.Types;
using BookingApp.Services.Owner;
using BookingApp.ViewModel.OwnerViewModels;
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
    private DetailedReservationMovingViewModel _detailedReservationMovingViewModel;
    public EventHandler ReservationReschedulingDetailsPageClosed { get; internal set; }

    public ReservationReschedulingDetailsPage(AccommodationReservationMoving accommodationReservationMoving)
    {
        _detailedReservationMovingViewModel = new DetailedReservationMovingViewModel(new AccommodationReservationMovingDTO(accommodationReservationMoving));
        DataContext = _detailedReservationMovingViewModel;
        InitializeComponent();
        CalculateStatus();
    }

    private void CalculateStatus()
    {
        DateSpan wantedSpan = _detailedReservationMovingViewModel.AccommodationReservationMovingDTO.AccommodationReservationMoving.WantedReservationTimespan;
        Accommodation accommodation = _detailedReservationMovingViewModel.AccommodationReservationMovingDTO.AccommodationReservationMoving.Accommodation;
        
        if (AccommodationReservationRepository.GetInstance().IsTimespanFree(wantedSpan, accommodation))
        {
            _detailedReservationMovingViewModel.AccommodationReservationMovingDTO.StatusOfWantedTimespan = "Reservation can be moved to wanted timespan";
            StatusLabel.Foreground = Brushes.Green;
        } else
        {
            _detailedReservationMovingViewModel.AccommodationReservationMovingDTO.StatusOfWantedTimespan = "Reservation timespan is already reserved.";
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
        var accommodationMoving = _detailedReservationMovingViewModel.AccommodationReservationMovingDTO.AccommodationReservationMoving;
        accommodationMoving.Comment = CommentTextbox.Text;
        accommodationMoving.Status = ReschedulingStatus.Rejected;

        AccommodationReservationMovingRepository.GetInstance().Update(accommodationMoving);

        NavigateBack();
    }

    private void Accept_Click(object sender, RoutedEventArgs e)
    {
        var accommodationMoving = _detailedReservationMovingViewModel.AccommodationReservationMovingDTO.AccommodationReservationMoving;
        accommodationMoving.Status = ReschedulingStatus.Accepted;

        AccommodationReservationMovingRepository.GetInstance().Update(accommodationMoving);
        AccommodationReservationService.GetInstance().MoveReservation(accommodationMoving);

        NavigateBack();
    }

    private void CheckBox_Checked_Change(object sender, RoutedEventArgs e)
    {
        CommentTextbox.IsEnabled = (sender as CheckBox)?.IsChecked == true;
    }
}
