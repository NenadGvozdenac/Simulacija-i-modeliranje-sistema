using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using BookingApp.Domain.Miscellaneous;

namespace BookingApp.Domain.Models
{
    public class TouristReservation : ISerializable
    {
        public int Id { get; set; }
        public int Id_TourTime { get; set; }
        public int Id_Tourist { get; set; }

        public int CheckpointId { get; set; }  //NEW

        public int UserId {  get; set; } //NEW
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Id_TourTime = Convert.ToInt32(values[1]);
            Id_Tourist = Convert.ToInt32(values[2]);
            CheckpointId = Convert.ToInt32(values[3]);
            UserId = Convert.ToInt32(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Id_TourTime.ToString(), Id_Tourist.ToString(), CheckpointId.ToString(), UserId.ToString()};
            return csvValues;
        }
    }
}
