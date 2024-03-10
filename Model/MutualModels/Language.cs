using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model.MutualModels
{
    public class Language : ISerializable
    {

        public string Name;


        public void FromCSV(string[] values)
        {
            Name = values[0];
            
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Name };
            return csvValues;
        }


    }
}
