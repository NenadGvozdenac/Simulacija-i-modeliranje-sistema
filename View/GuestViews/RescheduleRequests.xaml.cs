using BookingApp.Model.GuestModels;
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for RescheduleRequests.xaml
    /// </summary>
    public partial class RescheduleRequests : UserControl
    {
        public User _user;
        public AccommodationRepository _accommodationRepository;
        public AccommodationReservationMovingRepository _accommodationReservationMovingRepository;
        public LocationRepository _locationRepository;

        public ObservableCollection<AccommodationMovingDTO> _pendingRequests { get; set; }
        public ObservableCollection<AccommodationMovingDTO> _reviewedRequests { get; set; }

        public RescheduleRequests(User user, AccommodationRepository accommodationRepository, AccommodationReservationMovingRepository accommodationReservationMovingRepository)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            _accommodationRepository = accommodationRepository;
            _accommodationReservationMovingRepository = accommodationReservationMovingRepository;
            _locationRepository = new LocationRepository();
            _pendingRequests = new ObservableCollection<AccommodationMovingDTO>();
            _reviewedRequests = new ObservableCollection<AccommodationMovingDTO>();
            SetUpUserControl();
        }

        public void SetUpUserControl()
        {
            _pendingRequests.Clear();
            _reviewedRequests.Clear();
            foreach(AccommodationReservationMoving moving in  _accommodationReservationMovingRepository.GetAll()) 
            { 
                if(moving.Status == ReschedulingStatus.Pending && moving.GuestId == _user.Id && moving.CurrentReservationTimespan.Start > DateTime.Now)
                {
                    AccommodationMovingDTO temp = new AccommodationMovingDTO(_accommodationRepository.GetById(moving.AccommodationId), moving);
                    temp.Location = _locationRepository.GetById(_accommodationRepository.GetById(moving.AccommodationId).LocationId);
                    _pendingRequests.Add(temp);
                }
                else if(moving.GuestId == _user.Id && (moving.Status == ReschedulingStatus.Accepted || moving.Status == ReschedulingStatus.Rejected || moving.Status == ReschedulingStatus.TimedOut))
                {
                    AccommodationMovingDTO temp = new AccommodationMovingDTO(_accommodationRepository.GetById(moving.AccommodationId), moving);
                    temp.Location = _locationRepository.GetById(_accommodationRepository.GetById(moving.AccommodationId).LocationId);
                    _reviewedRequests.Add(temp);
                }
                else if(moving.GuestId == _user.Id && moving.CurrentReservationTimespan.Start <= DateTime.Now)
                {
                    moving.Status = ReschedulingStatus.TimedOut;
                    _accommodationReservationMovingRepository.Update(moving);
                    AccommodationMovingDTO temp = new AccommodationMovingDTO(_accommodationRepository.GetById(moving.AccommodationId), moving);
                    temp.Location = _locationRepository.GetById(_accommodationRepository.GetById(moving.AccommodationId).LocationId);
                    _reviewedRequests.Add(temp);
                }
            }           
        }
    }
}
