using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class AccommodationReservationService
{
    private AccommodationRepository _accommodationRepository;
    private AccommodationReservationRepository _accommodationReservationRepository;
    private AccommodationReservationMovingRepository _accommodationReservationMovingRepository;

    public AccommodationReservationService()
    {
        _accommodationRepository = App.ServiceProvider.GetRequiredService<AccommodationRepository>();
        _accommodationReservationRepository = App.ServiceProvider.GetRequiredService<AccommodationReservationRepository>();
        _accommodationReservationMovingRepository = App.ServiceProvider.GetRequiredService<AccommodationReservationMovingRepository>();
    }

    public static AccommodationReservationService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<AccommodationReservationService>();
    }

    public List<AccommodationReservation> GetReservationsByOwnerId(int ownerId)
    {
        List<Accommodation> accommodations = _accommodationRepository.GetAccommodationsByOwnerId(ownerId);
        List<AccommodationReservation> reservations = _accommodationReservationRepository.GetReservationsByAccommodations(accommodations);

        reservations.ForEach(reservation => _accommodationReservationRepository.CheckDate(reservation));

        return _accommodationReservationRepository.GetReservationsByAccommodations(accommodations);
    }

    public List<AccommodationReservation> GetAll()
    {
        return _accommodationReservationRepository.GetAll();
    }

    public void Add(AccommodationReservation reservation)
    {
        _accommodationReservationRepository.Add(reservation);
    }

    public void Update(AccommodationReservation reservation)
    {
        _accommodationReservationRepository.Update(reservation);
    }

    public void Delete(AccommodationReservation reservation)
    {
        _accommodationReservationRepository.Delete(reservation.Id);
    }

    public AccommodationReservation GetById(int reservationId)
    {
        return _accommodationReservationRepository.GetById(reservationId);
    }

    public void MoveReservation(AccommodationReservationMoving accommodationMoving)
    {
        DateSpan wantedDatespan = accommodationMoving.WantedReservationTimespan;

        List<AccommodationReservation> reservations = GetByAccommodationId(accommodationMoving.AccommodationId);

        List<AccommodationReservation> overlappingReservations = GetOverlappingReservations(wantedDatespan, reservations);

        overlappingReservations.Remove(accommodationMoving.Reservation);

        overlappingReservations.ForEach(reservation => Delete(reservation));

        AccommodationReservation reservation = accommodationMoving.Reservation;

        reservation.FirstDateOfStaying = wantedDatespan.Start;
        reservation.LastDateOfStaying = wantedDatespan.End;

        _accommodationReservationRepository.Update(reservation);
    }

    public List<AccommodationReservation> GetOverlappingReservations(DateSpan wantedDatespan, List<AccommodationReservation> reservations)
    {
        return reservations.Where(reservation =>
        {
            DateSpan reservationDatespan = new DateSpan(reservation.FirstDateOfStaying, reservation.LastDateOfStaying);
            return reservationDatespan.Overlaps(wantedDatespan);
        }).ToList();
    }

    public void CheckForCancelledReservations()
    {
        List<AccommodationReservationMoving> movingReservations = _accommodationReservationMovingRepository.GetAll();

        _accommodationReservationMovingRepository.DeleteAll(reservation => GetById(reservation.ReservationId) == null);
    }

    public List<AccommodationReservation> GetByAccommodationId(int accommodationId)
    {
        return _accommodationReservationRepository.GetByAccommodationId(accommodationId);
    }
}
