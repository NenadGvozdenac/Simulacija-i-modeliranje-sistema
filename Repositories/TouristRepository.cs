using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.View.PathfinderViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class TouristRepository : ITouristRepository
{
    private const string FilePath = "../../../Resources/Data/tourists.csv";

    private readonly Serializer<Tourist> _serializer;

    private List<Tourist> _tourists;

    public TouristRepository()
    {
        _serializer = new Serializer<Tourist>();
        _tourists = _serializer.FromCSV(FilePath);
    }

    public List<Tourist> GetAll()
    {
        return _tourists;
    }

    public Tourist GetById(int id)
    {
        return _tourists.FirstOrDefault(a => a.Id == id);
    }

    public Tourist GetByName(string name)
    {
        _tourists = _serializer.FromCSV(FilePath);
        return _tourists.FirstOrDefault(t => t.Name == name);
    }

    public void Add(Tourist tourist)
    {
        _tourists.Add(tourist);
        _serializer.ToCSV(FilePath, _tourists);
    }

    public int NextId()
    {
        _tourists = _serializer.FromCSV(FilePath);
        if (_tourists.Count < 1)
        {
            return 1;
        }
        return _tourists.Max(c => c.Id) + 1;
    }
    public void Update(Tourist tourist)
    {
        var existingTourist = _tourists.FirstOrDefault(t => t.Id == tourist.Id);
        if (existingTourist != null)
        {
            existingTourist.Name = tourist.Name;
            existingTourist.Surname = tourist.Surname;
            existingTourist.Age = tourist.Age;

            _serializer.ToCSV(FilePath, _tourists);
        }
    }

    public void Delete(int id)
    {
        var existingTourist = _tourists.FirstOrDefault(t => t.Id == id);
        if (existingTourist != null)
        {
            _tourists.Remove(existingTourist);
            _serializer.ToCSV(FilePath, _tourists);
        }
    }

}
