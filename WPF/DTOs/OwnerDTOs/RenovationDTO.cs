using BookingApp.Application.Localization;
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
            return _canCancel ? TranslationSource.Instance["YouCanCancel"] : TranslationSource.Instance["YouCantCancel"];
        }
    }

    private RenovationStatus _status;
    public string Status
    {
        get
        {
            return _status switch
            {
                RenovationStatus.Upcoming => TranslationSource.Instance["Upcoming"],
                RenovationStatus.Ongoing => TranslationSource.Instance["Ongoing"],
                RenovationStatus.Cancelled => TranslationSource.Instance["Cancelled"],
                RenovationStatus.Finished => TranslationSource.Instance["Finished"],
                _ => TranslationSource.Instance["Unknown"]
            };
        }
    }
    public DateTime LastCancellationDay { get; set; }
    public DateTime RenovationEnd { get; set; }
    public DateTime RenovationStart { get; set; }

    public RenovationDTO(AccommodationRenovation accommodationRenovation)
    {
        _canCancel = accommodationRenovation.DateSpan.Start.AddDays(-5) > DateTime.Now;
        _status = accommodationRenovation.Status;
        LastCancellationDay = accommodationRenovation.DateSpan.Start.AddDays(-5);
        RenovationEnd = accommodationRenovation.DateSpan.End;
        RenovationStart = accommodationRenovation.DateSpan.Start;
    }
}
