﻿using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.ViewModel.OwnerViewModels;
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

namespace BookingApp.Services.Owner;

public class AccommodationService : IService<Accommodation>
{
    private AccommodationRepository _accommodationRepository;
    private LocationRepository _locationRepository;
    private AccommodationImageRepository _accommodationImageRepository;

    public AccommodationService()
    {
        _accommodationRepository        = AccommodationRepository.GetInstance();
        _locationRepository = LocationRepository.GetInstance();
        _accommodationImageRepository   = AccommodationImageRepository.GetInstance();
    }

    public static AccommodationService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<AccommodationService>();
    }

    public List<Accommodation> GetAll()
    {
        List<Accommodation> accommodations = _accommodationRepository.GetAll();
        accommodations.ForEach(accommodation => accommodation.Location = _locationRepository.GetById(accommodation.LocationId));
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
        _accommodationRepository.Delete(entity.Id);
    }
}
