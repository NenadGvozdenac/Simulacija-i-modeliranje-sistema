using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTOs.OwnerDTOs;

public class GuestFeedbackDTO
{
    private User _user;
    public User User
    {
        get { return _user; }
        set { _user = value; }
    }

    private Accommodation _accommodation;
    public Accommodation Accommodation
    {
        get { return _accommodation; }
        set { _accommodation = value; }
    }

    private DateSpan _dateSpan;
    public DateSpan DateSpan
    {
        get { return _dateSpan; }
        set { _dateSpan = value; }
    }

    private AccommodationReservation _reservation;
    public AccommodationReservation Reservation
    {
        get { return _reservation; }
        set { _reservation = value; }
    }

    private string _cleanliness;
    public string Cleanliness
    {
        get { return string.Format("{0}.0 / 5.0", _cleanliness); }
        set { _cleanliness = value; }
    }

    private string _myCourtesy;
    public string MyCourtesy
    {
        get { return string.Format("{0}.0 / 5.0", _myCourtesy); }
        set { _myCourtesy = value; }
    }

    private string _comment;
    public string Comment
    {
        get { return _comment; }
        set { _comment = value; }
    }

    public GuestFeedbackDTO(AccommodationReview accommodationReview)
    {
        User = accommodationReview.Guest;
        Accommodation = accommodationReview.Accommodation;
        DateSpan = new DateSpan(accommodationReview.Reservation.FirstDateOfStaying, accommodationReview.Reservation.LastDateOfStaying);
        Reservation = accommodationReview.Reservation;
        Cleanliness = accommodationReview.Cleanliness.ToString();
        MyCourtesy = accommodationReview.OwnersCourtesy.ToString();
        Comment = accommodationReview.Feedback;
    }
}
