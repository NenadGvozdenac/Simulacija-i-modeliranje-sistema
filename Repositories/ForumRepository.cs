using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class ForumRepository : BaseRepository<Forum>, IForumRepository
{
    public ForumRepository() : base("../../../Resources/Data/forums.csv")
    {

    }

    public List<Forum> GetByLocationId(int id)
    {
        return GetAll().Where(forum => forum.LocationId == id).ToList();
    }
}
