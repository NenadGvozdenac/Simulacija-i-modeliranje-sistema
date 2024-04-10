using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Resources.Types;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedReservationMovingViewModel
{
    public AccommodationReservationMovingDTO AccommodationReservationMovingDTO { get; set; }

    public DetailedReservationMovingViewModel(AccommodationReservationMovingDTO accommodationReservationMovingDTO)
    {
        AccommodationReservationMovingDTO = accommodationReservationMovingDTO;
    }

    public void Accept_Click()
    {
        var accommodationMoving = AccommodationReservationMovingDTO.AccommodationReservationMoving;
        accommodationMoving.Status = ReschedulingStatus.Accepted;

        AccommodationReservationMovingRepository.GetInstance().Update(accommodationMoving);
        AccommodationReservationService.GetInstance().MoveReservation(accommodationMoving);
    }

    public void Reject_Click(string comment)
    {
        var accommodationMoving = AccommodationReservationMovingDTO.AccommodationReservationMoving;
        accommodationMoving.Comment = comment;
        accommodationMoving.Status = ReschedulingStatus.Rejected;

        AccommodationReservationMovingRepository.GetInstance().Update(accommodationMoving);

    }
}
