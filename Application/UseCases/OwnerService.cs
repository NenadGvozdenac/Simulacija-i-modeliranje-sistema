using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class OwnerService
{
    private IOwnerInfoRepository _ownerInfoRepository;
    private IUserRepository _userRepository;
    public OwnerService(IOwnerInfoRepository ownerInfoRepository, IUserRepository userRepository)
    {
        _ownerInfoRepository = ownerInfoRepository;
        _userRepository = userRepository;
    }

    public static OwnerService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<OwnerService>();
    }

    public (OwnerInfo, User) GetOwnerInfo(int ownerId)
    {
        return (_ownerInfoRepository.GetById(ownerId), _userRepository.GetById(ownerId));
    }

    public void UpdateOwnerInfo(OwnerInfo ownerInfo)
    {
        _ownerInfoRepository.Update(ownerInfo);
    }

    public void AddOwner(User user)
    {
        OwnerInfo ownerInfo = new(user.Id, false, 0, 0, 0);
        _ownerInfoRepository.Add(ownerInfo);
        _userRepository.Add(user);
    }

    public void DeleteOwner(int ownerId)
    {
        _ownerInfoRepository.Delete(ownerId);
        _userRepository.Delete(ownerId);
    }

    public void UpdateOwner(OwnerInfo ownerInfo, User user)
    {
        _ownerInfoRepository.Update(ownerInfo);
        _userRepository.Update(user);
    }

    public void Add((OwnerInfo, User) entity)
    {
        _ownerInfoRepository.Add(entity.Item1);
        _userRepository.Add(entity.Item2);
    }

    public void Add(User user)
    {
        OwnerInfo ownerInfo = new(user.Id, false, 0, 0, 0);
        _ownerInfoRepository.Add(ownerInfo);
    }

    public void Delete((OwnerInfo, User) entity)
    {
        _ownerInfoRepository.Delete(entity.Item2.Id);
        _userRepository.Delete(entity.Item2.Id);
    }

    public List<(OwnerInfo, User)> GetAll()
    {
        List<(OwnerInfo, User)> owners = new();
        List<OwnerInfo> ownerInfos = _ownerInfoRepository.GetAll();
        ownerInfos.ForEach(ownerInfo => owners.Add((ownerInfo, _userRepository.GetById(ownerInfo.OwnerId))));

        return owners;
    }

    public (OwnerInfo, User) GetById(int entityId)
    {
        OwnerInfo ownerInfo = _ownerInfoRepository.GetById(entityId);
        User user = _userRepository.GetById(entityId);
        return (ownerInfo, user);
    }

    public void Update((OwnerInfo, User) entity)
    {
        _ownerInfoRepository.Update(entity.Item1);
        _userRepository.Update(entity.Item2);
    }

    public void CheckForSuperOwner(User user)
    {
        (OwnerInfo, User) ownerInfo = GetInstance().GetOwnerInfo(user.Id);

        // TODO: Get all reviews of the owner
        List<AccommodationReview> accommodationReviews = AccommodationReviewService.GetInstance().GetByOwnerId(ownerInfo.Item1.OwnerId);

        // Put number of reviews in the owner info
        ownerInfo.Item1.NumberOfReviews = accommodationReviews.Count;

        // Put accommodations of the owner in the owner info
        ownerInfo.Item1.Accommodations = AccommodationService.GetInstance().GetByOwnerId(ownerInfo.Item1.OwnerId);

        // Put number of accommodations in the owner info
        ownerInfo.Item1.NumberOfAccommodations = ownerInfo.Item1.Accommodations.Count;

        // TODO: Get average review score of the owner
        ownerInfo.Item1.AverageReviewScore = AccommodationReviewService.GetInstance().GetAverageReviewScore(ownerInfo.Item1.OwnerId);

        // If user is reviewed by at least 50 guests, and has average rating above 4.5, make him a super owner
        UpdateOwnerInfo(new OwnerInfo()
        {
            AverageReviewScore = ownerInfo.Item1.AverageReviewScore,
            NumberOfReviews = ownerInfo.Item1.NumberOfReviews,
            NumberOfAccommodations = ownerInfo.Item1.NumberOfAccommodations,
            OwnerId = ownerInfo.Item1.OwnerId,
            IsSuperOwner = ownerInfo.Item1.NumberOfReviews >= 50 && ownerInfo.Item1.AverageReviewScore >= 4.5
        });
    }

    public List<User> GetSuperOwners()
    {
       return GetAll().Where(items => items.Item1.IsSuperOwner).Select(owner => owner.Item2).ToList();
    }
}
