using BookingApp.Model.GuestModels;
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
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

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for UpcomingReservations.xaml
    /// </summary>
    public partial class UpcomingReservations : UserControl
    {
        public AccommodationRepository _accommodationRepository;

        public AccommodationReservationRepository _accommodationReservationRepository;
        public AccommodationImageRepository _accommodationImageRepository { get; set; }
        public LocationRepository _locationRepository { get; set; }
        public ObservableCollection<UpcomingReservationsDTO> _upcomingReservations { get; set; }

        public UpcomingReservations(AccommodationRepository accommodationRepository ,AccommodationReservationRepository accommodationReservationRepository)
        {
            InitializeComponent();
            DataContext = this;
            _accommodationRepository = accommodationRepository;
            _accommodationReservationRepository = accommodationReservationRepository;
            _accommodationImageRepository = new AccommodationImageRepository();
            _locationRepository = new LocationRepository();
            _upcomingReservations = new ObservableCollection<UpcomingReservationsDTO>();
            Update();
        }

        public void Update()
        {
            foreach(AccommodationReservation reservation in _accommodationReservationRepository.GetAll())
            {
                if(reservation.FirstDateOfStaying > DateTime.Now)
                {
                    Accommodation acc = _accommodationRepository.GetById(reservation.AccommodationId);
                    AccommodationReservation accRes = _accommodationReservationRepository.GetById(reservation.Id);
                    UpcomingReservationsDTO temp = new UpcomingReservationsDTO(acc, accRes);
                    temp.Images = _accommodationImageRepository.GetImagesByAccommodationId(reservation.AccommodationId);
                    temp.Location = _locationRepository.GetById(acc.LocationId);
                    _upcomingReservations.Add(temp);
                }
            }
        }
    }
}
