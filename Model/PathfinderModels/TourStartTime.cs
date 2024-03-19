using BookingApp.Model.MutualModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Model.PathfinderModels;


public class TourStartTime : ISerializable
{
    public int Id { get; set; }

    public int TourId { get; set; }

    public DateTime Time { get; set; }

    public int Guests { get; set; }

    public String Status { get; set; }

    public int CurrentCheckpoint { get; set; }

    public void FromCSV(string[] values)
    {
        Id = Convert.ToInt32(values[0]);
        TourId = Convert.ToInt32(values[1]);
        Time = Convert.ToDateTime(values[2]);
        Guests = Convert.ToInt32(values[3]);
        Status = values[4];
        CurrentCheckpoint = Convert.ToInt32(values[5]);
    }

    public string[] ToCSV()
    {
        string[] csvValues = { Id.ToString(), TourId.ToString(), Time.ToString(), Guests.ToString(), Status, CurrentCheckpoint.ToString()};
        return csvValues;
    }



}
