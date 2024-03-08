using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Serializer;
using BookingApp.Model.MutualModels;
using System.Xml.Linq;

namespace BookingApp.Repository;

public class AccommodationRepository
{
    private const string FilePath = "../../../Resources/Data/accommodations.csv";

    private readonly Serializer<Accommodation> _serializer;

    private List<Accommodation> _accommodations;

    public AccommodationRepository()
    {
        _serializer = new Serializer<Accommodation>();
        _accommodations = _serializer.FromCSV(FilePath);
    }

    public List<Accommodation> GetAll()
    {
        return _accommodations;
    }

    public Accommodation GetById(int id)
    {
        return _accommodations.FirstOrDefault(a => a.Id == id);
    }

    public int NextId()
    {
        _accommodations = _serializer.FromCSV(FilePath);
        if (_accommodations.Count < 1)
        {
            return 1;
        }
        return _accommodations.Max(c => c.Id) + 1;
    }

    public void Add(Accommodation accommodation)
    {
        _accommodations.Add(accommodation);
        _serializer.ToCSV(FilePath, _accommodations);
    }

    public void Update(Accommodation accommodation)
    {
        var existingAccommodation = _accommodations.FirstOrDefault(a => a.Id == accommodation.Id);
        if (existingAccommodation != null)
        {
            existingAccommodation.Name = accommodation.Name;
            existingAccommodation.LocationId = accommodation.LocationId;
            existingAccommodation.Type = accommodation.Type;
            existingAccommodation.MaxGuestNumber = accommodation.MaxGuestNumber;
            existingAccommodation.MinReservationDays = accommodation.MinReservationDays;
            existingAccommodation.CancellationPeriodDays = accommodation.CancellationPeriodDays;
            existingAccommodation.Images = accommodation.Images;
            existingAccommodation.AverageReviewScore = accommodation.AverageReviewScore;
            existingAccommodation.Price = accommodation.Price;
            _serializer.ToCSV(FilePath, _accommodations);
        }
    }

    public void Delete(int id)
    {
        var existingAccommodation = _accommodations.FirstOrDefault(a => a.Id == id);
        if (existingAccommodation != null)
        {
            _accommodations.Remove(existingAccommodation);
            _serializer.ToCSV(FilePath, _accommodations);
        }
    }

    public List<Accommodation> GetAccommodationsByOwnerId(int id)
    {
        return _accommodations.Where(a => a.OwnerId == id).ToList();
    }
}
