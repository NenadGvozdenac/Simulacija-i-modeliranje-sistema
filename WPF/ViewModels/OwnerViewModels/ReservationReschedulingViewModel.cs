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

    public void Update()
    {
        _reservationsMoving = AccommodationReservationService.GetInstance().GetMovingsByOwnerId(_user.Id)
            .Where(x => x.Status == ReschedulingStatus.Pending)
            .Where(x => x.Reservation.ReservationType == ReservationType.Upcoming)
            .ToList();

        AddToPanel();
    }

    private void AddToPanel()
    {
        _reservationReschedulingPage.MainPanel.Children.Clear();

        foreach (AccommodationReservationMoving moving in _reservationsMoving)
        {
            ReservationRescheduling reservationRescheduling = new ReservationRescheduling(_reservationReschedulingPage, moving);
            reservationRescheduling.Margin = new Thickness(15);

            _reservationReschedulingPage.MainPanel.Children.Add(reservationRescheduling);
        }
    }
}
