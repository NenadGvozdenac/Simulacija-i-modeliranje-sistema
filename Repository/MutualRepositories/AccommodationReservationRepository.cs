using BookingApp.Miscellaneous;
using BookingApp.Model.MutualModels;
using BookingApp.Resources.Types;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.MutualRepositories;

public class AccommodationReservationRepository : IRepository<AccommodationReservation>
{
    private const string FilePath = "../../../Resources/Data/accommodation_reservations.csv";
    private readonly Serializer<AccommodationReservation> _serializer;

    private List<AccommodationReservation> _accommodationreservation;

    public AccommodationReservationRepository()
    {
        _serializer = new Serializer<AccommodationReservation>();
        _accommodationreservation = _serializer.FromCSV(FilePath);
    }

    public static AccommodationReservationRepository GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<AccommodationReservationRepository>();
    }

    public List<AccommodationReservation> GetAll()
    {
        return _accommodationreservation;
    }

    public AccommodationReservation GetById(int id)
    {
        return _accommodationreservation.FirstOrDefault(a => a.Id == id);
    }

    public int NextId()
    {
        _accommodationreservation = _serializer.FromCSV(FilePath);
        if (_accommodationreservation.Count < 1)
        {
            return 1;
        }
        return _accommodationreservation.Max(c => c.Id) + 1;
    }

    public void Add(AccommodationReservation accommodationres)
    {
        accommodationres.Id = NextId();
        _accommodationreservation.Add(accommodationres);
        _serializer.ToCSV(FilePath, _accommodationreservation);
    }

    public void Update(AccommodationReservation accommodationres)
    {
        var existingAccommodationRes = _accommodationreservation.FirstOrDefault(a => a.Id == accommodationres.Id);
        if (existingAccommodationRes != null)
        {
            existingAccommodationRes.AccommodationId = accommodationres.AccommodationId;
            existingAccommodationRes.UserId = accommodationres.UserId;
            existingAccommodationRes.GuestsNumber = accommodationres.GuestsNumber;
            existingAccommodationRes.FirstDateOfStaying = accommodationres.FirstDateOfStaying;
            existingAccommodationRes.LastDateOfStaying = accommodationres.LastDateOfStaying;

            _serializer.ToCSV(FilePath, _accommodationreservation);
        }
    }

    public void Delete(int id)
    {
        var existingAccommodationRes = _accommodationreservation.FirstOrDefault(a => a.Id == id);
        if (existingAccommodationRes != null)
        {
            _accommodationreservation.Remove(existingAccommodationRes);
            _serializer.ToCSV(FilePath, _accommodationreservation);
        }

        
    }
    public List<AccommodationReservation> GetReservationsByAccommodations(List<Accommodation> accommodations)
        {
            List<AccommodationReservation> result = new List<AccommodationReservation>();
            foreach(AccommodationReservation res in GetAll())
            {
                foreach(Accommodation acc in accommodations)
                {
                    if (res.AccommodationId == acc.Id)
                    {
                        result.Add(res);
                    }
                }
            }

            return result;
    }

    public List<DateTime> FindTakenDates(int id)
    {
        List<DateTime> result = new List<DateTime>();

        foreach(AccommodationReservation res in GetAll())
        {
            if (res.AccommodationId == id)
            {
                DateTime tempdate = res.FirstDateOfStaying;

                while (tempdate != res.LastDateOfStaying.AddDays(1))
                {
                    result.Add(tempdate);
                    tempdate = tempdate.AddDays(1);
                }
            }
        }
        
        return result;
    }

    public void CheckDate(AccommodationReservation reservation)
    {
        DateTime dateToday = DateTime.Now;
        DateTime firstDate = reservation.FirstDateOfStaying;
        DateTime lastDate = reservation.LastDateOfStaying;

        if (dateToday > lastDate)
        {
            reservation.ReservationType = ReservationType.Finished;
            Update(reservation);
        }
        else if (dateToday >= firstDate && dateToday <= lastDate)
        {
            reservation.ReservationType = ReservationType.Ongoing;
            Update(reservation);
        }
        else if (dateToday < firstDate)
        {
            reservation.ReservationType = ReservationType.Upcoming;
            Update(reservation);
        }
    }

    public bool IsTimespanFree(DateSpan wantedReservationTimespan, Accommodation accommodation, AccommodationReservationMoving accommodationReservationMoving)
    {
        List<AccommodationReservation> accommodationReservations = GetAll().Where(a => a.AccommodationId == accommodation.Id).ToList();

        foreach (AccommodationReservation accommodationReservation in accommodationReservations)
        {
            if(accommodationReservation.Id == accommodationReservationMoving.ReservationId)
            {
                continue;
            }
            if (wantedReservationTimespan.Start >= accommodationReservation.FirstDateOfStaying && wantedReservationTimespan.Start <= accommodationReservation.LastDateOfStaying)
            {
                return false;
            }
            if (wantedReservationTimespan.End >= accommodationReservation.FirstDateOfStaying && wantedReservationTimespan.End <= accommodationReservation.LastDateOfStaying)
            {
                return false;
            }
        }

        return true;
    }
}
