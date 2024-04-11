using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Resources.Types;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace BookingApp.Application.Commands;

public class ReschedulingCommand : ICommand
{
    private AccommodationReservationMoving accommodationReservationMoving;
    private ReschedulingStatus status;
    private string comment;
    private ReservationReschedulingDetailsPage reservationReschedulingDetailsPage;

    public ReschedulingCommand(ReservationReschedulingDetailsPage reservationReschedulingDetailsPage, string comment, AccommodationReservationMoving accommodationReservationMoving, ReschedulingStatus status)
    {
        this.reservationReschedulingDetailsPage = reservationReschedulingDetailsPage;
        this.comment = comment;
        this.accommodationReservationMoving = accommodationReservationMoving;
        this.status = status;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (ReschedulingStatus.Accepted == status)
        {
            accommodationReservationMoving.Status = ReschedulingStatus.Accepted;
            AccommodationReservationMovingRepository.GetInstance().Update(accommodationReservationMoving);
            AccommodationReservationService.GetInstance().MoveReservation(accommodationReservationMoving);
        }
        else if (ReschedulingStatus.Rejected == status)
        {
            accommodationReservationMoving.Comment = comment;
            accommodationReservationMoving.Status = ReschedulingStatus.Rejected;

            AccommodationReservationMovingRepository.GetInstance().Update(accommodationReservationMoving);
        }

        reservationReschedulingDetailsPage.NavigateBack();
    }
}
