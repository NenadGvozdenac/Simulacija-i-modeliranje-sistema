using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
using BookingApp.Model.GuestModels;
using System.Collections.ObjectModel;
using BookingApp.Model.MutualModels;

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for PastReservations.xaml
    /// </summary>
    public partial class PastReservations : UserControl
    {
        public event EventHandler<int> ReviewClicked;

        public User _user;
        public AccommodationRepository _accommodationRepository;
        public AccommodationReservationRepository _accommodationReservationRepository;
        public AccommodationImageRepository _accommodationImageRepository { get; set; }
        public LocationRepository _locationRepository { get; set; }
        public ObservableCollection<PastReservationsDTO> _pastReservations { get; set; }
        public PastReservations(User user, AccommodationRepository accommodationRepository, AccommodationReservationRepository accommodationReservationRepository)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            _accommodationRepository = accommodationRepository;
            _accommodationReservationRepository = accommodationReservationRepository;
            SetUpPastReservations();
            Update();
        }

        public void SetUpPastReservations()
        {
            _accommodationImageRepository = new AccommodationImageRepository();
            _locationRepository = new LocationRepository();
            _pastReservations = new ObservableCollection<PastReservationsDTO>();
        }

        public void Update()
        {
            _pastReservations.Clear();
            foreach (AccommodationReservation reservation in _accommodationReservationRepository.GetAll())
            {
                AddToCollection(reservation);
            }
            ReverseCollection();
        }

        private void ReverseCollection()
        {
            List<PastReservationsDTO> tempList = new List<PastReservationsDTO>(_pastReservations);
            tempList.Reverse();

            _pastReservations.Clear();
            foreach (PastReservationsDTO item in tempList)
            {
                _pastReservations.Add(item);
            }
        }

        public void AddToCollection(AccommodationReservation reservation)
        {
            if (reservation.LastDateOfStaying < DateTime.Now && reservation.UserId == _user.Id)
            {
                Accommodation acc = _accommodationRepository.GetById(reservation.AccommodationId);
                AccommodationReservation accRes = _accommodationReservationRepository.GetById(reservation.Id);
                PastReservationsDTO temp = new PastReservationsDTO(acc, accRes);
                temp.Images = _accommodationImageRepository.GetImagesByAccommodationId(reservation.AccommodationId);
                temp.Location = _locationRepository.GetById(acc.LocationId);
                _pastReservations.Add(temp);
            }
        }

        private void ReviewHandling(object sender, int reservationId)
        {
            ReviewClicked?.Invoke(this, reservationId);
        }
    }
}
