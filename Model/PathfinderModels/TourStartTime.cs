using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.PathfinderModels
{
    internal class TourStartTime
    {
        public int Id { get; set; }

        public int TourId { get; set; }

        public DateTime Time { get; set; }

        public int Guests { get; set; }


    }
}
