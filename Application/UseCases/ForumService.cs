using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class ForumService
{
    public IForumRepository _forumRepository;
    public IForumCommentRepository _forumCommentRepository;
    public ILocationRepository _locationRepository;

    public ForumService(IForumRepository forumRepository, 
        IForumCommentRepository forumCommentRepository,
        ILocationRepository locationRepository)
    {
        _forumRepository = forumRepository;
        _forumCommentRepository = forumCommentRepository;
        _locationRepository = locationRepository;

    }

    public static ForumService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<ForumService>();
    }

    public Forum GetById(int id)
    {
        Forum forum = _forumRepository.GetById(id);
        forum.Comments = _forumCommentRepository.GetByForumId(forum.Id);
        forum.Location = _locationRepository.GetById(forum.LocationId);
        return forum;
    }

    public List<Forum> GetAll()
    {
        List<Forum> forums = _forumRepository.GetAll();
        forums.ForEach(forum => forum.Comments = _forumCommentRepository.GetByForumId(forum.Id));
        forums.ForEach(forum => forum.Location = _locationRepository.GetById(forum.LocationId));
        return forums;
    }

    public void Add(Forum forum)
    {
        _forumRepository.Add(forum);
    }

    public List<Forum> GetForumsForAccommodations(List<Accommodation> accommodations)
    {
        List<Forum> forums = new List<Forum>();
        List<Location> locations = accommodations.Select(accommodation => _locationRepository.GetById(accommodation.LocationId)).Distinct().ToList();
        locations.ForEach(location => forums.AddRange(_forumRepository.GetByLocationId(location.Id)));
        forums.ForEach(forum => forum.Comments = _forumCommentRepository.GetByForumId(forum.Id));
        forums.ForEach(forum => forum.Location = _locationRepository.GetById(forum.LocationId));
        return forums;
    }

    public List<Forum> GetForumsByOwnerId(int ownerId)
    {
        List<Accommodation> accommodations = AccommodationService.GetInstance().GetByOwnerId(ownerId);
        return GetForumsForAccommodations(accommodations);
    }

    public bool IsLocationTaken(int locationId)
    {

        List<Forum> forum = GetAll().Where(forum => forum.LocationId == locationId).ToList();

        if (forum == null)
        {
            return false;
        }

        foreach (Forum f in forum)
        {
            if (f.ForumStatus == Resources.Types.ForumStatus.Open)
            {
                return true;
            }

        }

        return false;     
    }

    public Forum GetByLocationId(int locationId)
    {
        List<Forum> forums = _forumRepository.GetByLocationId(locationId);
        return forums.Where(forums => forums.ForumStatus == Resources.Types.ForumStatus.Open).FirstOrDefault();
    }

    public bool IsVisitedByUser(Forum forum, int userId)
    {
        List<Accommodation> accommodations = AccommodationService.GetInstance().GetAccommodationsByLocationId(forum.LocationId);
        List<AccommodationReservation> reservations = AccommodationReservationService.GetInstance().GetByUserId(userId);

        //check if any of accommodations is in reservations
        foreach (Accommodation accommodation in accommodations)
        {
            if (reservations.Any(reservation => reservation.AccommodationId == accommodation.Id))
            {
                return true;
            }
        }

        return false;
    }

    public void CloseForum(Forum forum)
    {
        forum.ForumStatus = Resources.Types.ForumStatus.Closed;
        _forumRepository.Update(forum);
    }

    public bool IsSpecialForum(Forum forum)
    {
        List<ForumComment> comments = _forumCommentRepository.GetByForumId(forum.Id);

        int countGuest = 0;
        int countSuperOwner = 0;

        foreach (ForumComment comment in comments)
        {
            if (IsVisitedByUser(forum, comment.UserId))
            {
                countGuest++;
            }
            if(UserService.GetInstance().GetById(comment.UserId).Type == Resources.Types.UserType.Owner)
            {
                
                countSuperOwner++;
            }
        }

        if (countGuest >= 20 && countSuperOwner >=10)
        {
            return true;
        }

        return false;
    }
}
