using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookingApp.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class, ISerializable, new()
{
    private readonly Serializer<T> _serializer;
    private List<T> _entities;
    private string _filePath;
    private string _id;
    public BaseRepository(string filePath, string id = "Id")
    {
        _filePath = filePath;
        _id = id;
        _serializer = new Serializer<T>();
        _entities = _serializer.FromCSV(filePath);
    }
    public void Add(T entity)
    {
        int id = NextId();
        PropertyInfo idProperty = typeof(T).GetProperty(_id);
        idProperty.SetValue(entity, id);

        _entities.Add(entity);
        _serializer.ToCSV(_filePath, _entities);
    }

    public int NextId()
    {
        PropertyInfo idProperty = typeof(T).GetProperty(_id);
        if (_entities.Count < 1)
        {
            return 1;
        }
        return _entities.Max(e => (int)idProperty.GetValue(e)) + 1;
    }

    public void Delete(int id)
    {
        PropertyInfo idProperty = typeof(T).GetProperty(_id);
        _entities.RemoveAll(e => (int)idProperty.GetValue(e) == id);
        _serializer.ToCSV(_filePath, _entities);
    }

    public void DeleteRange(List<T> entities)
    {
        PropertyInfo idProperty = typeof(T).GetProperty(_id);
        _entities.RemoveAll(e => entities.Any(entity => (int)idProperty.GetValue(entity) == (int)idProperty.GetValue(e)));
        _serializer.ToCSV(_filePath, _entities);
    }

    public List<T> GetAll()
    {
        return _entities;
    }

    public T GetById(int id)
    {
        PropertyInfo idProperty = typeof(T).GetProperty(_id);
        return _entities.FirstOrDefault(e => (int)idProperty.GetValue(e) == id);
    }

    public void Update(T entity)
    {
        PropertyInfo id = typeof(T).GetProperty(_id);
        T entityToUpdate = _entities.FirstOrDefault(e => (int)id.GetValue(e) == (int)id.GetValue(entity));

        if (entityToUpdate == null)
        {
            return;
        }

        int index = _entities.IndexOf(entityToUpdate);
        _entities[index] = entity;
        _serializer.ToCSV(_filePath, _entities);
    }
}