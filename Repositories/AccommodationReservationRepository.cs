using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Resources.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class AccommodationReservationRepository : BaseRepository<AccommodationReservation>, IAccommodationReservationRepository
{
    public AccommodationReservationRepository() : base("../../../Resources/Data/accommodation_reservations.csv") {}
    public List<AccommodationReservation> GetReservationsByAccommodations(List<Accommodation> accommodations)
    {
        List<AccommodationReservation> result = new List<AccommodationReservation>();
        foreach (AccommodationReservation res in GetAll())
        {
            foreach (Accommodation acc in accommodations)
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

        foreach (AccommodationReservation res in GetAll())
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

    public List<AccommodationReservation> GetByAccommodationId(int accommodationId)
    {
        return GetAll().Where(a => a.AccommodationId == accommodationId).ToList();
    }
}
