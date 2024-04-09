using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IRepository<T>
{
    public void Add(T entity);
    public void Update(T entity);
    public void Delete(int id);
    public T GetById(int id);
    public List<T> GetAll();
}
