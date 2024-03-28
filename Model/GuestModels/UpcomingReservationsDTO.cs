using BookingApp.Model.MutualModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.GuestModels
{
    public class UpcomingReservationsDTO
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public DateTime FirstDateOfStaying { get; set; }
        public DateTime LastDateOfStaying { get; set; }       
        public AvailableDates AvailableDates { get; set; }
        public string AvailableDatesString { get; set; }
        public int RemainingDaysToCancel { get; set; }
        public int AccommodationId { get; set; }
        public int ReservationId { get; set; }
        public List<AccommodationImage> Images { get; set; }

        public UpcomingReservationsDTO(Accommodation accomm, AccommodationReservation accommreservation) 
        {
            Name = accomm.Name;
            Location = accomm.Location;
            FirstDateOfStaying = accommreservation.FirstDateOfStaying;
            LastDateOfStaying = accommreservation.LastDateOfStaying;
            AvailableDates = new AvailableDates(FirstDateOfStaying,LastDateOfStaying);
            AvailableDatesString = AvailableDates.ToString();
            
            DateTime temp = FirstDateOfStaying.AddDays(-accomm.CancellationPeriodDays);

            TimeSpan difference = temp - DateTime.Now;
            RemainingDaysToCancel = difference.Days;
            AccommodationId = accomm.Id;
            ReservationId = accommreservation.Id;           
        }
    }
}
