using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using BookingApp.View.GuestViews;

namespace BookingApp.Model.MutualModels
{
    public class TouristReservation : ISerializable
    {
        public int Id { get; set; }
        public int Id_Tour {  get; set; }
        public int Id_Tourist {  get; set; }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Id_Tour = Convert.ToInt32(values[1]);
            Id_Tourist = Convert.ToInt32(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Id_Tour.ToString(), Id_Tourist.ToString() };
            return csvValues;
        }
    }
}
