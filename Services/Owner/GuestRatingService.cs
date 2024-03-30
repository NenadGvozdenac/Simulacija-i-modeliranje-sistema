using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.Owner;

public class GuestRatingService
{
    private AccommodationRepository _accommodationRepository;
    private UserRepository _userRepository;
    private GuestRatingRepository _guestRatingRepository;

    private static Lazy<GuestRatingService> _instance = new Lazy<GuestRatingService>(() => new GuestRatingService());
    public GuestRatingService()
    {
        _accommodationRepository = AccommodationRepository.GetInstance();
        _userRepository = UserRepository.GetInstance();
        _guestRatingRepository = GuestRatingRepository.GetInstance();
    }

    public static GuestRatingService GetInstance()
    {
        return _instance.Value;
    }

    public List<GuestRating> GetAll()
    {
        List<GuestRating> guestRatings = _guestRatingRepository.GetAll();
        guestRatings.ForEach(guestRating => {
            guestRating.Reservation = AccommodationReservationRepository.GetInstance().GetById(guestRating.ReservationId);
        });
        return _guestRatingRepository.GetAll();
    }

    public List<GuestRating> GetByAccommodationId(int id)
    {
        List<GuestRating> guestRatings = _guestRatingRepository.GetGuestRatingsByAccommodationId(id);
        guestRatings.ForEach(guestRating =>
        {
            guestRating.Reservation = AccommodationReservationRepository.GetInstance().GetById(guestRating.ReservationId);
        });
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
}
