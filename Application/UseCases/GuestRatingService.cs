using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class GuestRatingService
{
    private IGuestRatingRepository _guestRatingRepository;
    private IAccommodationReservationRepository _accommodationReservationRepository;
    public GuestRatingService(IGuestRatingRepository guestRatingRepository, IAccommodationReservationRepository accommodationReservationRepository)
    {
        _guestRatingRepository = guestRatingRepository;
        _accommodationReservationRepository = accommodationReservationRepository;
    }

    public static GuestRatingService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<GuestRatingService>();
    }

    public List<GuestRating> GetAll()
    {
        List<GuestRating> guestRatings = _guestRatingRepository.GetAll();
        guestRatings.ForEach(guestRating => guestRating.Reservation = _accommodationReservationRepository.GetById(guestRating.ReservationId));
        return _guestRatingRepository.GetAll();
    }

    public List<GuestRating> GetByAccommodationId(int id)
    {
        List<GuestRating> guestRatings = _guestRatingRepository.GetGuestRatingsByAccommodationId(id);
        guestRatings.ForEach(guestRating => guestRating.Reservation = _accommodationReservationRepository.GetById(guestRating.ReservationId));
        return guestRatings;
    }

    public void Add(GuestRating guestRating)
    {
        _guestRatingRepository.Add(guestRating);
    }

    public void Update(GuestRating guestRating)
    {
        _guestRatingRepository.Update(guestRating);
    }

    public void Delete(GuestRating guestRating)
    {
        _guestRatingRepository.Delete(guestRating.Id);
    }

    public GuestRating GetById(int id)
    {
        GuestRating guestRating = _guestRatingRepository.GetById(id);
        return _guestRatingRepository.GetById(id);
    }

    internal IEnumerable<GuestRating> GetByOwnerId()
    {
        throw new NotImplementedException();
    }

    public bool ExistsReviewOfGuest(User guest, Accommodation accommodation)
    {
        return _guestRatingRepository.ExistsReviewOfGuest(guest, accommodation);
    }

    internal void DeleteAll(Func<GuestRating, bool> value)
    {
        _guestRatingRepository.DeleteAll(value);
    }
}
