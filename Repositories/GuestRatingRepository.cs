using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class GuestRatingRepository : BaseRepository<GuestRating>, IGuestRatingRepository
{
    public GuestRatingRepository() : base("../../../Resources/Data/guest_ratings.csv")
    {
    }

    public List<GuestRating> GetGuestRatingsByAccommodationId(int accommodationId)
    {
        return GetAll().Where(guestRating => guestRating.AccommodationId == accommodationId).ToList();
    }

    public List<GuestRating> GetGuestRatingsByUserId(int userId)
    {
        return GetAll().Where(guestRating => guestRating.GuestId == userId).ToList();
    }

    public GuestRating GetGuestRatingByReservationId(int id)
    {
        return GetAll().FirstOrDefault(guestRating => guestRating.ReservationId == id);
    }

    public bool ExistsReviewOfGuest(User guest, Accommodation accommodation, bool isChecked = true)
    {
        return GetAll().Any(guestRating => guestRating.GuestId == guest.Id && guestRating.AccommodationId == accommodation.Id && guestRating.IsChecked == isChecked);
    }

    public void DeleteAll(Func<GuestRating, bool> value)
    {
        DeleteRange(GetAll().Where(value).ToList());
    }
}
