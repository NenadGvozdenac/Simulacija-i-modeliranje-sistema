using BookingApp.Miscellaneous;
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.Owner;

public class AccommodationReservationService : IService<AccommodationReservation>
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

        AccommodationReservation reservation = accommodationMoving.Reservation;

        reservation.FirstDateOfStaying = wantedDatespan.Start;
        reservation.LastDateOfStaying = wantedDatespan.End;

        _accommodationReservationRepository.Update(reservation);
    }

    public void CheckForCancelledReservations()
    {
        List<AccommodationReservationMoving> movingReservations = _accommodationReservationMovingRepository.GetAll();

        _accommodationReservationMovingRepository.DeleteAll(reservation => GetById(reservation.ReservationId) == null);
    }
}
