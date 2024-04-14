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

public class LocationRepository : BaseRepository<Location>, ILocationRepository
{
    public LocationRepository() : base("../../../Resources/Data/locations.csv")
    {
    }

    public Location GetLocationByCityAndCountry(string city, string country)
    {
        return GetAll().FirstOrDefault(a => a.City == city && a.Country == country);
    }

    public List<string> GetCitiesByCountry(string country)
    {
        return GetAll().Where(a => a.Country == country).Select(a => a.City).ToList();
    }

    public List<string> GetCountries()
    {
        return GetAll().Select(a => a.Country).Distinct().ToList();
    }

    public List<string> GetLocationsFormatted()
    {
        return GetAll().Select(a => $"{a.City}, {a.Country}").ToList();
    }
}
