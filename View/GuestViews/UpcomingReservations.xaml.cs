using BookingApp.Model.GuestModels;
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.View.GuestViews.Components;
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

        public event EventHandler<int> RescheduleClicked;
        public event EventHandler CancelClicked;

        private User _user;
        public AccommodationRepository _accommodationRepository;
        public AccommodationReservationRepository _accommodationReservationRepository;
        public AccommodationImageRepository _accommodationImageRepository { get; set; }
        public LocationRepository _locationRepository { get; set; }
        public ObservableCollection<UpcomingReservationsDTO> _upcomingReservations { get; set; }

        public UpcomingReservations(User user, AccommodationRepository accommodationRepository, AccommodationReservationRepository accommodationReservationRepository)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            _accommodationRepository = accommodationRepository;
            _accommodationReservationRepository = accommodationReservationRepository;
            SetUpUpcomingReservations();
            Update();
        }

        private void SetUpUpcomingReservations()
        {
            _accommodationImageRepository = new AccommodationImageRepository();
            _locationRepository = new LocationRepository();
            _upcomingReservations = new ObservableCollection<UpcomingReservationsDTO>();
        }

        public void Update()
        {
            int NumberOfUpcomingReservation = 0;
            _upcomingReservations.Clear();
            foreach(AccommodationReservation reservation in _accommodationReservationRepository.GetAll())
            {
                NumberOfUpcomingReservation = AddToCollection(reservation, NumberOfUpcomingReservation);
            }
            ReverseCollection();
            NumberOfUpcomingRes_TextBlock.Text = "You have " + NumberOfUpcomingReservation + " upcoming reservations";
        }

        public int AddToCollection(AccommodationReservation reservation, int NumberOfUpcomingReservation)
        {
            if (reservation.FirstDateOfStaying > DateTime.Now && reservation.UserId == _user.Id)
            {
                Accommodation acc = _accommodationRepository.GetById(reservation.AccommodationId);
                AccommodationReservation accRes = _accommodationReservationRepository.GetById(reservation.Id);
                UpcomingReservationsDTO temp = new UpcomingReservationsDTO(acc, accRes);
                temp.Images = _accommodationImageRepository.GetImagesByAccommodationId(reservation.AccommodationId);
                temp.Location = _locationRepository.GetById(acc.LocationId);
                _upcomingReservations.Add(temp);
                NumberOfUpcomingReservation++;
            }
            return NumberOfUpcomingReservation;
        }

        private void ReverseCollection()
        {
            List<UpcomingReservationsDTO> tempList = new List<UpcomingReservationsDTO>(_upcomingReservations);
            tempList.Reverse();

            _upcomingReservations.Clear();
            foreach (UpcomingReservationsDTO item in tempList)
            {
                _upcomingReservations.Add(item);
            }
        }
        
        private void CancelTheReservation(int reservationId)
        {
            UpcomingReservationsFrame.Content = null;
            _accommodationReservationRepository.Delete(reservationId);
            //GuestRatingRepository.GetInstance().Delete(reservationId); SONE GUSTER TODO
            Update();
        }
        
        private void NoClicked()
        {
            UpcomingReservationsFrame.Content = null;
        }

        private void UpcomingReservationsCard_RescheduleClicked(object sender, int reservationId)
        {
            RescheduleClicked?.Invoke(this, reservationId);
        }

        private void UpcomingReservationsCard_CancelClicked(object sender, int reservationId)
        {
            AccommodationReservation reservation = _accommodationReservationRepository.GetById(reservationId);
            var a = new CancelReservation(reservation, _accommodationRepository);
            a.YesClicked += (sender, e) => CancelTheReservation(e);
            a.NoClicked += (sender, e) => NoClicked();
            UpcomingReservationsFrame.Content = a;      
        }
    }
}
