using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.MutualModels
{
    public class AccommodationReview : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccommodationId { get; set; }
        public int ReservationId { get; set; }
        public int Cleanliness { get; set; }
        public int OwnersCourtesy { get; set; }
        public string Feedback { get; set; }
        //public List<ReviewImages> { get; set;} TODO

        public AccommodationReview()
        {

        }
        public AccommodationReview(int userId, int accommodationId, int reservationId, int cleanliness, int ownerCourtesy, string feedback)
        {
            UserId = userId;
            AccommodationId = accommodationId;
            ReservationId = reservationId;
            Cleanliness = cleanliness;
            OwnersCourtesy = ownerCourtesy;
            Feedback = feedback;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            AccommodationId = int.Parse(values[2]);
            ReservationId = int.Parse(values[3]);
            Cleanliness = int.Parse(values[4]);
            OwnersCourtesy = int.Parse(values[5]);
            Feedback = values[6];
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), UserId.ToString() ,AccommodationId.ToString(), ReservationId.ToString(), Cleanliness.ToString(), OwnersCourtesy.ToString(), Feedback};
        }
    }
}
