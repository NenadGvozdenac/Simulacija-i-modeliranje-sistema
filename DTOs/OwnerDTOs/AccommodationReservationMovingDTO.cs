using BookingApp.Model.MutualModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs.OwnerDTOs;

public class AccommodationReservationMovingDTO
{
    public AccommodationReservationMoving AccommodationReservationMoving { get; set; }
    public int DaysOfReservation { get; set; }
    public string Comment { get; set; }
    public string StatusOfWantedTimespan { get; set; }
    public DateTime DayBeforeCancellationIsFinal { get; set; }

    public AccommodationReservationMovingDTO(AccommodationReservationMoving accommodationReservationMoving)
    {
        this.AccommodationReservationMoving = accommodationReservationMoving;
        this.DaysOfReservation = (accommodationReservationMoving.CurrentReservationTimespan.End - accommodationReservationMoving.CurrentReservationTimespan.Start).Days;
        this.DayBeforeCancellationIsFinal = accommodationReservationMoving.CurrentReservationTimespan.Start.AddDays(-accommodationReservationMoving.Accommodation.CancellationPeriodDays);
    }
}
