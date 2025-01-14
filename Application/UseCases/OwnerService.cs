﻿using BookingApp.Domain.Models;
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

    public void UpdateOwnerInfo(OwnerInfo ownerInfo)
    {
        _ownerInfoRepository.Update(ownerInfo);
    }

    public void AddOwner(User user)
    {
        OwnerInfo ownerInfo = new(user.Id, false, 0, 0, 0, "Light", "English");
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
        OwnerInfo ownerInfo = new(user.Id, false, 0, 0, 0, "Light", "English");
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
        (OwnerInfo, User) ownerInfo = GetById(user.Id);

        List<AccommodationReview> accommodationReviews = AccommodationReviewService.GetInstance().GetByOwnerId(ownerInfo.Item1.OwnerId);

        ownerInfo.Item1.NumberOfReviews = accommodationReviews.Count;

        ownerInfo.Item1.Accommodations = AccommodationService.GetInstance().GetByOwnerId(ownerInfo.Item1.OwnerId);

        ownerInfo.Item1.NumberOfAccommodations = ownerInfo.Item1.Accommodations.Count;

        ownerInfo.Item1.AverageReviewScore = AccommodationReviewService.GetInstance().GetAverageReviewScore(ownerInfo.Item1.OwnerId);

        // If user is reviewed by at least 50 guests, and has average rating above 4.5, make him a super owner
        UpdateOwnerInfo(new OwnerInfo() {
            OwnerId = ownerInfo.Item1.OwnerId,
            AverageReviewScore = ownerInfo.Item1.AverageReviewScore,
            NumberOfReviews = ownerInfo.Item1.NumberOfReviews,
            NumberOfAccommodations = ownerInfo.Item1.NumberOfAccommodations,
            IsSuperOwner = ownerInfo.Item1.NumberOfReviews >= 50 && ownerInfo.Item1.AverageReviewScore > 4.5,
            PrefferedLanguage = ownerInfo.Item1.PrefferedLanguage,
            PrefferedTheme = ownerInfo.Item1.PrefferedTheme
        });
    }

    public List<User> GetSuperOwners()
    {
       return GetAll().Where(items => items.Item1.IsSuperOwner).Select(owner => owner.Item2).ToList();
    }

    public List<Notification> GetNotificationsForOwner(int id)
    {
        List<Notification> notifications = new();

        notifications.AddRange(GetUnreviewedReviewForOwner(id));
        notifications.AddRange(GetNewlyOpenForumsForOwner(id));

        return notifications;
    }

    private List<Notification<Forum>> GetNewlyOpenForumsForOwner(int id)
    {
        return ForumService.GetInstance().GetForumsByOwnerId(id)
            .Where(forum => forum.ForumStatus == Resources.Types.ForumStatus.Open)
            .Select(forum => new Notification<Forum>("Forum Notification", "A new forum opened up!", forum.Location.ToString(), forum.CreationDate, forum))
            .ToList();
    }

    private List<Notification<GuestRating>> GetUnreviewedReviewForOwner(int id)
    {
        List<Accommodation> accommodations = AccommodationService.GetInstance().GetByOwnerId(id);

        return accommodations
            .SelectMany(accommodation => GuestRatingService.GetInstance().GetByAccommodationId(accommodation.Id))
            .Distinct()
            .Where(review => !review.IsChecked)
            .Where(review => GuestRatingService.GetInstance().GetDaysRemainingToReview(review.Id) <= 5 && GuestRatingService.GetInstance().GetDaysRemainingToReview(review.Id) > 0)
            .Select(review => {
                User user = UserService.GetInstance().GetById(review.GuestId);
                int daysRemainingToReview = GuestRatingService.GetInstance().GetDaysRemainingToReview(review.Id);
                return new Notification<GuestRating>("Review Notification", $"{user.Username} can be reviewed!", $"Time remaining: {daysRemainingToReview} days", review);
            }).ToList();
    }
}
