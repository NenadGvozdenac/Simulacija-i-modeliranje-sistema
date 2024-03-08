using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.MutualModels
{
    public class AccommodationImage : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public string Path { get; set; }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            Path = values[2];
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), AccommodationId.ToString(), Path };
        }
    }
}
