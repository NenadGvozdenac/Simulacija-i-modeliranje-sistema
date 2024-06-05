using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Repositories
{
    public interface IComplexTourRequestsRepository
    {
        void Add(ComplexTourRequest tourRequest);
        void Delete(int id);
        List<ComplexTourRequest> GetAll();
        ComplexTourRequest GetById(int id);
        List<ComplexTourRequest> GetByUserId(int userId);
        int NextId();
        void Update(ComplexTourRequest tourRequest);
    }
}