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

namespace BookingApp.Application.UseCases;

public class AccommodationRenovationService
{
    private IAccommodationRepository _accommodationRepository;
    private IAccommodationRenovationRepository _accommodationRenovationRepository;

    public AccommodationRenovationService(IAccommodationRepository accommodationRepository
        , IAccommodationRenovationRepository accommodationRenovationRepository)
    {
        _accommodationRepository = accommodationRepository;
        _accommodationRenovationRepository = accommodationRenovationRepository;
    }

    public static AccommodationRenovationService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<AccommodationRenovationService>();
    }

    public void AddAccommodationRenovation(AccommodationRenovation accommodationRenovation)
    {
        _accommodationRenovationRepository.Add(accommodationRenovation);
    }

    public void UpdateAccommodationRenovation(AccommodationRenovation accommodationRenovation)
    {
        _accommodationRenovationRepository.Update(accommodationRenovation);
    }

    public void DeleteAccommodationRenovation(AccommodationRenovation accommodationRenovation)
    {
        _accommodationRenovationRepository.Delete(accommodationRenovation.Id);
    }

    public List<AccommodationRenovation> GetAllAccommodationRenovations()
    {
        List<AccommodationRenovation> renovations = _accommodationRenovationRepository.GetAll();

        renovations.ForEach(r => r.Accommodation = _accommodationRepository.GetById(r.AccommodationId));

        return renovations;
    }

    public List<AccommodationRenovation> GetAccommodationRenovationsByAccommodationId(int accommodationId)
    {
        List<AccommodationRenovation> renovations = _accommodationRenovationRepository.GetAll().Where(r => r.AccommodationId == accommodationId).ToList();
        renovations.ForEach(r => r.Accommodation = _accommodationRepository.GetById(r.AccommodationId));
        return renovations;
    }

    public List<AccommodationRenovation> GetRenovationsByOwnerId(int id)
    {
        List<int> ownerAccommodations = _accommodationRepository.GetAccommodationsByOwnerId(id).Select(a => a.Id).ToList();
        List<AccommodationRenovation> renovations = _accommodationRenovationRepository.GetAll().Where(r => ownerAccommodations.Contains(r.AccommodationId)).ToList();
        renovations.ForEach(r => r.Accommodation = _accommodationRepository.GetById(r.AccommodationId));
        return renovations;
    }

    public DateSpan GetLastRenovationDate(int id)
    {
        List<AccommodationRenovation> renovations = GetAccommodationRenovationsByAccommodationId(id).Where(d => d.DateSpan.End <= DateTime.Now).ToList();
        
        if (renovations.Count == 0)
        {
            return null;
        }

        AccommodationRenovation lastRenovation = renovations.OrderByDescending(r => r.DateSpan.End).First();

        return lastRenovation.DateSpan;
    }

    public List<DateSpan> FindAvailableTimespanForRenovation(Accommodation accommodation, DateTime startDate, DateTime endDate, int number)
    {
        List<DateSpan> dateSpans = new();

        do
        {
            DateSpan dateSpan = new(startDate, startDate.AddDays(number));

            if (!AccommodationReservationService.GetInstance().HasActiveReservation(accommodation, dateSpan))
            {
                dateSpans.Add(dateSpan);
            }

            startDate = startDate.AddDays(1);
        } while (startDate.AddDays(number) < endDate);

        return dateSpans;
    }

    public void ScheduleRenovation(Accommodation accommodation, DateSpan selectedDateSpan)
    {
        AccommodationRenovation renovation = new()
        {
            AccommodationId = accommodation.Id,
            RenovationDays = selectedDateSpan.Days,
            DateSpan = selectedDateSpan,
            Status = RenovationStatus.Upcoming
        };

        AddAccommodationRenovation(renovation);
    }
}
