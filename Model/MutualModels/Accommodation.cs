using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.MutualModels;

class Accomodation : ISerializable
{
    public int Id { get; set; }
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
        Name = values[1];
        Location = new Location() { City = values[2], Country = values[3] };
        Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[4]);
        MaxGuestNumber = Convert.ToInt32(values[5]);
        MinReservationDays = Convert.ToInt32(values[6]);
        CancellationPeriodDays = Convert.ToInt32(values[7]);
    }

    public string[] ToCSV()
    {
        string[] csvValues = { Id.ToString(), Name, Location.City, Location.Country, Type.ToString(), MaxGuestNumber.ToString(), MinReservationDays.ToString(), CancellationPeriodDays.ToString() };
        return csvValues;
    }
}