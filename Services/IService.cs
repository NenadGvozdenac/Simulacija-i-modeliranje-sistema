using BookingApp.Model.MutualModels;
using System.Collections.Generic;

namespace BookingApp.Services;

public interface IService<T>
{
    void Add(T entity);
    void Delete(T entity);
    List<T> GetAll();
    T GetById(int entityId);
    void Update(T entity);
}