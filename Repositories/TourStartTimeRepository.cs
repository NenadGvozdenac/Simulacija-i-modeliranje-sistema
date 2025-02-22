﻿using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.View.PathfinderViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class TourStartTimeRepository : ITourStartTimeRepository
{
    private const string FilePath = "../../../Resources/Data/tourDates.csv";

    private readonly Serializer<TourStartTime> _serializer;

    private List<TourStartTime> _times;

    public TourStartTimeRepository()
    {
        _serializer = new Serializer<TourStartTime>();
        _times = _serializer.FromCSV(FilePath);
    }

    public void Add(TourStartTime time)
    {
        _times.Add(time);
        _serializer.ToCSV(FilePath, _times);
    }

    public void Update(TourStartTime time)
    {
        var existingTime = _times.FirstOrDefault(c => c.Id == time.Id);
        if (existingTime != null)
        {
            existingTime.Id = time.Id;
            existingTime.Time = time.Time;
            existingTime.TourId = time.TourId;
            existingTime.Guests = time.Guests;
            existingTime.Status = time.Status;
            existingTime.CurrentCheckpoint = time.CurrentCheckpoint;

            _serializer.ToCSV(FilePath, _times);
        }
    }



    public void Delete(int id)
    {
        var existingTime = _times.FirstOrDefault(c => c.Id == id);
        if (existingTime != null)
        {
            _times.Remove(existingTime);
            _serializer.ToCSV(FilePath, _times);
        }
    }

    public List<TourStartTime> GetAll()
    {
        return _times;
    }


    public int NextId()
    {
        _times = _serializer.FromCSV(FilePath);
        if (_times.Count < 1)
        {
            return 1;
        }
        return _times.Max(c => c.Id) + 1;
    }

    public List<TourStartTime> GetByTourId(int tourId)
    {
        return _times.Where(a => a.TourId == tourId).ToList();
    }

    public TourStartTime GetById(int Id)
    {
        return _times.FirstOrDefault(a => a.Id == Id);
    }


    public TourStartTime GetByTourStartTimeAndId(DateTime tourTime, int TourId)
    {
        return _times.First(a => a.Time == tourTime && a.TourId == TourId);
    }

    public void RemoveByTourStartTimeAndId(DateTime tourTime, int TourId)
    {
        _times.RemoveAll(a => a.Time == tourTime && a.TourId == TourId);
        _serializer.ToCSV(FilePath, _times);
    }

    public void RemoveByTourId(int tourId)
    {
        _times.RemoveAll(a => a.TourId == tourId);
        _serializer.ToCSV(FilePath, _times);
    }

    public List<TourStartTime> GetTimeByTourId(int id)
    {
        return _times.Where(a => a.TourId == id).ToList();
    }

    public void DeleteByTourId(int id)
    {
        _times.RemoveAll(a => a.TourId == id);
        _serializer.ToCSV(FilePath, _times);
    }



}
