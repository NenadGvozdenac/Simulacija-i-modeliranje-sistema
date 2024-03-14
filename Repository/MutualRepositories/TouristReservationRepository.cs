﻿using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using BookingApp.View.GuestViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.MutualRepositories
{
    public class TouristReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.csv";

        private readonly Serializer<TouristReservation> _serializer;

        private List<TouristReservation> _reservations;

        public TouristReservationRepository()
        {
            _serializer = new Serializer<TouristReservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }

        public List<TouristReservation> GetAll()
        {
            return _reservations;
        }

        public TouristReservation GetById(int id)
        {
            return _reservations.FirstOrDefault(a => a.Id == id);
        }

        public TouristReservation GetByTouristId(int id)
        {
            _reservations = _serializer.FromCSV(FilePath);
            return _reservations.FirstOrDefault(a => a.Id_Tourist == id);
        }

        public void Add(TouristReservation reservation)
        {
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
        }

        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(c => c.Id) + 1;
        }
        public void Update(TouristReservation reservation)
        {
            var existingTourist = _reservations.FirstOrDefault(t => t.Id == reservation.Id);
            if (existingTourist != null)
            {
                existingTourist.Id = reservation.Id;
                existingTourist.Id_Tour = reservation.Id_Tour;
                existingTourist.Id_Tourist = reservation.Id_Tourist;

                _serializer.ToCSV(FilePath, _reservations);
            }
        }

        public void Delete(int id)
        {
            var existingReservation = _reservations.FirstOrDefault(t => t.Id == id);
            if (existingReservation != null)
            {
                _reservations.Remove(existingReservation);
                _serializer.ToCSV(FilePath, _reservations);
            }
        }

    }
}