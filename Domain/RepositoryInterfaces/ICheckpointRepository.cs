using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ICheckpointRepository
    {
        public List<Checkpoint> GetAll();
        void Add(Checkpoint checkpoint);
        void Delete(int id);
        void DeleteByAccommodationId(int id);
        List<Checkpoint> GetCheckpointsByTourId(int id);
        int NextId();
        void RemoveByTourId(int tourId);
        void Update(Checkpoint checkpoint);
    }
}