using BookingApp.Domain.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class AccommodationReview : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User Guest { get; set; }
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public int ReservationId { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public int Cleanliness { get; set; }
        public int OwnersCourtesy { get; set; }
        public string Feedback { get; set; }
        public List<ReviewImage> ReviewImages { get; set; }
        public int LevelOfUrgency { get; set; }
        public string RenovationFeedback { get; set; }
        public bool RequiresRenovation { get; set; }

        public AccommodationReview()
        {

        }
        public AccommodationReview(int userId, int accommodationId, int reservationId, int cleanliness, int ownerCourtesy, string feedback, bool requiresRenovation)
        {
            UserId = userId;
            AccommodationId = accommodationId;
            ReservationId = reservationId;
            Cleanliness = cleanliness;
            OwnersCourtesy = ownerCourtesy;
            Feedback = feedback;
            RequiresRenovation = requiresRenovation;
        }

        public AccommodationReview(int userId, int accommodationId, int reservationId, int cleanliness, int ownerCourtesy, string feedback, int levelofurgency, string renovationfeedback, bool requiresRenovation)
        {
            UserId = userId;
            AccommodationId = accommodationId;
            ReservationId = reservationId;
            Cleanliness = cleanliness;
            OwnersCourtesy = ownerCourtesy;
            Feedback = feedback;
            LevelOfUrgency = levelofurgency;
            RenovationFeedback = renovationfeedback;
            RequiresRenovation = requiresRenovation;
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
            LevelOfUrgency = int.Parse(values[7]);
            RenovationFeedback = values[8];
            RequiresRenovation = bool.Parse(values[9]);
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), UserId.ToString(), AccommodationId.ToString(), ReservationId.ToString(), Cleanliness.ToString(), OwnersCourtesy.ToString(), Feedback, LevelOfUrgency.ToString(), RenovationFeedback, RequiresRenovation.ToString() };
        }
    }
}
