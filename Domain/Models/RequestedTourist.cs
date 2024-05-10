using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;

namespace BookingApp.Domain.Models
{
    class RequestedTourist : ISerializable
    {
        public int Id { get; set; }
        public int RequestId {  get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            RequestId = Convert.ToInt32(values[1]);
            Name = values[2];
            Surname = values[3];
            Age = Convert.ToInt32(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), RequestId.ToString(), Name, Surname, Age.ToString() };
            return csvValues;
        }
    }
}
