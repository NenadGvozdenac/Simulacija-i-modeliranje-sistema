using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class BeginButtonClickedEventArgs : EventArgs
    {
        public int TourId { get; }
        public DateTime StartTime { get; }

        public BeginButtonClickedEventArgs(int tourId, DateTime startTime)
        {
            TourId = tourId;
            StartTime = startTime;
        }
    }
}
