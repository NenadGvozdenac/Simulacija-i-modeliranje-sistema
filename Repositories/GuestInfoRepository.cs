using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class GuestInfoRepository : BaseRepository<GuestInfo>, IGuestInfoRepository
{
    public GuestInfoRepository() : base("../../../Resources/Data/guest_infos.csv", "Id") { }

    public GuestInfo GetByGuestId(int guestId)
    {
        return GetAll().Find(guestInfo => guestInfo.GuestId == guestId);
    }

    public GuestInfo NewYearCheck(GuestInfo guestInfo)
    {
        if (guestInfo.DateOfBecomingSuperGuest.Year != DateOnly.FromDateTime(DateTime.UtcNow).Year)
        {
            guestInfo.NumberOfReservationsThisYear = 0;
            Update(guestInfo);
        }

        return guestInfo;
    }

    public GuestInfo NumberOfReservationsThisYearUpdate(GuestInfo guestinfo)
    {
        List<AccommodationReservation> reservations = AccommodationReservationService.GetInstance().GetAll().FindAll(reservation => reservation.UserId == guestinfo.GuestId);

        reservations = reservations.FindAll(reservation => reservation.LastDateOfStaying.Year == DateOnly.FromDateTime(DateTime.UtcNow).Year && reservation.LastDateOfStaying < DateTime.Now);

        guestinfo.NumberOfReservationsThisYear = reservations.Count;
        Update(guestinfo);

        return guestinfo;
    }

    public void SuperGuestCheck(GuestInfo guestInfo)
    {
        guestInfo = NewYearCheck(guestInfo);
        guestInfo = NumberOfReservationsThisYearUpdate(guestInfo);

        if (guestInfo.IsSuperGuest)
        {
            if (guestInfo.DateOfBecomingSuperGuest.AddYears(1) < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                if (guestInfo.NumberOfReservationsThisYear >= 10)
                {
                    guestInfo.IsSuperGuest = true;
                    guestInfo.NumberOfPoints = 5;
                    guestInfo.DateOfBecomingSuperGuest = DateOnly.FromDateTime(DateTime.UtcNow);
                    Update(guestInfo);
                }
                else
                {
                    guestInfo.IsSuperGuest = false;
                    guestInfo.NumberOfPoints = 0;
                    guestInfo.DateOfBecomingSuperGuest = DateOnly.FromDateTime(DateTime.UtcNow);
                    Update(guestInfo);
                }
            }
        }
        else if(guestInfo.NumberOfReservationsThisYear >= 10)
        {
            guestInfo.IsSuperGuest = true;
            guestInfo.NumberOfPoints = 5;
            guestInfo.DateOfBecomingSuperGuest = DateOnly.FromDateTime(DateTime.UtcNow);
            Update(guestInfo);
        }
    }
}
