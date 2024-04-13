using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repositories;

public class OwnerInfoRepository : BaseRepository<OwnerInfo>, IOwnerInfoRepository
{
    public OwnerInfoRepository() : base("../../../Resources/Data/owner_infos.csv", "OwnerId") {}
}
