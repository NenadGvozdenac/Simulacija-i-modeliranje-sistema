using BookingApp.Domain.Models;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTOs.OwnerDTOs;

public class RenovationDTO
{
    private bool _canCancel;
    public string CancelRenovationStatus
    {
        get
        {
            return _canCancel ? "Yes, you can cancel." : "No, you can not cancel.";
        }
    }

    public RenovationStatus Status { get; set; }
    public DateTime LastCancellationDay { get; set; }
    public DateTime RenovationEnd { get; set; }
    public DateTime RenovationStart { get; set; }

    public RenovationDTO(AccommodationRenovation accommodationRenovation)
    {
        _canCancel = accommodationRenovation.DateSpan.Start.AddDays(-5) > DateTime.Now;
        Status = accommodationRenovation.Status;
        LastCancellationDay = accommodationRenovation.DateSpan.Start.AddDays(-5);
        RenovationEnd = accommodationRenovation.DateSpan.End;
        RenovationStart = accommodationRenovation.DateSpan.Start;
    }
}
