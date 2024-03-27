using BookingApp.Miscellaneous;
using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs.OwnerDTOs;

public class GuestRatingDTO
{
    private bool _isChecked;
    public bool IsChecked
    {
        get => _isChecked;
        set => _isChecked = value;
    }

    private AccommodationDTO _accommodation;
    public AccommodationDTO Accommodation
    {
        get => _accommodation;
        set => _accommodation = value;
    }

    private AccommodationReservationDTO _reservation;
    public AccommodationReservationDTO Reservation
    {
        get => _reservation;
        set => _reservation = value;
    }

    private User _guest;
    public User Guest
    {
        get => _guest;
        set => _guest = value;
    }

    private string _cleanliness;
    public string Cleanliness
    {
        get => string.Format("{0}/5", _cleanliness);
        set => _cleanliness = value;
    }

    private string _respectfulness;
    public string Respectfulness
    {
        get => string.Format("{0}/5", _respectfulness);
        set => _respectfulness = value;
    }

    private string _comment;
    public string Comment
    {
        get => _comment;
        set => _comment = value;
    }

    private DateSpan _dateSpan;
    public DateSpan DateSpan
    {
        get => _dateSpan;
        set => _dateSpan = value;
    }

    public GuestRatingDTO(GuestRating guestRating)
    {
        IsChecked = guestRating.IsChecked;
        Accommodation = new AccommodationDTO(AccommodationRepository.GetInstance().GetById(guestRating.AccommodationId));
        Reservation = new AccommodationReservationDTO(guestRating.Reservation);
        Guest = UserRepository.GetInstance().GetById(guestRating.GuestId);
        Cleanliness = guestRating.Cleanliness.ToString();
        Respectfulness = guestRating.Respectfulness.ToString();
        Comment = guestRating.Comment;
        DateSpan = new DateSpan(guestRating.Reservation.FirstDateOfStaying, guestRating.Reservation.LastDateOfStaying);
    }
}
