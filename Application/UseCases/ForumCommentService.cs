using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class ForumCommentService
{
    public IForumCommentRepository _forumCommentRepository;
    public IUserRepository _userRepository;

    public ForumCommentService(IForumCommentRepository forumCommentRepository, IUserRepository userRepository)
    {
        _forumCommentRepository = forumCommentRepository;
        _userRepository = userRepository;
    }

    public static ForumCommentService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<ForumCommentService>();
    }

    public ForumComment GetById(int id)
    {
        ForumComment comment = _forumCommentRepository.GetById(id);
        comment.User = _userRepository.GetById(comment.UserId);
        return comment;
    }

    public List<ForumComment> GetByForumId(int forumId)
    {
        List<ForumComment> comments = _forumCommentRepository.GetByForumId(forumId);
        comments.ForEach(comment => comment.User = _userRepository.GetById(comment.UserId));
        return comments;
    }

    public void Add(ForumComment comment)
    {
        _forumCommentRepository.Add(comment);
    }
}
