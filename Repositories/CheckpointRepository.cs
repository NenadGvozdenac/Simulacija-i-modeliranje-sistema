using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model.PathfinderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class CheckpointRepository : ICheckpointRepository
{
    private const string FilePath = "../../../Resources/Data/checkpoints.csv";

    private readonly Serializer<Checkpoint> _serializer;

    private List<Checkpoint> _checkpoints;

    public CheckpointRepository()
    {
        _serializer = new Serializer<Checkpoint>();
        _checkpoints = _serializer.FromCSV(FilePath);
    }

    public void Add(Checkpoint checkpoint)
    {
        _checkpoints.Add(checkpoint);
        _serializer.ToCSV(FilePath, _checkpoints);
    }

    public void Update(Checkpoint checkpoint)
    {
        var existingCheckpoint = _checkpoints.FirstOrDefault(c => c.Id == checkpoint.Id);
        if (existingCheckpoint != null)
        {
            existingCheckpoint.Id = checkpoint.Id;
            existingCheckpoint.Name = checkpoint.Name;
            existingCheckpoint.TourId = checkpoint.TourId;
            existingCheckpoint.Checked = checkpoint.Checked;

            _serializer.ToCSV(FilePath, _checkpoints);
        }
    }

    public void Delete(int id)
    {
        var existingAccommodation = _checkpoints.FirstOrDefault(c => c.Id == id);
        if (existingAccommodation != null)
        {
            _checkpoints.Remove(existingAccommodation);
            _serializer.ToCSV(FilePath, _checkpoints);
        }
    }

    public int NextId()
    {
        _checkpoints = _serializer.FromCSV(FilePath);
        if (_checkpoints.Count < 1)
        {
            return 1;
        }
        return _checkpoints.Max(c => c.Id) + 1;
    }



    public void RemoveByTourId(int tourId)
    {
        _checkpoints.RemoveAll(a => a.TourId == tourId);
        _serializer.ToCSV(FilePath, _checkpoints);
    }

    public List<Checkpoint> GetCheckpointsByTourId(int id)
    {
        return _checkpoints.Where(a => a.TourId == id).ToList();
    }

    public void DeleteByAccommodationId(int id)
    {
        _checkpoints.RemoveAll(a => a.TourId == id);
        _serializer.ToCSV(FilePath, _checkpoints);
    }


}
