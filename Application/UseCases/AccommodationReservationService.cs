﻿using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
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
    private IAccommodationRepository _accommodationRepository;
    private IAccommodationReservationRepository _accommodationReservationRepository;
    private IAccommodationReservationMovingRepository _accommodationReservationMovingRepository;
    private IGuestRatingRepository _guestRatingRepository;
    private IUserRepository _userRepository;

    public AccommodationReservationService(IAccommodationRepository accommodationRepository, 
        IAccommodationReservationRepository accommodationReservationRepository,
        IAccommodationReservationMovingRepository accommodationReservationMovingRepository,
        IGuestRatingRepository guestRatingRepository,
        IUserRepository userRepository)
    {
        _accommodationRepository = accommodationRepository;
        _accommodationReservationRepository = accommodationReservationRepository;
        _accommodationReservationMovingRepository = accommodationReservationMovingRepository;
        _guestRatingRepository = guestRatingRepository;
        _userRepository = userRepository;
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

    public List<AccommodationReservation> GetByUserId(int userId)
    {
        return GetAll().Where(reservation => reservation.UserId == userId).ToList();
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

    public void DeleteById(int id)
    {
        _accommodationReservationRepository.Delete(id);
    }

    public AccommodationReservation GetById(int reservationId)
    {
        return _accommodationReservationRepository.GetById(reservationId);
    }

    public void MoveReservation(AccommodationReservationMoving accommodationMoving)
    {
        DateSpan wantedDatespan = accommodationMoving.WantedReservationTimespan;

        List<AccommodationReservation> reservations = GetByAccommodationId(accommodationMoving.AccommodationId);

        DeleteOverlappingReservations(reservations, wantedDatespan, accommodationMoving);

        AccommodationReservation reservation = accommodationMoving.Reservation;

        reservation.FirstDateOfStaying = wantedDatespan.Start;
        reservation.LastDateOfStaying = wantedDatespan.End;

        _accommodationReservationRepository.Update(reservation);

        CheckForCancelledReservations();
        DeleteAllReservationsReschedulingForReservation(reservation.Id);
    }

    private void DeleteOverlappingReservations(List<AccommodationReservation> reservations, DateSpan wantedDatespan, AccommodationReservationMoving accommodationMoving)
    {
        List<AccommodationReservation> overlappingReservations = GetOverlappingReservations(wantedDatespan, reservations);

        overlappingReservations.Remove(accommodationMoving.Reservation);

        overlappingReservations.ForEach(reservation => {
            Delete(reservation);
            _guestRatingRepository.DeleteAll(guestRating => guestRating.ReservationId == reservation.Id);
        });
    }

    private void DeleteAllReservationsReschedulingForReservation(int id)
    {
        _accommodationReservationMovingRepository.DeleteAll(reservation => reservation.ReservationId == id && reservation.Status == ReschedulingStatus.Pending);
    }

    public void AddMoving(AccommodationReservationMoving accommodationReservationMoving)
    {
        _accommodationReservationMovingRepository.Add(accommodationReservationMoving);
    }

    public List<AccommodationReservationMoving> GetAllMoving()
    {
        return _accommodationReservationMovingRepository.GetAll();
    }

    public void UpdateMoving(AccommodationReservationMoving accommodationReservationMoving)
    {
        _accommodationReservationMovingRepository.Update(accommodationReservationMoving);
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
            moving.Guest = _userRepository.GetById(moving.GuestId);
            moving.Reservation = GetById(moving.ReservationId);
        });

        return lista;
    }

    public bool IsTimespanFree(DateSpan wantedReservationTimespan, Accommodation accommodation, AccommodationReservationMoving accommodationReservationMoving)
    {
        List<AccommodationReservation> accommodationReservations = GetByAccommodationId(accommodation.Id);

        foreach (AccommodationReservation accommodationReservation in accommodationReservations) 
        {
            if (accommodationReservation.Id == accommodationReservationMoving.ReservationId) 
            {
                continue;
            }

            if(DateSpan.IsOverlapping(new(accommodationReservation.FirstDateOfStaying, accommodationReservation.LastDateOfStaying), wantedReservationTimespan)) 
            {
                return false;
            }
        }

        return true;
    }
    public List<DateTime> FindTakenDates(int id)
    {
        return _accommodationReservationRepository.FindTakenDates(id);
    }

    public bool HasActiveReservation(Accommodation accommodation, DateSpan dateSpan)
    {
        List<AccommodationReservation> reservations = GetByAccommodationId(accommodation.Id);

        foreach (AccommodationReservation reservation in reservations)
        {
            DateSpan reservationDateSpan = new DateSpan(reservation.FirstDateOfStaying, reservation.LastDateOfStaying);

            if (reservationDateSpan.Overlaps(dateSpan))
            {
                return true;
            }
        }

        return false;
    }
}
