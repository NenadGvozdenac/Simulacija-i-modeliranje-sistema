using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories;

public class GuestInfoRepository : BaseRepository<GuestInfo>, IGuestInfoRepository
{
    public GuestInfoRepository() : base("../../../Resources/Data/guest_infos.csv", "Id") { }

    public GuestInfo GetByGuestId(int guestId)
    {
        return GetAll().Find(guestInfo => guestInfo.GuestId == guestId);
    }
}
