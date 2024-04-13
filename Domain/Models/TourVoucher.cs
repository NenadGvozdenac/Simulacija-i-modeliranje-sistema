using BookingApp.Model.PathfinderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;

namespace BookingApp.Domain.Models
{
    public class TourVoucher : ISerializable
    {
        public int Id { get; set; }

        public int TouristId { get; set; }

        public DateTime ExpirationDate { get; set; }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            ExpirationDate = Convert.ToDateTime(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TouristId.ToString(), ExpirationDate.ToString() };
            return csvValues;
        }

    }
}
