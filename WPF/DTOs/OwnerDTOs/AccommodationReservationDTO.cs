using BookingApp.Application.UseCases;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Resources.Types;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingApp.WPF.DTOs.OwnerDTOs;

public partial class AccommodationReservationDTO : ObservableObject
{
    [ObservableProperty]
    private User _owner;

    [ObservableProperty]
    private AccommodationDTO _accommodationDTO;

    [ObservableProperty]
    private AccommodationReservation _reservation;

    [ObservableProperty]
    private User _guest;

    [ObservableProperty]
    private ReservationType _reservationType;

    [ObservableProperty]
    private DateSpan _dateSpan;

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
        get => string.Format("{0}/5.00", _guestRating);
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
        Guest = OwnerService.GetInstance().GetById(reservation.UserId).Item2;
        AccommodationDTO = new(AccommodationService.GetInstance().GetById(reservation.AccommodationId));
        Owner = AccommodationDTO.Owner;
        Reservation = reservation;
        NumberOfReviews = AccommodationReviewService.GetInstance().GetByAccommodationId(reservation.AccommodationId).Count.ToString();
        ReservationDays = (reservation.LastDateOfStaying - reservation.FirstDateOfStaying).Days.ToString();
        GuestsNumber = reservation.GuestsNumber.ToString();
        GuestRating = "0";
        DateSpan = new(reservation.FirstDateOfStaying, reservation.LastDateOfStaying);
        ReservationType = reservation.ReservationType;
        LastCancellationDate = DateParser.ToString(reservation.FirstDateOfStaying.AddDays(-AccommodationService.GetInstance().GetById(reservation.AccommodationId).CancellationPeriodDays));
    }
}
