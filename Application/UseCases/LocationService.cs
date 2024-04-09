using BookingApp.Domain.Models;
using BookingApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class LocationService
{
    private LocationRepository _locationRepository;
    public LocationService()
    {
        _locationRepository = App.ServiceProvider.GetRequiredService<LocationRepository>();
    }

    public static LocationService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<LocationService>();
    }

    public List<string> GetCountries()
    {
        return _locationRepository.GetCountries();
    }

    public List<string> GetCitiesByCountry(string country)
    {
        return _locationRepository.GetCitiesByCountry(country);
    }

    public List<Location> GetCities()
    {
        return _locationRepository.GetAll();
    }

    public Location GetLocationByCityAndCountry(string city, string country)
    {
        return _locationRepository.GetLocationByCityAndCountry(city, country);
    }

    public void Add(Location entity)
    {
        _locationRepository.Add(entity);
    }

    public void Delete(Location entity)
    {
        _locationRepository.Delete(entity.Id);
    }

    public List<Location> GetAll()
    {
        return _locationRepository.GetAll();
    }

    public Location GetById(int entityId)
    {
        return _locationRepository.GetById(entityId);
    }

    public void Update(Location entity)
    {
        _locationRepository.Update(entity);
    }
}
