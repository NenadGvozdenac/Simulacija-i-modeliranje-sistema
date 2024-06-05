using BookingApp.Application.Localization;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookingApp.WPF.DTOs.OwnerDTOs;

public class AccommodationReservationMovingDTO
{
    public AccommodationReservationMoving AccommodationReservationMoving { get; set; }
    public int DaysOfReservation { get; set; }
    public string Comment { get; set; }
    public string StatusOfWantedTimespan { get; set; }
    private DateTime _dayBeforeCancellationIsFinal;
    public string DayBeforeCancellationIsFinal {
        get => DateParser.ToString(_dayBeforeCancellationIsFinal); 
    }

    private double _price;
    public string Price
    {
        get => _price.ToString() + " $";
    }

    private Brush _statusColor;

    public AccommodationReservationMovingDTO(AccommodationReservationMoving accommodationReservationMoving)
    {
        AccommodationReservationMoving = accommodationReservationMoving;
        DaysOfReservation = (accommodationReservationMoving.CurrentReservationTimespan.End - accommodationReservationMoving.CurrentReservationTimespan.Start).Days;
        _dayBeforeCancellationIsFinal = accommodationReservationMoving.CurrentReservationTimespan.Start.AddDays(-accommodationReservationMoving.Accommodation.CancellationPeriodDays);
        _price = accommodationReservationMoving.Accommodation.Price;
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

        if (AccommodationReservationService.GetInstance().IsTimespanFree(wantedSpan, accommodation, AccommodationReservationMoving))
        {
            StatusOfWantedTimespan = TranslationSource.Instance["StatusCanBeMoved"];
            _statusColor = Brushes.Green;
        }
        else
        {
            StatusOfWantedTimespan = TranslationSource.Instance["StatusCannotBeMoved"];
            _statusColor = Brushes.Red;
        }
    }
}
