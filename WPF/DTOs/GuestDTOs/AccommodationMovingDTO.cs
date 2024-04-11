using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTOs.GuestDTOs
{
    public class AccommodationMovingDTO
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public DateSpan CurrentReservationTimespan { get; set; }
        public DateSpan WantedReservationTimespan { get; set; }
        public ReschedulingStatus Status { get; set; }
        public string Comment { get; set; }

        public AccommodationMovingDTO(Accommodation accom, AccommodationReservationMoving accommov)
        {
            Name = accom.Name;
            Location = accom.Location;
            CurrentReservationTimespan = accommov.CurrentReservationTimespan;
            WantedReservationTimespan = accommov.WantedReservationTimespan;
            Status = accommov.Status;
            if (accommov.Comment.Equals(""))
                Comment = "No comment";
            else
                Comment = accommov.Comment;
        }

    }
}
