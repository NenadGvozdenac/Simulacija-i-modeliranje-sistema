using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        void Add(Location location);
        void Delete(int id);
        List<Location> GetAll();
        Location GetById(int id);
        List<string> GetCitiesByCountry(string country);
        List<string> GetCountries();
        Location GetLocationByCityAndCountry(string city, string country);
        List<string> GetLocationsFormatted();
        void Update(Location location);
    }
}