using BookingApp.Application.Localization;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTOs.OwnerDTOs;

public class AccommodationDTO
{
    public User Owner { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; }
    private string _type;
    public string Type
    {
        get => TranslationSource.Instance[_type];
        set => _type = value;
    }
    private string _maxGuestNumber;
    public string MaxGuestNumber
    {
        get => string.Format(_maxGuestNumber == "1" ? "{0} {1}" : "{0} {1}", _maxGuestNumber, TranslationSource.Instance["GuestsLC"]);
        set => _maxGuestNumber = value;
    }

    private string _minReservationDays;
    public string MinReservationDays
    {
        get => string.Format(_minReservationDays == "1" ? "{0} {1}" : "{0} {1}", _minReservationDays, TranslationSource.Instance["DaysLC"]);
        set => _minReservationDays = value;
    }

    private string _cancellationPeriodDays;
    public string CancellationPeriodDays
    {
        get => string.Format(_cancellationPeriodDays == "1" ? "{0} {1" : "{0} {1}", _cancellationPeriodDays, TranslationSource.Instance["DaysLC"]);
        set => _cancellationPeriodDays = value;
    }

    private string _averageReviewScore;
    public string AverageReviewScore
    {
        get => string.Format("{0} / 5.00", _averageReviewScore);
        set => _averageReviewScore = value;
    }

    private string _price;
    public string Price
    {
        get => string.Format("{0} $", _price);
        set => _price = value;
    }

    private string _numberOfReviews;
    public string NumberOfReviews
    {
        get => string.Format(_numberOfReviews == "1" ? "{0} {1}" : "{0} {1}", _numberOfReviews, TranslationSource.Instance["ReviewsLC"]);
        set => _numberOfReviews = value;
    }

    public List<AccommodationImage> Images { get; set; }

    public AccommodationDTO(Accommodation accommodation)
    {
        Owner = OwnerService.GetInstance().GetById(accommodation.OwnerId).Item2;
        Name = accommodation.Name;
        Location = LocationService.GetInstance().GetById(accommodation.LocationId);
        Type = accommodation.Type.ToString();
        MaxGuestNumber = accommodation.MaxGuestNumber.ToString();
        MinReservationDays = accommodation.MinReservationDays.ToString();
        CancellationPeriodDays = accommodation.CancellationPeriodDays.ToString();
        AverageReviewScore = AccommodationReviewService.GetInstance().GetAverageReviewScoreByAccommodationId(accommodation.Id).ToString("0.00");
        Price = accommodation.Price.ToString();
        NumberOfReviews = AccommodationReviewService.GetInstance().GetByAccommodationId(accommodation.Id).Count.ToString();
        Images = accommodation.Images;
    }
}
