using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.OwnerModels;

public class GuestRating : ISerializable
{
    public int Id { get; set; }
    public bool IsChecked { get; set; }
    public int AccommodationId { get; set; }
    public int ReservationId { get; set; }
    public int GuestId { get; set; }
    public AccommodationReservation Reservation { get; set; }
    public int Cleanliness { get; set; }
    public int Respectfulness { get; set; }
    public string Comment { get; set; }

    public GuestRating()
    {
    }

    public GuestRating(int id, bool isChecked, int accommodationId, int reservationId,  int guestId, int cleanliness, int respectfulness, string comment)
    {
        Id = id;
        IsChecked = isChecked;
        AccommodationId = accommodationId;
        ReservationId = reservationId;
        GuestId = guestId;
        Cleanliness = cleanliness;
        Respectfulness = respectfulness;
        Comment = comment;
    }

    public string[] ToCSV()
    {
        return new string[] { Id.ToString(), IsChecked.ToString(), AccommodationId.ToString(), ReservationId.ToString(), GuestId.ToString(), Cleanliness.ToString(), Respectfulness.ToString(), Comment };
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        IsChecked = bool.Parse(values[1]);
        AccommodationId = int.Parse(values[2]);
        ReservationId = int.Parse(values[3]);
        GuestId = int.Parse(values[4]);
        Cleanliness = int.Parse(values[5]);
        Respectfulness = int.Parse(values[6]);
        Comment = values[7];
    }
}
