using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.View.GuestViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class TourRepository : ITourRepository
{
    private const string FilePath = "../../../Resources/Data/Tours.csv";

    private readonly Serializer<Tour> _serializer;

    private List<Tour> _tours;

    public TourRepository()
    {
        _serializer = new Serializer<Tour>();
        _tours = _serializer.FromCSV(FilePath);
    }

    public List<Tour> GetAll()
    {
        return _tours;
    }

    public Tour GetById(int id)
    {
        return _tours.FirstOrDefault(a => a.Id == id);
    }


    public Tour GetByName(string name)
    {
        _tours = _serializer.FromCSV(FilePath);
        return _tours.FirstOrDefault(t => t.Name == name);
    }

    /// <summary>
    /// Adds a tour to the repository
    /// </summary>
    /// <param name="tour"></param>
    public void Add(Tour tour)
    {
        _tours.Add(tour);
        _serializer.ToCSV(FilePath, _tours);
    }

    /// <summary>
    /// Gets the next id for the tour
    /// </summary>
    /// <returns></returns>
    public int NextId()
    {
        _tours = _serializer.FromCSV(FilePath);
        if (_tours.Count < 1)
        {
            return 1;
        }
        return _tours.Max(c => c.Id) + 1;
    }

    public void Update(Tour tour)
    {
        var existingTour = _tours.FirstOrDefault(t => t.Id == tour.Id);
        if (existingTour != null)
        {
            existingTour.Name = tour.Name;
            existingTour.Location = tour.Location;
            existingTour.Duration = tour.Duration;
            existingTour.Dates = tour.Dates;
            existingTour.Capacity = tour.Capacity;
            existingTour.Description = tour.Description;
            existingTour.Checkpoints = tour.Checkpoints;
            existingTour.Language = tour.Language;
            existingTour.Images = tour.Images;
            existingTour.OwnerId = tour.OwnerId;
            _serializer.ToCSV(FilePath, _tours);
        }
    }

    public void Delete(int id)
    {
        var existingTour = _tours.FirstOrDefault(t => t.Id == id);
        if (existingTour != null)
        {
            _tours.Remove(existingTour);
            _serializer.ToCSV(FilePath, _tours);
        }
    }














}
