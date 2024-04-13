using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourVoucherRepository
    {
        void Add(TourVoucher voucher);
        void Delete(int id);
        List<TourVoucher> GetAll();
        TourVoucher GetById(int id);
        TourVoucher GetByTouristId(int id);
        int NextId();
        void Update(TourVoucher voucher);
    }
}