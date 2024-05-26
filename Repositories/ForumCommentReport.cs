using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class ForumCommentReportRepository : BaseRepository<ForumCommentReport>, IForumCommentReportRepository
{
    public ForumCommentReportRepository() : base("../../../Resources/Data/forum_comment_reports.csv") { }

    public List<ForumCommentReport> GetByForumCommentId(int id)
    {
        return GetAll().Where(report => report.ForumCommentId == id).ToList();
    }

    public List<ForumCommentReport> GetByUserId(int id)
    {
        return GetAll().Where(report => report.UserId == id).ToList();
    }
}
