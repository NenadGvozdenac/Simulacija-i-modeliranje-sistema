using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;

namespace BookingApp.Domain.Models
{
    public class Language : ISerializable
    {
        public int Id { get; set; }

        public string Name;


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name };
            return csvValues;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

    }
}
