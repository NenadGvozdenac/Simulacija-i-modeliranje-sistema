using BookingApp.Domain.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models;

public class GuestInfo : ISerializable
{
    public int Id { get; set; }
    public int GuestId { get; set; }
    public bool IsSuperGuest { get; set; }
    public int NumberOfReservationsThisYear { get; set; }
    public DateOnly DateOfBecomingSuperGuest { get; set; }
    public int NumberOfPoints { get; set; }

    public GuestInfo()
    {
    }

    public GuestInfo(int id, int guestId, bool isSuperGuest, int numberOfReservationsThisYear, DateOnly dateOfBecomingSuperGuest, int numberOfPoints)
    {
        Id = id;
        GuestId = guestId;
        IsSuperGuest = isSuperGuest;
        NumberOfReservationsThisYear = numberOfReservationsThisYear;
        DateOfBecomingSuperGuest = dateOfBecomingSuperGuest;
        NumberOfPoints = numberOfPoints;
    }

    public void FromCSV(string[] values)
    {
        Id = Convert.ToInt32(values[0]);
        GuestId = Convert.ToInt32(values[1]);
        IsSuperGuest = Convert.ToBoolean(values[2]);
        NumberOfReservationsThisYear = Convert.ToInt32(values[3]);
        DateOfBecomingSuperGuest = DateOnly.Parse(values[4]);
        NumberOfPoints = Convert.ToInt32(values[5]);
    }

    public string[] ToCSV()
    {
        return new string[] { Id.ToString(), GuestId.ToString(), IsSuperGuest.ToString(), NumberOfReservationsThisYear.ToString(), DateOfBecomingSuperGuest.ToString(), NumberOfPoints.ToString() };
    }
}
