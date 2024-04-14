using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;

namespace BookingApp.Domain.Models;

public class Tour : ISerializable
{

    public int Id { get; set; }

    public int OwnerId { get; set; }
    public string Name { get; set; }

    public int LocationId { get; set; }

    public Location Location { get; set; }
    public string Description { get; set; }

    public int LanguageId { get; set; }

    public Language Language { get; set; }

    public int Capacity { get; set; }
    public List<Checkpoint> Checkpoints { get; set; }
    public List<TourStartTime> Dates { get; set; }
    public int Duration { get; set; }
    public List<TourImage> Images { get; set; }

    public DateTime CurrentDate { get; set; }

    public void FromCSV(string[] values)
    {
        Id = Convert.ToInt32(values[0]);
        Name = values[1];
        LocationId = Convert.ToInt32(values[2]);
        Description = values[3];
        LanguageId = Convert.ToInt32(values[4]);
        Capacity = Convert.ToInt32(values[5]);
        Duration = Convert.ToInt32(values[6]);
        OwnerId = Convert.ToInt32(values[7]);

    }

    public string[] ToCSV()
    {
        string[] csvValues = { Id.ToString(), Name, LocationId.ToString(), Description, LanguageId.ToString(), Capacity.ToString(), Duration.ToString(), OwnerId.ToString() };
        return csvValues;
    }
}
