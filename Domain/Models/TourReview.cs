using BookingApp.Domain.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BookingApp.Model.MutualModels
{

    public class TourReview : ISerializable
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public int TourId {  get; set; }
        public int ReservationId {  get; set; }

        public string Checkpoint { get; set; }
        public int GuideKnowledge {  get; set; }
        public int GuideLanguage {  get; set; }
        public int TourInterestingness {  get; set; }
        public string Feedback {  get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public ObservableCollection<TourReviewImage> ReviewImages { get; set; }

        public TourReview() { }
        public TourReview(int userId, int tourId, int guideKnowledge, int guideLanguage, int interestingness, string feedback)
        {
            UserId = userId;
            TourId = tourId;
            //ReservationId = reservationId;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourInterestingness = interestingness;
            Feedback = feedback;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            TourId = int.Parse(values[2]);
            //ReservationId = int.Parse(values[3]);
            GuideKnowledge = int.Parse(values[3]);
            GuideLanguage = int.Parse(values[4]);
            TourInterestingness = int.Parse(values[5]);
            Feedback = values[6];
            Status = values[7];
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), UserId.ToString(), TourId.ToString(),/* ReservationId.ToString(),*/ GuideKnowledge.ToString(), GuideLanguage.ToString(), TourInterestingness.ToString(), Feedback, Status.ToString()};
        }
    }
}
