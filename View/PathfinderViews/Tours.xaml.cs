using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.View.GuestViews;
using BookingApp.Model.PathfinderModels;
using BookingApp.View.TouristViews;
using BookingApp.Domain.Models;
using BookingApp.Repositories;

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for Tours.xaml
    /// </summary>
    public partial class Tours : UserControl
    {
        public ObservableCollection<Tour> tours { get; set; }

        public TourRepository tourRepository { get; set; }
        public LocationRepository locationRepository { get; set; }
        public TourImageRepository tourImageRepository { get; set; }

        public TourStartTimeRepository tourStartTimeRepository { get; set; }

        public LanguageRepository languageRepository { get; set; }

        public TouristReservationRepository touristReservationRepository { get; set; }

        public TourVoucherRepository tourVoucherRepository { get; set; }


        public Tours()
        {
            InitializeComponent();
            DataContext = this;

            tours = new ObservableCollection<Tour>();
            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();
            tourImageRepository = new TourImageRepository();
            languageRepository = new LanguageRepository();
            tourStartTimeRepository = new TourStartTimeRepository();
            tourVoucherRepository = new TourVoucherRepository();
            touristReservationRepository = new TouristReservationRepository();
            Update();

        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            foreach (TourStartTime time in tourStartTimeRepository.GetAll())
            {
                Tour toura = tourRepository.GetById(time.TourId);
                    if (time.Status == "scheduled" && DateTime.Now < time.Time){
                        Tour tour = new Tour();
                        tour.Name = toura.Name;
                        tour.Capacity = toura.Capacity;
                        tour.CurrentDate = time.Time;
                        tour.Location = locationRepository.GetById(toura.LocationId);
                        tour.Images = tourImageRepository.GetImagesByTourId(tour.Id);
                        tour.Language = languageRepository.GetById(toura.LanguageId);
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

        private void tourcard_CancelTourClicked(object sender,  BeginButtonClickedEventArgs e){
            foreach(Tour tour in tours)
            {
                if(tour.Id == e.TourId && tour.CurrentDate == e.StartTime) {
                    tours.Remove(tour);
                    List<TouristReservation> reservations = new List<TouristReservation>();
                    reservations = touristReservationRepository.GetByTourStartTimeAndId(tour.CurrentDate, tour.Id);

                    foreach (TouristReservation reservation in reservations) { 
                        TourVoucher voucher = new TourVoucher();
                        voucher.Id = tourVoucherRepository.NextId();
                        voucher.TouristId = reservation.Id_Tourist;
                        voucher.ExpirationDate = DateTime.Now.AddDays(365);
                        tourVoucherRepository.Add(voucher);
                        touristReservationRepository.Delete(reservation.Id);
                     }


                    tourStartTimeRepository.RemoveByTourStartTimeAndId(e.StartTime, e.TourId);
                    return;
                }
            }
        }
   
    
    
    
    
    
    }
}
