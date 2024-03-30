using BookingApp.Model.MutualModels;
using BookingApp.Serializer;
using BookingApp.View.GuestViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.MutualRepositories
{
   public class TourVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/tourVouchers.csv";

        private readonly Serializer<TourVoucher> _serializer;

        private List<TourVoucher> _vouchers;

        public TourVoucherRepository()
        {
            _serializer = new Serializer<TourVoucher>();
            _vouchers = _serializer.FromCSV(FilePath);
        }

        public List<TourVoucher> GetAll()
        {
            return _vouchers;
        }

        public TourVoucher GetById(int id)
        {
            return _vouchers.FirstOrDefault(a => a.Id == id);
        }

        public TourVoucher GetByTouristId(int id)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.FirstOrDefault(a => a.TouristId == id);
        }

        public void Add(TourVoucher voucher)
        {
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
        }

        public int NextId()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(c => c.Id) + 1;
        }

        public void Update(TourVoucher voucher)
        {
            var existingVoucher = _vouchers.FirstOrDefault(t => t.Id == voucher.Id);
            if (existingVoucher != null)
            {
                existingVoucher.Id = voucher.Id;
                existingVoucher.TouristId = voucher.TouristId;
                existingVoucher.ExpirationDate = voucher.ExpirationDate;
                

                _serializer.ToCSV(FilePath, _vouchers);
            }
        }

        public void Delete(int id)
        {
            var existingVoucher = _vouchers.FirstOrDefault(t => t.Id == id);
            if (existingVoucher != null)
            {
                _vouchers.Remove(existingVoucher);
                _serializer.ToCSV(FilePath, _vouchers);
            }
        }



    }
}
