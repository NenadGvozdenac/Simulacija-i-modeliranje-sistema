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

    public int IsLocationTakenAndOpened(int locationId)
    {

        Forum forum = GetAll().FirstOrDefault(forum => forum.LocationId == locationId);

        if (forum == null)
        {
            return 1;
        }

        if(forum != null && forum.ForumStatus == Resources.Types.ForumStatus.Open)
        {
            return 2;
        }

        return 3;     
    }

    public Forum GetByLocationId(int locationId)
    {
        List<Forum> forums = _forumRepository.GetByLocationId(locationId);
        return forums.Where(forums => forums.ForumStatus == Resources.Types.ForumStatus.Open).FirstOrDefault();
    }

    public Forum GetOneByLocationId(int locationId)
    {
        List<Forum> forums = _forumRepository.GetByLocationId(locationId);
        return forums.FirstOrDefault(forums => forums.LocationId == locationId);
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

    //update
    public void Update(Forum forum)
    {
        _forumRepository.Update(forum);
    }
}
