using BookingApp.Miscellaneous;
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Resources.Types;
using BookingApp.Services.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs.OwnerDTOs;

public class AccommodationReservationDTO
{
    private User _owner;

    public User Owner
    {
        get => _owner;
        set => _owner = value;
    }

    private AccommodationDTO _accommodationDTO;

    public AccommodationDTO AccommodationDTO
    {
        get => _accommodationDTO;
        set => _accommodationDTO = value;
    }

    private AccommodationReservation _reservation;

    public AccommodationReservation Reservation
    {
        get => _reservation;
        set => _reservation = value;
    }

    private string _numberOfReviews;
    public string NumberOfReviews
    {
        get => string.Format(_numberOfReviews == "1" ? "{0} review" : "{0} reviews", _numberOfReviews);
        set => _numberOfReviews = value;
    }

    private string _reservationDays;
    public string ReservationDays
    {
        get => string.Format(_reservationDays == "1" ? "{0} day" : "{0} days", _reservationDays);
        set => _reservationDays = value;
    }

    private string _guestRating;
    public string GuestRating
    {
        get => string.Format("{0}/5", _guestRating);
        set => _guestRating = value;
    }

    private string _lastCancellationDate;
    public string LastCancellationDate
    {
        get => _lastCancellationDate;
        set => _lastCancellationDate = value;
    }

    private string _guestsNumber;
    public string GuestsNumber
    {
        get => string.Format(_guestsNumber == "1" ? "{0} guest" : "{0} guests", _guestsNumber);
        set => _guestsNumber = value;
    }

    public AccommodationReservationDTO(AccommodationReservation reservation)
    {
        Owner = OwnerService.GetInstance().GetOwnerInfo(reservation.UserId).Item2;
        AccommodationDTO = new(AccommodationService.GetInstance().GetById(reservation.AccommodationId));
        Reservation = reservation;
        NumberOfReviews = "0";
        ReservationDays = (reservation.LastDateOfStaying - reservation.FirstDateOfStaying).Days.ToString();
        GuestsNumber = reservation.GuestsNumber.ToString();
        GuestRating = "0";
        LastCancellationDate = DateParser.ToString(reservation.FirstDateOfStaying.AddDays(-AccommodationService.GetInstance().GetById(reservation.AccommodationId).CancellationPeriodDays));
    }
}
