using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IForumCommentReportRepository
{
    public void Add(ForumCommentReport forumCommentReport);
    public void Update(ForumCommentReport forumCommentReport);
    public List<ForumCommentReport> GetAll();
    public ForumCommentReport GetById(int id);
    public List<ForumCommentReport> GetByForumCommentId(int id);
    public List<ForumCommentReport> GetByUserId(int id);
}
