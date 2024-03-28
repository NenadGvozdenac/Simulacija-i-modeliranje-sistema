using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs.OwnerDTOs;

public class AccommodationDTO
{
    public User Owner { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; }
    public AccommodationType Type { get; set; }
    private string _maxGuestNumber;
    public string MaxGuestNumber
    {
        get => string.Format(_maxGuestNumber == "1" ? "{0} guest" : "{0} guests", _maxGuestNumber);
        set => _maxGuestNumber = value;
    }

    private string _minReservationDays;
    public string MinReservationDays
    {
        get => string.Format(_minReservationDays == "1" ? "{0} day" : "{0} days", _minReservationDays);
        set => _minReservationDays = value;
    }

    private string _cancellationPeriodDays;
    public string CancellationPeriodDays
    {
        get => string.Format(_cancellationPeriodDays == "1" ? "{0} day" : "{0} days", _cancellationPeriodDays);
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
        get => string.Format(_numberOfReviews == "1" ? "{0} review" : "{0} reviews", _numberOfReviews);
        set => _numberOfReviews = value;
    }

    public List<AccommodationImage> Images { get; set; }

    public AccommodationDTO(Accommodation accommodation)
    {
        Owner = UserRepository.GetInstance().GetById(accommodation.OwnerId);
        Name = accommodation.Name;
        Location = accommodation.Location;
        Type = accommodation.Type;
        MaxGuestNumber = accommodation.MaxGuestNumber.ToString();
        MinReservationDays = accommodation.MinReservationDays.ToString();
        CancellationPeriodDays = accommodation.CancellationPeriodDays.ToString();
        AverageReviewScore = accommodation.AverageReviewScore.ToString();
        Price = accommodation.Price.ToString();
        NumberOfReviews = "0";        // TODO: Change when reviews are implemented
        Images = accommodation.Images;
    }
}
