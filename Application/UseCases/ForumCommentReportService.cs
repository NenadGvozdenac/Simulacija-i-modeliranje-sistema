using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases;

public class ForumCommentReportService
{
    private IForumCommentReportRepository _forumCommentReportRepository;

    public ForumCommentReportService(IForumCommentReportRepository forumCommentReportRepository)
    {
        _forumCommentReportRepository = forumCommentReportRepository;
    }

    public static ForumCommentReportService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<ForumCommentReportService>();
    }

    public void Add(ForumCommentReport forumCommentReport)
    {
        _forumCommentReportRepository.Add(forumCommentReport);
    }

    public void Update(ForumCommentReport forumCommentReport)
    {
        _forumCommentReportRepository.Update(forumCommentReport);
    }

    public List<ForumCommentReport> GetAll()
    {
        List<ForumCommentReport> forumCommentReports = _forumCommentReportRepository.GetAll();
        forumCommentReports.ForEach(forumCommentReport =>
        {
            forumCommentReport.ForumComment = ForumCommentService.GetInstance().GetById(forumCommentReport.ForumCommentId);
            forumCommentReport.User = UserService.GetInstance().GetById(forumCommentReport.UserId);
        });

        return forumCommentReports;
    }

    public ForumCommentReport? GetById(int id)
    {
        return GetAll().FirstOrDefault(forumCommentReport => forumCommentReport.Id == id);
    }

    public List<ForumCommentReport> GetByForumCommentId(int id)
    {
        return GetAll().FindAll(forumCommentReport => forumCommentReport.ForumCommentId == id);
    }

    public List<ForumCommentReport> GetByUserId(int id)
    {
        return GetAll().FindAll(forumCommentReport => forumCommentReport.UserId == id);
    }

    public ForumCommentReport? GetWhetherUserAlreadyReported(int userId, int forumCommentId)
    {
        return GetAll().FirstOrDefault(forumCommentReport => forumCommentReport.UserId == userId && forumCommentReport.ForumCommentId == forumCommentId);
    }
}
