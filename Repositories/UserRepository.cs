using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BookingApp.Repositories;

public class UserRepository : IRepository<User>, IUserRepository
{
    private const string FilePath = "../../../Resources/Data/users.csv";
    private readonly Serializer<User> _serializer;

    private List<User> _users;

    public UserRepository()
    {
        _serializer = new Serializer<User>();
        _users = _serializer.FromCSV(FilePath);
    }

    public static UserRepository GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<UserRepository>();
    }

    public User GetByUsername(string username)
    {
        _users = _serializer.FromCSV(FilePath);
        return _users.First(u => u.Username == username);
    }

    public User GetById(int id)
    {
        return _users.First(u => u.Id == id);
    }

    /// <summary>
    /// Adds a user to the repository
    /// </summary>
    /// <param name="user"></param>
    public void Add(User user)
    {
        user.Id = NextId();
        _users.Add(user);
        _serializer.ToCSV(FilePath, _users);
    }

    /// <summary>
    /// Gets the next id for the user
    /// </summary>
    /// <returns></returns>
    public int NextId()
    {
        _users = _serializer.FromCSV(FilePath);
        if (_users.Count < 1)
        {
            return 1;
        }
        return _users.Max(c => c.Id) + 1;
    }

    public void Delete(int ownerId)
    {
        _users.RemoveAll(u => u.Id == ownerId);
        _serializer.ToCSV(FilePath, _users);
    }

    public void Update(User user)
    {
        if (_users.Contains(user))
        {
            _users[_users.FindIndex(u => u.Id == user.Id)] = user;
            _serializer.ToCSV(FilePath, _users);
        }
    }

    public List<User> GetAll()
    {
        return _users;
    }
}
