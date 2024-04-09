using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IOwnerInfoRepository
    {
        List<OwnerInfo> OwnerInfos { get; set; }

        void Add(OwnerInfo ownerInfo);
        void Delete(int id);
        List<OwnerInfo> GetAll();
        OwnerInfo GetById(int id);
        void Update(OwnerInfo ownerInfo);
    }
}