using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.MutualModels;

public class Accommodation : ISerializable
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }
    public AccommodationType Type { get; set; }
    public int MaxGuestNumber { get; set; }
    public int MinReservationDays { get; set; }
    public int CancellationPeriodDays { get; set; }
    public double AverageReviewScore { get; set; }
    public double Price { get; set; }
    public List<AccommodationImage> Images { get; set; }

    public void FromCSV(string[] values)
    {
        Id = Convert.ToInt32(values[0]);
        OwnerId = Convert.ToInt32(values[1]);
        Name = values[2];
        LocationId = Convert.ToInt32(values[3]);
        Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[4]);
        MaxGuestNumber = Convert.ToInt32(values[5]);
        MinReservationDays = Convert.ToInt32(values[6]);
        CancellationPeriodDays = Convert.ToInt32(values[7]);
        AverageReviewScore = Convert.ToDouble(values[8]);
        Price = Convert.ToDouble(values[9]);
    }

    public string[] ToCSV()
    {
        string[] csvValues = { Id.ToString(), OwnerId.ToString(), Name, LocationId.ToString(), Type.ToString(), MaxGuestNumber.ToString(), MinReservationDays.ToString(), CancellationPeriodDays.ToString(), AverageReviewScore.ToString(), Price.ToString()};
        return csvValues;
    }
}