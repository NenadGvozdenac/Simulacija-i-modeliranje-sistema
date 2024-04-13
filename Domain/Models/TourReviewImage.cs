using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;
using BookingApp.View.GuestViews;


namespace BookingApp.Model.MutualModels
{
    public class TourReviewImage : ISerializable
    {
        public int Id {  get; set; }
        public int ReviewId {  get; set; }
        public int ReservationId {  get; set; }
        public int TourId { get; set; }
        public string Path {  get; set; }

        public TourReviewImage() { }
        public TourReviewImage(int tourId, string path)
        {
            TourId = tourId;
            Path = path;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReviewId = int.Parse(values[1]);
            TourId = int.Parse(values[2]);
            Path = values[3];
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), ReviewId.ToString(), TourId.ToString(), Path };
        }

    }
}
