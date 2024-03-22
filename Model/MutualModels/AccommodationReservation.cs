using BookingApp.Miscellaneous;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.MutualModels;

public class AccommodationReservation : ISerializable
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AccommodationId { get; set; }
    public int GuestsNumber { get; set; }
    public DateTime FirstDateOfStaying { get; set; }
    public DateTime LastDateOfStaying { get; set; }

    public AccommodationReservation() { }
    public AccommodationReservation(int userid, int accommodationid, int guestnumber, DateTime firstday, DateTime lastday)
    {
        UserId = userid;
        AccommodationId = accommodationid;
        GuestsNumber = guestnumber;
        FirstDateOfStaying = firstday;
        LastDateOfStaying = lastday;
    }
    public void FromCSV(string[] values)
    {
        Id = Convert.ToInt32(values[0]);
        UserId = Convert.ToInt32(values[1]);
        AccommodationId = Convert.ToInt32(values[2]);
        GuestsNumber = Convert.ToInt32(values[3]);
        FirstDateOfStaying = DateParser.Parse(values[4]);
        LastDateOfStaying = DateParser.Parse(values[5]);
    }

    public string[] ToCSV()
    {
        string[] csvValues = {Id.ToString(), UserId.ToString(), AccommodationId.ToString(), GuestsNumber.ToString(),DateParser.ToString(FirstDateOfStaying), DateParser.ToString(LastDateOfStaying)};
        return csvValues;
    }
}
