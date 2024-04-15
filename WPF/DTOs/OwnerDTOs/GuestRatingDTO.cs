using BookingApp.Application.UseCases;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingApp.WPF.DTOs.OwnerDTOs;

public partial class GuestRatingDTO : ObservableObject
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private bool _isChecked;

    [ObservableProperty]
    private AccommodationDTO _accommodation;

    [ObservableProperty]
    private AccommodationReservationDTO _reservation;

    [ObservableProperty]
    private User _guest;

    [ObservableProperty]
    private DateSpan _dateSpan;

    private string _cleanliness;
    public string Cleanliness
    {
        get => string.Format("{0}/5.00", _cleanliness);
        set => _cleanliness = value;
    }

    private string _respectfulness;
    public string Respectfulness
    {
        get => string.Format("{0}/5.00", _respectfulness);
        set => _respectfulness = value;
    }

    private string _comment;
    public string Comment
    {
        get => _comment;
        set => _comment = value;
    }

    public GuestRatingDTO(GuestRating guestRating)
    {
        Id = guestRating.Id;
        IsChecked = guestRating.IsChecked;
        Accommodation = new AccommodationDTO(AccommodationService.GetInstance().GetById(guestRating.AccommodationId));
        Reservation = new AccommodationReservationDTO(guestRating.Reservation);
        Guest = OwnerService.GetInstance().GetById(guestRating.GuestId).Item2;
        Cleanliness = guestRating.Cleanliness.ToString();
        Respectfulness = guestRating.Respectfulness.ToString();
        Comment = guestRating.Comment;
        DateSpan = new DateSpan(guestRating.Reservation.FirstDateOfStaying, guestRating.Reservation.LastDateOfStaying);
    }
}
