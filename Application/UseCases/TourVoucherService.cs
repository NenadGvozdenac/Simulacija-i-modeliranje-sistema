using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourVoucherService
    {
        private ITourVoucherRepository _tourVoucherRepository;


        public TourVoucherService(ITourVoucherRepository voucherRepository)
        {
            _tourVoucherRepository = voucherRepository;
        }

        public static TourVoucherService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourVoucherService>();
        }

        public List<TourVoucher> GetAll()
        {
            return _tourVoucherRepository.GetAll();
        }

        public TourVoucher GetById(int id)
        {
            return _tourVoucherRepository.GetById(id);
        }

        public TourVoucher GetByTouristId(int id)
        {
            return _tourVoucherRepository.GetByTouristId(id);
        }

        public void Add(TourVoucher voucher)
        {
           _tourVoucherRepository.Add(voucher);
        }

        public int NextId()
        {
           return _tourVoucherRepository.NextId();
        }

        public void Update(TourVoucher voucher)
        {
           _tourVoucherRepository.Update(voucher);
        }

        public void Delete(int id)
        {
           _tourVoucherRepository.Delete(id);
            
        }




    }
}
