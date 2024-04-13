using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Miscellaneous
{
    public class AvailableDates
    {
        public DateTime firstAvailableDate;
        public DateTime lastAvailableDate;

        public AvailableDates() { }

        public AvailableDates(DateTime firstAvailableDate, DateTime lastAvailableDate)
        {
            this.firstAvailableDate = firstAvailableDate;
            this.lastAvailableDate = lastAvailableDate;
        }

        public override string ToString()
        {
            return $"{DateParser.ToString(firstAvailableDate)} - {DateParser.ToString(lastAvailableDate)}";
        }

    }
}
