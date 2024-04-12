using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuestRatingRepository
    {
        void Add(GuestRating guestRating);
        void Delete(int id);
        void DeleteAll(Func<GuestRating, bool> value);
        bool ExistsReviewOfGuest(User guest, Accommodation accommodation, bool isChecked = true);
        List<GuestRating> GetAll();
        GuestRating GetById(int id);
        GuestRating GetGuestRatingByReservationId(int id);
        List<GuestRating> GetGuestRatingsByAccommodationId(int accommodationId);
        List<GuestRating> GetGuestRatingsByUserId(int userId);
        int NextId();
        void Update(GuestRating guestRating);
    }
}