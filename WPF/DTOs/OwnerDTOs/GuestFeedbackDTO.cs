using BookingApp.Application.Localization;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookingApp.WPF.DTOs.OwnerDTOs;

public partial class GuestFeedbackDTO : ObservableObject
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

    public string AccommodationType
    {
        get { return TranslationSource.Instance[Accommodation.Type.ToString()]; }
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

    private List<ReviewImage> _images;
    public List<ReviewImage> Images
    {
        get { return _images; }
        set { _images = value; }
    }

    private int levelOfUrgency;
    public int LevelOfUrgency
    {
        get { return levelOfUrgency; }
        set { levelOfUrgency = value; }
    }

    private string requiresRenovation;
    public string RequiresRenovation
    {
        get { return requiresRenovation == "True" ? TranslationSource.Instance["DoesRequireRenovation"] : TranslationSource.Instance["DoesNotRequireRenovation"]; }
        set { requiresRenovation = value; }
    }

    private string renovationFeedback;
    public string RenovationFeedback
    {
        get { return renovationFeedback; }
        set { renovationFeedback = value; }
    }
    public string RenovationRequiredThumbnail { get; set; }
    public Brush RenovationRequiredColor { get; set; }

    [ObservableProperty]

    private string _imageURL;

    public GuestFeedbackDTO(AccommodationReview accommodationReview)
    {
        User = accommodationReview.Guest;
        Accommodation = accommodationReview.Accommodation;
        DateSpan = new DateSpan(accommodationReview.Reservation.FirstDateOfStaying, accommodationReview.Reservation.LastDateOfStaying);
        Reservation = accommodationReview.Reservation;
        Cleanliness = accommodationReview.Cleanliness.ToString();
        MyCourtesy = accommodationReview.OwnersCourtesy.ToString();
        Comment = accommodationReview.Feedback;
        Images = accommodationReview.ReviewImages;
        LevelOfUrgency = accommodationReview.LevelOfUrgency;
        RequiresRenovation = accommodationReview.RequiresRenovation.ToString();
        RenovationFeedback = accommodationReview.RenovationFeedback;
        RenovationRequiredThumbnail = accommodationReview.RequiresRenovation ? "RENOVATION REQUIRED!" : "RENOVATION NOT REQUIRED!";
        RenovationRequiredColor = accommodationReview.RequiresRenovation ? Brushes.Red : Brushes.Green;
        ImageURL = Images.Count > 0 ? Images.First().Path : "";
    }
}
