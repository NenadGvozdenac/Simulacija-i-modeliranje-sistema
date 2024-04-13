using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IOwnerInfoRepository
{
    void Add(OwnerInfo ownerInfo);
    void Delete(int id);
    List<OwnerInfo> GetAll();
    OwnerInfo GetById(int id);
    void Update(OwnerInfo ownerInfo);
}