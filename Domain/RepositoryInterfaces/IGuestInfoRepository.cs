using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IGuestInfoRepository
{
    void Add(GuestInfo guestinfo);
    void Delete(int id);
    List<GuestInfo> GetAll();
    GuestInfo GetById(int id);
    void Update(GuestInfo guestInfo);
    GuestInfo GetByGuestId(int guestId);
}
