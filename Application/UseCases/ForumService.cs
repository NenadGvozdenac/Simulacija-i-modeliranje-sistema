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
}
