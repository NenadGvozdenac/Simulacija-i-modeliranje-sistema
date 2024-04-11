using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Resources.Types;
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
    private GuestRatingRepository _guestRatingRepository;

    public AccommodationReservationService()
    {
        _guestRatingRepository = App.ServiceProvider.GetRequiredService<GuestRatingRepository>();
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

        overlappingReservations.ForEach(reservation => {
            Delete(reservation);
            _guestRatingRepository.DeleteAll(guestRating => guestRating.ReservationId == reservation.Id);
        });

        AccommodationReservation reservation = accommodationMoving.Reservation;

        reservation.FirstDateOfStaying = wantedDatespan.Start;
        reservation.LastDateOfStaying = wantedDatespan.End;

        _accommodationReservationRepository.Update(reservation);

        CheckForCancelledReservations();
        DeleteAllReservationsReschedulingForReservation(reservation.Id);
    }

    private void DeleteAllReservationsReschedulingForReservation(int id)
    {
        _accommodationReservationMovingRepository.DeleteAll(reservation => reservation.ReservationId == id && reservation.Status == ReschedulingStatus.Pending);
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
        List<AccommodationReservationMoving> movingReservations = _accommodationReservationMovingRepository.GetAll().Where(m => m.Status == ReschedulingStatus.Pending).ToList();

        _accommodationReservationMovingRepository.DeleteAll(reservation => GetById(reservation.ReservationId) == null);
    }

    public List<AccommodationReservation> GetByAccommodationId(int accommodationId)
    {
        return _accommodationReservationRepository.GetByAccommodationId(accommodationId);
    }

    public List<AccommodationReservationMoving> GetMovingsByOwnerId(int ownerId)
    {
        List<Accommodation> ownerAccommodations = _accommodationRepository.GetAccommodationsByOwnerId(ownerId);
        List< AccommodationReservationMoving > lista = _accommodationReservationMovingRepository.GetMovingsByAccommodations(ownerAccommodations);

        lista.ForEach(moving =>
        {
            moving.Accommodation = ownerAccommodations.Find(accommodation => accommodation.Id == moving.AccommodationId);
            moving.Guest = UserRepository.GetInstance().GetById(moving.GuestId);
            moving.Reservation = GetById(moving.ReservationId);
        });

        return lista;
    }
}
