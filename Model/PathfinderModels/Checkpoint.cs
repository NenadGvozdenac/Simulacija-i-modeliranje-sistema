using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model.MutualModels;
using BookingApp.Serializer;

namespace BookingApp.Model.PathfinderModels
{
    internal class Checkpoint : ISerializable
    {
        public int Id { get; set; }

        public string Name { get; set; }

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


    }
}
