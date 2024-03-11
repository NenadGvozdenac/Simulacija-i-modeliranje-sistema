using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.MutualModels
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccommodationId { get; set; }
        public int GuestsNumber { get; set; }
        public DateTime FirstDateOfStaying { get; set; }
        public DateTime LastDateOfStaying { get; set; }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            GuestsNumber = Convert.ToInt32(values[3]);
            FirstDateOfStaying = Convert.ToDateTime(values[4]);
            LastDateOfStaying = Convert.ToDateTime(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), UserId.ToString(), AccommodationId.ToString(), GuestsNumber.ToString(),FirstDateOfStaying.ToString(), LastDateOfStaying.ToString()};
            return csvValues;
        }
    }
}
