using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.Owner;

public class LocationService
{
    private LocationRepository _locationRepository;

    private static Lazy<LocationService> instance = new Lazy<LocationService>(() => new LocationService());
    private LocationService()
    {
        _locationRepository = LocationRepository.GetInstance();
    }

    public static LocationService GetInstance()
    {
        return instance.Value;
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
}
