﻿using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class GuestRatingRepository : IRepository<GuestRating>, IGuestRatingRepository
{
    private const string FilePath = "../../../Resources/Data/guest_ratings.csv";
    private readonly Serializer<GuestRating> _serializer;

    private List<GuestRating> _guestRatings;

    public GuestRatingRepository()
    {
        _serializer = new Serializer<GuestRating>();
        _guestRatings = _serializer.FromCSV(FilePath);
    }

    public List<GuestRating> GetGuestRatingsByAccommodationId(int accommodationId)
    {
        return _guestRatings.Where(guestRating => guestRating.AccommodationId == accommodationId).ToList();
    }

    public void Add(GuestRating guestRating)
    {
        guestRating.Id = NextId();
        _guestRatings.Add(guestRating);
        _serializer.ToCSV(FilePath, _guestRatings);
    }

    public int NextId()
    {
        _guestRatings = _serializer.FromCSV(FilePath);
        if (_guestRatings.Count < 1)
        {
            return 1;
        }
        return _guestRatings.Max(c => c.Id) + 1;
    }

    public void Update(GuestRating guestRating)
    {
        GuestRating oldGuestRating = _guestRatings.First(guest => guest.Id == guestRating.Id);

        if (oldGuestRating == null)
        {
            return;
        }

        int index = _guestRatings.IndexOf(oldGuestRating);
        _guestRatings[index] = guestRating;
        _serializer.ToCSV(FilePath, _guestRatings);
    }

    public void Delete(int id)
    {
        _guestRatings.RemoveAt(_guestRatings.FindIndex(guestRating => guestRating.Id == id));
        _serializer.ToCSV(FilePath, _guestRatings);
    }

    public GuestRating GetById(int id)
    {
        return _guestRatings.First(guestRating => guestRating.Id == id);
    }

    public List<GuestRating> GetAll()
    {
        return _guestRatings;
    }

    public List<GuestRating> GetGuestRatingsByUserId(int userId)
    {
        return _guestRatings.Where(guestRating => guestRating.GuestId == userId).ToList();
    }

    public GuestRating GetGuestRatingByReservationId(int id)
    {
        return _guestRatings.First(guestRating => guestRating.ReservationId == id);
    }

    public bool ExistsReviewOfGuest(User guest, Accommodation accommodation, bool isChecked = true)
    {
        return _guestRatings.Any(guestRating => guestRating.GuestId == guest.Id && guestRating.AccommodationId == accommodation.Id && guestRating.IsChecked == isChecked);
    }

    public void DeleteAll(Func<GuestRating, bool> value)
    {
        _guestRatings.RemoveAll(value.Invoke);
        _serializer.ToCSV(FilePath, _guestRatings);
    }
}
