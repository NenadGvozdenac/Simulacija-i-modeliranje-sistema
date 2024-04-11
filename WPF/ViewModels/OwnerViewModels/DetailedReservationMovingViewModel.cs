using BookingApp.Application.Commands;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Resources.Types;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedReservationMovingViewModel
{
    private ReservationReschedulingDetailsPage reservationReschedulingDetailsPage;

    public AccommodationReservationMovingDTO AccommodationReservationMovingDTO { get; set; }

    public ICommand AcceptClick => new ReschedulingCommand(reservationReschedulingDetailsPage, 
        null, AccommodationReservationMovingDTO.AccommodationReservationMoving, ReschedulingStatus.Accepted);
    public ICommand RejectClick => new ReschedulingCommand(reservationReschedulingDetailsPage, 
        AccommodationReservationMovingDTO.Comment, AccommodationReservationMovingDTO.AccommodationReservationMoving, ReschedulingStatus.Rejected);

    public DetailedReservationMovingViewModel(ReservationReschedulingDetailsPage reservationReschedulingDetailsPage, AccommodationReservationMovingDTO accommodationReservationMovingDTO)
    {
        this.reservationReschedulingDetailsPage = reservationReschedulingDetailsPage;
        AccommodationReservationMovingDTO = accommodationReservationMovingDTO;
    }
}
