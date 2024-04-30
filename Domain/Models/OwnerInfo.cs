using BookingApp.Domain.Miscellaneous;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models;

public class OwnerInfo : ISerializable
{
    public int OwnerId { get; set; }
    public bool IsSuperOwner { get; set; }
    public List<Accommodation> Accommodations { get; set; }
    public List<AccommodationReservation> Reservations { get; set; }
    public int NumberOfAccommodations { get; set; }
    public double AverageReviewScore { get; set; }
    public int NumberOfReviews { get; set; }
    public string PrefferedTheme { get; set; }
    public string PrefferedLanguage { get; set; }

    public OwnerInfo()
    {
        Accommodations = new List<Accommodation>();
        Reservations = new List<AccommodationReservation>();
    }

    public OwnerInfo(int ownerId, bool isSuperOwner, int numberOfAccommodations, double averageReviewScore, int numberOfReviews, string prefferedTheme, string prefferedLanguage)
    {
        OwnerId = ownerId;
        IsSuperOwner = isSuperOwner;
        NumberOfAccommodations = numberOfAccommodations;
        AverageReviewScore = averageReviewScore;
        NumberOfReviews = numberOfReviews;
        PrefferedTheme = prefferedTheme;
        PrefferedLanguage = prefferedLanguage;
    }

    public void FromCSV(string[] values)
    {
        OwnerId = Convert.ToInt32(values[0]);
        IsSuperOwner = Convert.ToBoolean(values[1]);
        NumberOfAccommodations = Convert.ToInt32(values[2]);
        AverageReviewScore = Convert.ToDouble(values[3]);
        NumberOfReviews = Convert.ToInt32(values[4]);
        PrefferedTheme = values[5];
        PrefferedLanguage = values[6];
    }

    public string[] ToCSV()
    {
        return new string[] { OwnerId.ToString(), IsSuperOwner.ToString(), NumberOfAccommodations.ToString(), AverageReviewScore.ToString(), NumberOfReviews.ToString(), PrefferedTheme, PrefferedLanguage };
    }
}
