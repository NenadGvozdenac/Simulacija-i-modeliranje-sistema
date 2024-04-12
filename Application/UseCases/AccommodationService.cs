﻿using BookingApp.WPF.ViewModels.OwnerViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using BookingApp.Domain.Models;
using BookingApp.Repositories;

namespace BookingApp.Application.UseCases;

public class AccommodationService
{
    private AccommodationRepository _accommodationRepository;
    private LocationRepository _locationRepository;
    private AccommodationImageRepository _accommodationImageRepository;
    private OwnerService _ownerService;

    public AccommodationService()
    {
        _accommodationRepository = AccommodationRepository.GetInstance();
        _locationRepository = LocationRepository.GetInstance();
        _accommodationImageRepository = AccommodationImageRepository.GetInstance();
        _ownerService = OwnerService.GetInstance();
    }

    public static AccommodationService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<AccommodationService>();
    }

    public List<Accommodation> GetAll()
    {
        List<Accommodation> accommodations = _accommodationRepository.GetAll();
        accommodations.ForEach(accommodation => accommodation.Location = _locationRepository.GetById(accommodation.LocationId));
        accommodations.ForEach(accommodation => accommodation.Images = _accommodationImageRepository.GetByAccommodationId(accommodation.Id));
        return accommodations;
    }

    public Accommodation GetById(int id)
    {
        Accommodation accommodation = _accommodationRepository.GetById(id);
        accommodation.Location = _locationRepository.GetById(accommodation.LocationId);

        return accommodation;
    }

    public List<Accommodation> GetByOwnerId(int ownerId)
    {
        List<Accommodation> accommodations = _accommodationRepository.GetAccommodationsByOwnerId(ownerId);
        accommodations.ForEach(accommodation => accommodation.Location = _locationRepository.GetById(accommodation.LocationId));
        accommodations.ForEach(accommodation => accommodation.Images = _accommodationImageRepository.GetByAccommodationId(accommodation.Id));
        return accommodations;
    }

    public void Add(Accommodation accommodation)
    {
        _accommodationRepository.Add(accommodation);
        accommodation.Images.ForEach(image => image.AccommodationId = accommodation.Id);
        _accommodationImageRepository.AddAll(accommodation.Images);
    }

    public void Update(Accommodation accommodation)
    {
        _accommodationRepository.Update(accommodation);
    }

    public void Delete(Accommodation entity)
    {
        _accommodationImageRepository.DeleteByAccommodationId(entity.Id);
        _accommodationRepository.Delete(entity.Id);
    }

    public List<Accommodation> GetAllAccommodationsFromSuperOwners()
    {
        List<int> superOwners = _ownerService.GetSuperOwners().Select(owner => owner.Id).ToList();
        return GetAll().Where(accommodation => superOwners.Contains(accommodation.OwnerId)).ToList();
    }

    public List<Accommodation> GetAllAcommodationNotFromSuperOwners()
    {
        return GetAll().Except(GetAllAccommodationsFromSuperOwners()).ToList();
    }
}