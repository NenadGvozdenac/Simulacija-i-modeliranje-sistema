using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;

namespace BookingApp.Domain.Models
{
    public class ComplexTourRequest : ISerializable
    {
        public int Id {  get; set; }
        public int UserId {  get; set; }
        public List<int> TourRequestIds { get; set; }
        public string Status {  get; set; }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Status = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), Status };
            return csvValues;
        }
    }
}
