using BookingApp.Domain.Miscellaneous;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models;

public class AccommodationRenovation : ISerializable
{
    public int Id { get; set; }
    public int AccommodationId { get; set; }
    public Accommodation Accommodation { get; set; }
    public int RenovationDays { get; set; }
    public DateSpan DateSpan { get; set; }
    public RenovationStatus Status { get; set; }
    public AccommodationRenovation()
    {
        
    }

    public AccommodationRenovation(int id, int accommodationId, int renovationDays, DateSpan dateSpan, RenovationStatus renovationStatus)
    {
        Id = id;
        AccommodationId = accommodationId;
        RenovationDays = renovationDays;
        DateSpan = dateSpan;
        Status = renovationStatus;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        AccommodationId = int.Parse(values[1]);
        RenovationDays = int.Parse(values[2]);
        DateSpan = DateSpan.Parse(values[3]);
        Status = (RenovationStatus)Enum.Parse(typeof(RenovationStatus), values[4]);
    }

    public string[] ToCSV()
    {
        return new string[] { Id.ToString(), AccommodationId.ToString(), RenovationDays.ToString(), DateSpan.ToString(), Status.ToString() };
    }
}
