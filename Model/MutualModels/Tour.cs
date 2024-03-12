using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model.PathfinderModels;
using BookingApp.Serializer;

namespace BookingApp.Model.MutualModels;
    
public class Tour : ISerializable
{

    public int Id { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; }
    public string Description { get; set; }
    public string Language { get; set; }
    public int Capacity { get; set; }
    public List<Checkpoint> Checkpoints { get; set; }
    public List<TourStartTime> Dates { get; set; }
    public int Duration { get; set; }
    public List<TourImage> Images { get; set; }

    public void FromCSV(string[] values)
    {
        Id = Convert.ToInt32(values[0]);
        Name = values[1];
        Location = new Location() { City = values[2], Country = values[3] };
        Description = values[4];
        Language = values[5];
        Capacity = Convert.ToInt32(values[6]);
        Duration = Convert.ToInt32(values[7]);
    }

    public string[] ToCSV()
    {
        string[] csvValues = { Id.ToString(), Name, Location.City, Location.Country, Description, Language, Capacity.ToString(), Duration.ToString() };
        return csvValues;
    }
}
