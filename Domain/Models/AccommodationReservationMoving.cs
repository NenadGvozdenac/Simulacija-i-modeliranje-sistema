using BookingApp.Domain.Miscellaneous;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models;

public class AccommodationReservationMoving : ISerializable
{
    public int Id { get; set; }
    public int AccommodationId { get; set; }
    public Accommodation Accommodation { get; set; }
    public int ReservationId { get; set; }
    public AccommodationReservation Reservation { get; set; }
    public int GuestId { get; set; }
    public User Guest { get; set; }
    public DateSpan CurrentReservationTimespan { get; set; }
    public DateSpan WantedReservationTimespan { get; set; }
    public ReschedulingStatus Status { get; set; }
    public string Comment { get; set; }

    public AccommodationReservationMoving() { }

    public AccommodationReservationMoving(int accommodationId, int reservationId, int guestId, DateSpan currentReservationTimespan, DateSpan wantedReservationTimespan, ReschedulingStatus status, string comment)
    {
        AccommodationId = accommodationId;
        ReservationId = reservationId;
        GuestId = guestId;
        CurrentReservationTimespan = currentReservationTimespan;
        WantedReservationTimespan = wantedReservationTimespan;
        Status = status;
        Comment = comment;
    }

    public AccommodationReservationMoving(int accommodationId, int reservationId, int guestId, DateTime currentFirst, DateTime currentLast, DateTime wantedFirst, DateTime wantedLast)
    {
        AccommodationId = accommodationId;
        ReservationId = reservationId;
        GuestId = guestId;
        CurrentReservationTimespan = new(currentFirst, currentLast);
        WantedReservationTimespan = new(wantedFirst, wantedLast);
        Status = ReschedulingStatus.Pending;
        Comment = "";
    }

    public void FromCSV(string[] values)
    {
        Id = Convert.ToInt32(values[0]);
        AccommodationId = Convert.ToInt32(values[1]);
        ReservationId = Convert.ToInt32(values[2]);
        GuestId = Convert.ToInt32(values[3]);
        CurrentReservationTimespan = DateSpan.Parse(values[4]);
        WantedReservationTimespan = DateSpan.Parse(values[5]);
        Status = (ReschedulingStatus)Enum.Parse(typeof(ReschedulingStatus), values[6]);
        Comment = values[7];
    }

    public string[] ToCSV()
    {
        return new string[] { Id.ToString(), AccommodationId.ToString(), ReservationId.ToString(), GuestId.ToString(), CurrentReservationTimespan.ToString(), WantedReservationTimespan.ToString(), Status.ToString(), Comment };
    }
}
