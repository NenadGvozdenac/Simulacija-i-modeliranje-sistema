using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.View.PathfinderViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class ToursViewModel
    {
        public ObservableCollection<Tour> tours { get; set; }
        public Tours _tours { get; set;} 

        public ToursViewModel(Tours tours_)
        {
            
            _tours = tours_;
            tours = new ObservableCollection<Tour>();            
            Update();

        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            foreach (TourStartTime time in TourStartTimeService.GetInstance().GetAll())
            {
                Tour toura = TourService.GetInstance().GetById(time.TourId);
                if (time.Status == "scheduled" && DateTime.Now < time.Time)
                {
                    Tour tour = new Tour();
                    tour.Name = toura.Name;
                    tour.Capacity = toura.Capacity;
                    tour.CurrentDate = time.Time;
                    tour.Location = LocationService.GetInstance().GetById(toura.LocationId);
                    tour.Images = TourImageService.GetInstance().GetImagesByTourId(tour.Id);
                    tour.Language = LanguageService.GetInstance().GetById(toura.LanguageId); //fix
                    tour.Id = toura.Id;
                    tour.LocationId = toura.LocationId;
                    tour.LanguageId = toura.LanguageId;
                    tour.Duration = toura.Duration;
                    tour.Checkpoints = toura.Checkpoints;
                    tour.Dates = toura.Dates;
                    tour.Description = toura.Description;



                    tours.Add(tour);

                }

            }

        }

        public void tourcard_CancelTourClicked(object sender, BeginButtonClickedEventArgs e)
        {
            foreach (Tour tour in tours)
            {
                if (tour.Id == e.TourId && tour.CurrentDate == e.StartTime)
                {
                    tours.Remove(tour);
                    List<TouristReservation> reservations = new List<TouristReservation>();
                    reservations = TourReservationService.GetInstance().GetByTourStartTimeAndId(tour.CurrentDate, tour.Id);

                    foreach (TouristReservation reservation in reservations)
                    {
                        TourVoucher voucher = new TourVoucher();
                        voucher.Id = TourVoucherService.GetInstance().NextId();
                        voucher.TouristId = reservation.Id_Tourist;
                        voucher.ExpirationDate = DateTime.Now.AddDays(365);
                        TourVoucherService.GetInstance().Add(voucher);
                        TourReservationService.GetInstance().Delete(reservation.Id);
                    }


                    TourStartTimeService.GetInstance().RemoveByTourStartTimeAndId(e.StartTime, e.TourId);
                    return;
                }
            }
        }

    }
}
