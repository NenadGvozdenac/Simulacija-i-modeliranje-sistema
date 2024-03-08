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
    public Location Location { get; set; }
    public AccommodationType Type { get; set; }
    public int MaxGuestNumber { get; set; }
    public int MinReservationDays { get; set; }
    public int CancellationPeriodDays { get; set; }
    public List<string> Images { get; set; }

    public void FromCSV(string[] values)
    {
        Id = Convert.ToInt32(values[0]);
        OwnerId = Convert.ToInt32(values[1]);
        Name = values[2];
        Location = new Location() { City = values[3], Country = values[4] };
        Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[5]);
        MaxGuestNumber = Convert.ToInt32(values[6]);
        MinReservationDays = Convert.ToInt32(values[7]);
        CancellationPeriodDays = Convert.ToInt32(values[8]);
    }

    public string[] ToCSV()
    {
        string[] csvValues = { Id.ToString(), OwnerId.ToString(), Name, Location.City, Location.Country, Type.ToString(), MaxGuestNumber.ToString(), MinReservationDays.ToString(), CancellationPeriodDays.ToString() };
        return csvValues;
    }
}