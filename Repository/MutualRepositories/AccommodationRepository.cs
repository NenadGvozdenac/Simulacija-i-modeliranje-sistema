using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Serializer;
using BookingApp.Model.Vlasnik;

namespace BookingApp.Repository;

class AccommodationRepository
{
    private const string FilePath = "../../../Resources/Data/accommodations.csv";

    private readonly Serializer<Accomodation> _serializer;

    private List<Accomodation> _accommodations;

    public AccommodationRepository()
    {
        _serializer = new Serializer<Accomodation>();
        _accommodations = _serializer.FromCSV(FilePath);
    }

    public List<Accomodation> GetAll()
    {
        return _accommodations;
    }

    public Accomodation GetById(int id)
    {
        return _accommodations.FirstOrDefault(a => a.Id == id);
    }

    public void Add(Accomodation accommodation)
    {
        _accommodations.Add(accommodation);
        _serializer.ToCSV(FilePath, _accommodations);
    }

    public void Update(Accomodation accommodation)
    {
        var existingAccommodation = _accommodations.FirstOrDefault(a => a.Id == accommodation.Id);
        if (existingAccommodation != null)
        {
            existingAccommodation.Name = accommodation.Name;
            existingAccommodation.Location = accommodation.Location;
            existingAccommodation.Type = accommodation.Type;
            existingAccommodation.MaxGuestNumber = accommodation.MaxGuestNumber;
            existingAccommodation.MinReservationDays = accommodation.MinReservationDays;
            existingAccommodation.CancellationPeriodDays = accommodation.CancellationPeriodDays;
            existingAccommodation.Images = accommodation.Images;
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
}
