using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.MutualRepositories
{
    public class AccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation_reservations.csv";

        private readonly Serializer<AccommodationReservation> _serializer;

        private List<AccommodationReservation> _accommodationreservation;

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _accommodationreservation = _serializer.FromCSV(FilePath);
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationreservation;
        }

        public AccommodationReservation GetById(int id)
        {
            return _accommodationreservation.FirstOrDefault(a => a.Id == id);
        }

        public int NextId()
        {
            _accommodationreservation = _serializer.FromCSV(FilePath);
            if (_accommodationreservation.Count < 1)
            {
                return 1;
            }
            return _accommodationreservation.Max(c => c.Id) + 1;
        }

        public void Add(AccommodationReservation accommodationres)
        {
            accommodationres.Id = NextId();
            _accommodationreservation.Add(accommodationres);
            _serializer.ToCSV(FilePath, _accommodationreservation);
        }

        public void Update(AccommodationReservation accommodationres)
        {
            var existingAccommodationRes = _accommodationreservation.FirstOrDefault(a => a.Id == accommodationres.Id);
            if (existingAccommodationRes != null)
            {
                existingAccommodationRes.AccommodationId = accommodationres.AccommodationId;
                existingAccommodationRes.UserId = accommodationres.UserId;
                existingAccommodationRes.FirstDateOfStaying = accommodationres.FirstDateOfStaying;
                existingAccommodationRes.LastDateOfStaying = accommodationres.LastDateOfStaying;

                _serializer.ToCSV(FilePath, _accommodationreservation);
            }
        }

        public void Delete(int id)
        {
            var existingAccommodationRes = _accommodationreservation.FirstOrDefault(a => a.Id == id);
            if (existingAccommodationRes != null)
            {
                _accommodationreservation.Remove(existingAccommodationRes);
                _serializer.ToCSV(FilePath, _accommodationreservation);
            }
        }

        public List<DateTime> FindTakenDates(int id)
        {
            List<DateTime> result = new List<DateTime>();

            foreach(AccommodationReservation res in GetAll())
            {
                if (res.Id == id)
                {
                    DateTime tempdate = res.FirstDateOfStaying;

                    while (tempdate != res.LastDateOfStaying.AddDays(1))
                    {
                        result.Add(tempdate);
                        tempdate = tempdate.AddDays(1);
                    }
                }
            }
            
            return result;
        }
    }
}
