﻿using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class GuestService
{
    private IGuestInfoRepository _guestInfoRepository;
    private IUserRepository _userRepository;
    public GuestService(IGuestInfoRepository guestInfoRepository, IUserRepository userRepository)
    {
        _guestInfoRepository = guestInfoRepository;
        _userRepository = userRepository;
    }

    public static GuestService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<GuestService>();
    }

    public void UpdateGuestInfo(GuestInfo guestInfo)
    {
        _guestInfoRepository.Update(guestInfo);
    }

    public void AddGuest(User user)
    {
        GuestInfo guestInfo = new(-1, user.Id, false, 0, DateOnly.MinValue, 0);
        _guestInfoRepository.Add(guestInfo);
        _userRepository.Add(user);
    }

    public void DeleteGuest(int guestId)
    {
        _guestInfoRepository.Delete(guestId);
        _userRepository.Delete(guestId);
    }

    public void UpdateGuest(GuestInfo guestInfo, User user)
    {
        _guestInfoRepository.Update(guestInfo);
        _userRepository.Update(user);
    }

    public void Add((GuestInfo, User) entity)
    {
        _guestInfoRepository.Add(entity.Item1);
        _userRepository.Add(entity.Item2);
    }

    public void Add(User user)
    {
        GuestInfo guestInfo = new(-1, user.Id, false, 0, DateOnly.FromDateTime(DateTime.UtcNow), 0);
        _guestInfoRepository.Add(guestInfo);
    }

    public void Delete((GuestInfo, User) entity)
    {
        _guestInfoRepository.Delete(entity.Item1.GuestId);
        _userRepository.Delete(entity.Item2.Id);
    }
    
    public GuestInfo GetByGuestId(int guestId)
    {
        return _guestInfoRepository.GetByGuestId(guestId);
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
                    UpdateGuestInfo(guestInfo);
                }
                else
                {
                    guestInfo.IsSuperGuest = false;
                    guestInfo.NumberOfPoints = 0;
                    guestInfo.DateOfBecomingSuperGuest = DateOnly.FromDateTime(DateTime.UtcNow);
                    UpdateGuestInfo(guestInfo);
                }
            }
        }
        else if(guestInfo.NumberOfReservationsThisYear >= 10)
        {
            guestInfo.IsSuperGuest = true;
            guestInfo.NumberOfPoints = 5;
            guestInfo.DateOfBecomingSuperGuest = DateOnly.FromDateTime(DateTime.UtcNow);
            UpdateGuestInfo(guestInfo);
        }
    
    }
    public GuestInfo NewYearCheck(GuestInfo guestInfo)
    {
        if (guestInfo.DateOfBecomingSuperGuest.Year != DateOnly.FromDateTime(DateTime.UtcNow).Year)
        {
            guestInfo.NumberOfReservationsThisYear = 0;
            UpdateGuestInfo(guestInfo);
        }

        return guestInfo;
    }

    public GuestInfo NumberOfReservationsThisYearUpdate(GuestInfo guestinfo)
    {
        List<AccommodationReservation> reservations = AccommodationReservationService.GetInstance().GetAll().FindAll(reservation => reservation.UserId == guestinfo.GuestId);

        reservations = reservations.FindAll(reservation => reservation.LastDateOfStaying.Year == DateOnly.FromDateTime(DateTime.UtcNow).Year && reservation.LastDateOfStaying < DateTime.Now);

        guestinfo.NumberOfReservationsThisYear = reservations.Count;
        UpdateGuestInfo(guestinfo);

        return guestinfo;
    }
}
