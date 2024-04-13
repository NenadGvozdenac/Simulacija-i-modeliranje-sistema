using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.OwnerViews.Components;
using BookingApp.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using BookingApp.Resources.Types;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class ReservationReschedulingViewModel
{
    private readonly User _user;

    private ReservationReschedulingPage _reservationReschedulingPage;
    private List<AccommodationReservationMoving> _reservationsMoving;

    public ReservationReschedulingViewModel(ReservationReschedulingPage reservationReschedulingPage, User user)
    {
        _user = user;

        _reservationReschedulingPage = reservationReschedulingPage;

        Update();
    }

    private void Update()
    {
        _reservationsMoving = AccommodationReservationService.GetInstance().GetMovingsByOwnerId(_user.Id).Where(x => x.Status == ReschedulingStatus.Pending).ToList();

        AddToPanel();
    }

    private void AddToPanel()
    {
        _reservationReschedulingPage.MainPanel.Children.Clear();

        foreach (AccommodationReservationMoving moving in _reservationsMoving)
        {
            ReservationRescheduling reservationRescheduling = new ReservationRescheduling(moving);
            reservationRescheduling.Margin = new Thickness(15);

            reservationRescheduling.ReservationReschedulingDetails += (s, e) => ShowDetails(e);

            _reservationReschedulingPage.MainPanel.Children.Add(reservationRescheduling);
        }
    }

    private void ShowDetails(AccommodationReservationMoving e)
    {
        ReservationReschedulingDetailsPage reservationReschedulingDetailsPage = new ReservationReschedulingDetailsPage(e);
        reservationReschedulingDetailsPage.ReservationReschedulingDetailsPageClosed += (s, e) => Update();
        NavigationService.GetNavigationService(_reservationReschedulingPage).Navigate(reservationReschedulingDetailsPage);
    }
}
