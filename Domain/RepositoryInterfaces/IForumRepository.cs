using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IForumRepository
{
    public List<Forum> GetAll();
    public Forum GetById(int id);
    public void Add(Forum forum);
    public void Update(Forum forum);
    public List<Forum> GetByLocationId(int id);
}
