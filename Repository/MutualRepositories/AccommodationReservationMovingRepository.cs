using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using BookingApp.View.GuestViews;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.MutualRepositories;

public class AccommodationReservationMovingRepository : IRepository<AccommodationReservationMoving>
{
    private const string FilePath = "../../../Resources/Data/accommodation_reservation_moving.csv";
   
    private readonly Serializer<AccommodationReservationMoving> _serializer;

    public List<AccommodationReservationMoving> AccommodationReservationMovings { get; set; }

    public AccommodationReservationMovingRepository()
    {
        _serializer = new Serializer<AccommodationReservationMoving>();
        AccommodationReservationMovings = _serializer.FromCSV(FilePath);
    }

    public static AccommodationReservationMovingRepository GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<AccommodationReservationMovingRepository>();
    }

    public void Add(AccommodationReservationMoving accommodationReservationMoving)
    {
        accommodationReservationMoving.Id = NextId();
        AccommodationReservationMovings.Add(accommodationReservationMoving);
        _serializer.ToCSV(FilePath, AccommodationReservationMovings);
    }

    public List<AccommodationReservationMoving> GetAll()
    {
        return AccommodationReservationMovings;
    }

    private int NextId()
    {
        AccommodationReservationMovings = _serializer.FromCSV(FilePath);
        if (AccommodationReservationMovings.Count < 1)
        {
            return 1;
        }
        return AccommodationReservationMovings.Max(c => c.Id) + 1;
    }

    public void Update(AccommodationReservationMoving accommodationReservationMoving)
    {
        AccommodationReservationMoving oldAccommodationReservationMoving = AccommodationReservationMovings.FirstOrDefault(accommodation => accommodation.Id == accommodationReservationMoving.Id);

        if (oldAccommodationReservationMoving == null)
        {
            return;
        }

        oldAccommodationReservationMoving.AccommodationId = accommodationReservationMoving.AccommodationId;
        oldAccommodationReservationMoving.ReservationId = accommodationReservationMoving.ReservationId;
        oldAccommodationReservationMoving.GuestId = accommodationReservationMoving.GuestId;
        oldAccommodationReservationMoving.CurrentReservationTimespan = accommodationReservationMoving.CurrentReservationTimespan;
        oldAccommodationReservationMoving.WantedReservationTimespan = accommodationReservationMoving.WantedReservationTimespan;
        oldAccommodationReservationMoving.Status = accommodationReservationMoving.Status;
        oldAccommodationReservationMoving.Comment = accommodationReservationMoving.Comment;

        _serializer.ToCSV(FilePath, AccommodationReservationMovings);
    }

    public void Delete(int id)
    {
        AccommodationReservationMoving accommodationReservationMoving = AccommodationReservationMovings.FirstOrDefault(accommodation => accommodation.Id == id);

        if (accommodationReservationMoving == null)
        {
            return;
        }

        AccommodationReservationMovings.Remove(accommodationReservationMoving);
        _serializer.ToCSV(FilePath, AccommodationReservationMovings);
    }

    public List<AccommodationReservationMoving> GetMovingsByAccommodations(List<Accommodation> ownerAccommodations)
    {
        List<AccommodationReservationMoving> movings = new List<AccommodationReservationMoving>();

        foreach (Accommodation accommodation in ownerAccommodations)
        {
            movings.AddRange(AccommodationReservationMovings.Where(moving => moving.AccommodationId == accommodation.Id).ToList());
        }

        return movings;
    }

    public AccommodationReservationMoving GetById(int id)
    {
        return AccommodationReservationMovings.FirstOrDefault(a => a.Id == id);
    }

    public void DeleteAll(Func<AccommodationReservationMoving, bool> value)
    { 
        AccommodationReservationMovings.RemoveAll(value.Invoke);
        _serializer.ToCSV(FilePath, AccommodationReservationMovings);
    }
}
