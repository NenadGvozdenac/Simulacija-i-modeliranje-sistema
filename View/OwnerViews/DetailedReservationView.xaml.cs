using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.OwnerViews;

public partial class DetailedReservationView : Page
{
    private AccommodationReservation reservation;
    public AccommodationReservation Reservation
    {
        get { return reservation; }
        set { reservation = value; }
    }

    private Accommodation accommodation;
    public Accommodation Accommodation
    {
        get { return accommodation; }
        set { accommodation = value; }
    }

    private ReservationType reservationType;
    public ReservationType ReservationType
    {
        get { return reservationType; }
        set { reservationType = value; }
    }

    private DateTime lastCancellationDay;
    public DateTime LastCancellationDay
    {
        get { return lastCancellationDay; }
        set { lastCancellationDay = value; }
    }

    private int numberOfReviews;
    public int NumberOfReviews
    {
        get { return numberOfReviews; }
        set { numberOfReviews = value; }
    }

    private int reservationDays;
    public int ReservationDays
    {
        get { return reservationDays; }
        set { reservationDays = value; }
    }

    private double guestRating;
    public double GuestRating
    {
        get { return guestRating; }
        set { guestRating = value; }
    }

    private string guestUsername;
    public string GuestUsername
    {
        get { return guestUsername; }
        set { guestUsername = value; }
    }

    public DetailedReservationView(AccommodationReservation accommodationReservation, Accommodation accommodation)
    {
        DataContext = this;
        this.reservation = accommodationReservation;
        this.accommodation = accommodation;
        this.reservationType = accommodationReservation.ReservationType;
        this.numberOfReviews = 0;
        this.guestRating = 0;
        this.guestUsername = UserRepository.GetInstance().GetById(accommodationReservation.UserId).Username;
        this.reservationDays = (accommodationReservation.LastDateOfStaying - accommodationReservation.FirstDateOfStaying).Days;
        this.lastCancellationDay = accommodationReservation.FirstDateOfStaying.AddDays(-accommodation.CancellationPeriodDays);
        
        InitializeComponent();
    }

    private void BackArrowClick(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
