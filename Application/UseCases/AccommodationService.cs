using BookingApp.WPF.ViewModels.OwnerViewModels;
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
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Application.UseCases;

public class AccommodationService
{
    private IAccommodationRepository _accommodationRepository;
    private ILocationRepository _locationRepository;
    private IAccommodationImageRepository _accommodationImageRepository;
    private IAccommodationReservationRepository _accommodationReservationRepository;
    private OwnerService _ownerService;

    public AccommodationService(IAccommodationRepository accommodationRepository, 
        ILocationRepository locationRepository, 
        IAccommodationImageRepository accommodationImageRepository,
        IAccommodationReservationRepository accommodationReservationRepository)
    {
        _ownerService = OwnerService.GetInstance();
        _accommodationRepository = accommodationRepository;
        _locationRepository = locationRepository;
        _accommodationImageRepository = accommodationImageRepository;
        _accommodationReservationRepository = accommodationReservationRepository;
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
        accommodations.ForEach(accommodation => accommodation.Reservations = _accommodationReservationRepository.GetByAccommodationId(accommodation.Id));
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

    public Location GetMostPopularLocation()
    {
        return GetAll().GroupBy(accommodation => accommodation.LocationId)
            .OrderByDescending(group => group.Count())
            .Select(group => group.First().Location)
            .First();
    }
    public Location GetLeastPopularLocation()
    {
        return GetAll().GroupBy(accommodation => accommodation.LocationId)
            .OrderBy(group => group.Count())
            .Select(group => group.First().Location)
            .First();
    }

    public int GetLocationReservations(Location popularLocation)
    {
        return GetAll().Where(accommodation => accommodation.LocationId == popularLocation.Id).Count();
    }

    public double GetLocationFullness(Location Location, User ownerId)
    {
        List<Accommodation> accommodations = GetAll().Where(accommodation => accommodation.OwnerId == ownerId.Id && accommodation.LocationId == Location.Id).ToList();

        if (accommodations.Count == 0)
        {
            return 0;
        }

        int totalReservations = accommodations.Sum(accommodation => accommodation.ReservationNumber);

        if (totalReservations == 0) return 0;

        int daysFull = accommodations.Sum(accommodation => accommodation.Reservations.Sum(reservation => (reservation.LastDateOfStaying - reservation.FirstDateOfStaying).Days));

        return (double)daysFull / totalReservations;
    }

    public List<Accommodation> GetAccommodationsByLocation(Location leastPopularLocation, int userId)
    {
        return GetAll().Where(accommodation => accommodation.LocationId == leastPopularLocation.Id && accommodation.OwnerId == userId).ToList();
    }

    public List<Accommodation> GetAccommodationsByLocationId(int locationid)
    {
        return GetAll().Where(accommodation => accommodation.LocationId == locationid).ToList();
    }
}