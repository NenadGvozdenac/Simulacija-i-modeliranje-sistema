using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTOs.GuestDTOs
{
    public class PastReservationsDTO
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public int AccommodationId { get; set; }
        public int ReservationId { get; set; }
        public double AverageReviewScore { get; set; }
        public int DaysSinceLastDateOfStaying { get; set; }
        public double Price { get; set; }
        public List<AccommodationImage> Images { get; set; }

        public PastReservationsDTO(Accommodation accomm, AccommodationReservation accommreservation)
        {
            Name = accomm.Name;
            Location = accomm.Location;
            AccommodationId = accomm.Id;
            AverageReviewScore = accomm.AverageReviewScore;
            Price = accomm.Price;

            TimeSpan difference = DateTime.Now - accommreservation.LastDateOfStaying;
            DaysSinceLastDateOfStaying = difference.Days;

            ReservationId = accommreservation.Id;
        }
    }
}
