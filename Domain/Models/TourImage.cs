﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;

namespace BookingApp.Domain.Models;

public class TourImage : ISerializable
{
    public TourImage() { }  
    public TourImage(string path)
    {
        Path = path;
    }

    public int Id { get; set; }
    public int TourId { get; set; }
    public string Path { get; set; }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        TourId = int.Parse(values[1]);
        Path = values[2];
    }

    public string[] ToCSV()
    {
        return new string[] { Id.ToString(), TourId.ToString(), Path };
    }
}
