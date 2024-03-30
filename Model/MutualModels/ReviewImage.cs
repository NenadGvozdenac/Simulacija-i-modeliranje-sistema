using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.MutualModels
{
    public class ReviewImage : ISerializable
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int ReservationId { get; set; }
        public int AccommodationId { get; set; }
        public string Path { get; set; }

        public ReviewImage() { }

        public ReviewImage(int reservationId, int accommodationId, string path)
        {
            ReservationId = reservationId;
            AccommodationId = accommodationId;
            Path = path;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReviewId = int.Parse(values[1]);
            ReservationId = int.Parse(values[2]);
            AccommodationId = int.Parse(values[3]);
            Path = values[4];
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), ReviewId.ToString(), ReservationId.ToString(), AccommodationId.ToString(), Path };
        }
    }
}
