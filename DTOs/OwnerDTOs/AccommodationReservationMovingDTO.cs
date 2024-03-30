﻿using BookingApp.Miscellaneous;
using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.ViewModel.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookingApp.DTOs.OwnerDTOs;

public class AccommodationReservationMovingDTO
{
    public AccommodationReservationMoving AccommodationReservationMoving { get; set; }
    public int DaysOfReservation { get; set; }
    public string Comment { get; set; }
    public string StatusOfWantedTimespan { get; set; }
    public DateTime DayBeforeCancellationIsFinal { get; set; }

    private Brush _statusColor;

    public AccommodationReservationMovingDTO(AccommodationReservationMoving accommodationReservationMoving)
    {
        this.AccommodationReservationMoving = accommodationReservationMoving;
        this.DaysOfReservation = (accommodationReservationMoving.CurrentReservationTimespan.End - accommodationReservationMoving.CurrentReservationTimespan.Start).Days;
        this.DayBeforeCancellationIsFinal = accommodationReservationMoving.CurrentReservationTimespan.Start.AddDays(-accommodationReservationMoving.Accommodation.CancellationPeriodDays);
        CalculateStatus();
    }

    public Brush GetLabelColor()
    {
        return _statusColor;
    }

    private void CalculateStatus()
    {
        DateSpan wantedSpan = AccommodationReservationMoving.WantedReservationTimespan;
        Accommodation accommodation = AccommodationReservationMoving.Accommodation;

        if (AccommodationReservationRepository.GetInstance().IsTimespanFree(wantedSpan, accommodation, AccommodationReservationMoving))
        {
            StatusOfWantedTimespan = "Reservation can be moved to wanted timespan";
            _statusColor = Brushes.Green;
        }
        else
        {
            StatusOfWantedTimespan = "Reservation timespan is already reserved.";
            _statusColor = Brushes.Red;
        }
    }
}