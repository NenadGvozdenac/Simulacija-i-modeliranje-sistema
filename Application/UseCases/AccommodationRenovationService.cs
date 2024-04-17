using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class AccommodationRenovationService
{
    private IAccommodationRepository _accommodationRepository;
    private IAccommodationRenovationRepository _accommodationRenovationRepository;

    public AccommodationRenovationService(IAccommodationRepository accommodationRepository
        , IAccommodationRenovationRepository accommodationRenovationRepository)
    {
        _accommodationRepository = accommodationRepository;
        _accommodationRenovationRepository = accommodationRenovationRepository;
    }

    public static AccommodationRenovationService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<AccommodationRenovationService>();
    }

    public void AddAccommodationRenovation(AccommodationRenovation accommodationRenovation)
    {
        _accommodationRenovationRepository.Add(accommodationRenovation);
    }

    public void UpdateAccommodationRenovation(AccommodationRenovation accommodationRenovation)
    {
        _accommodationRenovationRepository.Update(accommodationRenovation);
    }

    public void DeleteAccommodationRenovation(AccommodationRenovation accommodationRenovation)
    {
        _accommodationRenovationRepository.Delete(accommodationRenovation.Id);
    }

    public List<AccommodationRenovation> GetAllAccommodationRenovations()
    {
        return _accommodationRenovationRepository.GetAll();
    }

    public List<AccommodationRenovation> GetAccommodationRenovationsByAccommodationId(int accommodationId)
    {
        return _accommodationRenovationRepository.GetByAccommodationId(accommodationId);
    }

    public List<AccommodationRenovation> GetRenovationsByOwnerId(int id)
    {
        List<int> ownerAccommodations = _accommodationRepository.GetAccommodationsByOwnerId(id).Select(a => a.Id).ToList();
        return _accommodationRenovationRepository.GetAll().Where(r => ownerAccommodations.Contains(r.AccommodationId)).ToList();
    }
}
