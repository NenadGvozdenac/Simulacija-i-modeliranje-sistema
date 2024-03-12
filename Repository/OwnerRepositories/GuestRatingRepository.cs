using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.OwnerRepositories
{
    public class GuestRatingRepository
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
            GuestRating oldGuestRating = _guestRatings.FirstOrDefault(guest => guest.Id == guestRating.Id);

            if(oldGuestRating == null)
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
            return _guestRatings.FirstOrDefault(guestRating => guestRating.Id == id);
        }

        public List<GuestRating> GetAll()
        {
            return _guestRatings;
        }

        public List<GuestRating> GetGuestRatingsByUserId(int userId)
        {
            return _guestRatings.Where(guestRating => guestRating.GuestId == userId).ToList();
        }
    }
}
