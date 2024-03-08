using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model.MutualModels;
using BookingApp.Serializer;

namespace BookingApp.Model.PathfinderModels;
   
    public class Checkpoint : ISerializable
    {
        public int Id { get; set; }

        public int TourId {get;  set;}

        public string Name { get; set; }

       
        
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);  
            TourId = Convert.ToInt32(values[1]);
            Name = values[2];
        }


        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),TourId.ToString(),Name };
            return csvValues;
        }

        


}

