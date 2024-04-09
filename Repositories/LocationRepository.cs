using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class LocationRepository : IRepository<Location>, ILocationRepository
{
    private const string FilePath = "../../../Resources/Data/locations.csv";

    private readonly Serializer<Location> _serializer;

    private List<Location> _locations;

    public LocationRepository()
    {
        _serializer = new Serializer<Location>();
        _locations = _serializer.FromCSV(FilePath);
    }

    public static LocationRepository GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<LocationRepository>();
    }

    public List<Location> GetAll()
    {
        return _locations;
    }

    public Location GetById(int id)
    {
        return _locations.FirstOrDefault(a => a.Id == id);
    }

    public void Add(Location location)
    {
        _locations.Add(location);
        _serializer.ToCSV(FilePath, _locations);
    }

    public void Update(Location location)
    {
        var existingLocation = _locations.FirstOrDefault(a => a.Id == location.Id);
        if (existingLocation != null)
        {
            existingLocation.Id = location.Id;
            existingLocation.City = location.City;
            existingLocation.Country = location.Country;
            _serializer.ToCSV(FilePath, _locations);
        }
    }

    public void Delete(int id)
    {
        var existingAccommodation = _locations.FirstOrDefault(a => a.Id == id);
        if (existingAccommodation != null)
        {
            _locations.Remove(existingAccommodation);
            _serializer.ToCSV(FilePath, _locations);
        }
    }

    public Location GetLocationByCityAndCountry(string city, string country)
    {
        return _locations.FirstOrDefault(a => a.City == city && a.Country == country);
    }

    public List<string> GetCitiesByCountry(string country)
    {
        return _locations.Where(a => a.Country == country).Select(a => a.City).ToList();
    }

    public List<string> GetCountries()
    {
        return _locations.Select(a => a.Country).Distinct().ToList();
    }
}
