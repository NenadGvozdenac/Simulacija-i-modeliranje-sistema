using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class ForumCommentRepository : BaseRepository<ForumComment>, IForumCommentRepository
{
    public ForumCommentRepository() : base("../../../Resources/Data/forum_comments.csv")
    {

    }

    public List<ForumComment> GetByForumId(int id)
    {
        return GetAll().Where(comment => comment.ForumId == id).ToList();
    }
}
