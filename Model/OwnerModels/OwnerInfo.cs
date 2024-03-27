﻿using BookingApp.Model.MutualModels;
using BookingApp.Resources.Types;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model.OwnerModels;

public class OwnerInfo : ISerializable
{
    public int OwnerId { get; set; }
    public bool IsSuperOwner { get; set; }
    public List<Accommodation> Accommodations { get; set; }
    public List<AccommodationReservation> Reservations { get; set; }
    public int NumberOfAccommodations { get; set; }
    public int AverageReviewScore { get; set; }
    public int NumberOfReviews { get; set; }

    public OwnerInfo()
    {
        Accommodations = new List<Accommodation>();
        Reservations = new List<AccommodationReservation>();
    }

    public OwnerInfo(int ownerId, bool isSuperOwner, List<Accommodation> accommodations, List<AccommodationReservation> reservations, int numberOfAccommodations, int averageReviewScore, int numberOfReviews)
    {
        OwnerId = ownerId;
        IsSuperOwner = isSuperOwner;
        Accommodations = accommodations;
        Reservations = reservations;
        NumberOfAccommodations = numberOfAccommodations;
        AverageReviewScore = averageReviewScore;
        NumberOfReviews = numberOfReviews;
    }

    public OwnerInfo(int ownerId, bool isSuperOwner, int numberOfAccommodations, int averageReviewScore, int numberOfReviews)
    {
        OwnerId = ownerId;
        IsSuperOwner = isSuperOwner;
        NumberOfAccommodations = numberOfAccommodations;
        AverageReviewScore = averageReviewScore;
        NumberOfReviews = numberOfReviews;
    }

    public void FromCSV(string[] values)
    {
        OwnerId = Convert.ToInt32(values[0]);
        IsSuperOwner = Convert.ToBoolean(values[1]);
        NumberOfAccommodations = Convert.ToInt32(values[2]);
        AverageReviewScore = Convert.ToInt32(values[3]);
        NumberOfReviews = Convert.ToInt32(values[4]);
    }

    public string[] ToCSV()
    {
        return new string[] { OwnerId.ToString(), IsSuperOwner.ToString(), NumberOfAccommodations.ToString(), AverageReviewScore.ToString(), NumberOfReviews.ToString() };
    }
}
