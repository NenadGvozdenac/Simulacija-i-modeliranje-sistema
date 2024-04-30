using BookingApp.Application.UseCases;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models;

public class Month : ISerializable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Days { get; set; }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
        Days = int.Parse(values[2]);
    }

    public string[] ToCSV()
    {
        return new string[] { Id.ToString(), Name, Days.ToString() };
    }

    public static int GetMostPopularMonthInYear(int id, string selectedYearDetails)
    {
        List<AccommodationReservation> accommodationReservations = AccommodationReservationService.GetInstance().GetByAccommodationId(id)
            .Where(res => res.FirstDateOfStaying.Year.ToString() == selectedYearDetails).ToList();

        if (accommodationReservations.Count == 0)
            return -1;

        List<int> daysOccupied = new List<int>(new int[12]);

        foreach (var reservation in accommodationReservations)
        {
            for (int i = reservation.FirstDateOfStaying.Month; i <= reservation.LastDateOfStaying.Month; i++)
            {
                daysOccupied[i - 1] += (reservation.LastDateOfStaying - reservation.FirstDateOfStaying).Days;
            }
        }

        var month = daysOccupied.IndexOf(daysOccupied.Max()) + 1;

        return month;
    }

    public static int GetNumberOfReservationsPerMonth(int id, string selectedYear, string selectedMonth)
    {
        List<AccommodationReservation> accommodationReservations =
            AccommodationReservationService.GetInstance().GetByAccommodationId(id)
            .Where(res => res.FirstDateOfStaying.Year.ToString() == selectedYear
                && res.FirstDateOfStaying.Month == MonthService.GetInstance().GetMonthByName(selectedMonth).Id).ToList();

        return accommodationReservations.Count;
    }

    public static int GetNumberOfCancelledReservationsPerMonth(int id, string selectedYear, string selectedMonth)
    {
        List<AccommodationReservation> accommodationReservations =
            AccommodationReservationService.GetInstance().GetByAccommodationId(id)
            .Where(res => res.FirstDateOfStaying.Year.ToString() == selectedYear
                           && res.FirstDateOfStaying.Month == MonthService.GetInstance().GetMonthByName(selectedMonth).Id
                                          && res.ReservationType == ReservationType.Cancelled).ToList();

        return accommodationReservations.Count;
    }

    public static int GetNumberOfMovedReservationsPerMonth(int id, string selectedYear, string selectedMonth)
    {
        List<AccommodationReservationMoving> accommodationReservations =
            AccommodationReservationService.GetInstance().GetMovingsByOwnerId(id)
            .Where(moving => moving.CurrentReservationTimespan.Start.Year.ToString() == selectedYear
                                        && moving.CurrentReservationTimespan.Start.Month == MonthService.GetInstance().GetMonthByName(selectedMonth).Id).ToList();

        return accommodationReservations.Count;
    }

    public static int GetNumberOfRenovationRecommendationsPerMonth(int id, string selectedYear, string selectedMonth)
    {
        List<AccommodationReview> accommodationReviews =
            AccommodationReviewService.GetInstance().GetByAccommodationId(id)
            .Where(review => review.RequiresRenovation)
            .Where(review => review.Reservation.FirstDateOfStaying.Year.ToString() == selectedYear
                                       && review.Reservation.FirstDateOfStaying.Month.ToString() == selectedMonth).ToList();

        return accommodationReviews.Count;
    }
}
