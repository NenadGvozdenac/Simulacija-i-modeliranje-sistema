using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Resources.Types;
using BookingApp.WPF.DTOs.OwnerDTOs;
using BookingApp.WPF.ViewModels.OwnerViewModels;
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
        _detailedReservationMovingViewModel = new DetailedReservationMovingViewModel(this, new AccommodationReservationMovingDTO(accommodationReservationMoving));
        DataContext = _detailedReservationMovingViewModel;
        InitializeComponent();
        StatusLabel.Foreground = _detailedReservationMovingViewModel.AccommodationReservationMovingDTO.GetLabelColor();
    }

    private void Back_Click(object sender, MouseButtonEventArgs e)
    {
        NavigateBack();
    }

    public void NavigateBack()
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

    private void CheckBox_Checked_Change(object sender, RoutedEventArgs e)
    {
        CommentTextbox.IsEnabled = (sender as CheckBox)?.IsChecked == true;
    }
}
