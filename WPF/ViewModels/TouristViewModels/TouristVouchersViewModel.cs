﻿using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TouristVouchersViewModel
    {
        private ObservableCollection<TourVoucher> _vouchers;
        public ObservableCollection<TourVoucher> Vouchers
        {
            get { return _vouchers; }
            set
            {
                if (_vouchers != value)
                {
                    _vouchers = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<TourVoucher> vouchers { get; set; }
        public int userId { get; set; }
        public TouristVouchers touristVouchers {  get; set; }
        public TouristVouchersViewModel(int userId, TouristVouchers _touristVouchers)
        {
            touristVouchers = _touristVouchers;
            this.userId = userId;
            vouchers = new ObservableCollection<TourVoucher>();
            Update();
        }

        public void Update()
        {
            vouchers.Clear();
            foreach (TourVoucher voucher in TourVoucherService.GetInstance().GetAll())
            {
                if (voucher.TouristId == userId && voucher.ExpirationDate > DateTime.Now)
                {
                    vouchers.Add(voucher);
                }else if(voucher.ExpirationDate < DateTime.Now)
                {
                    TourVoucherService.GetInstance().Delete(voucher.Id);
                }
            }
            Vouchers = new ObservableCollection<TourVoucher>(vouchers);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
