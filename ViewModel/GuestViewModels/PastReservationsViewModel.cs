using BookingApp.Model.GuestModels;
using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.View.GuestViews;

namespace BookingApp.ViewModel.GuestViewModels;
public class PastReservationsViewModel
{
    public event EventHandler<int> ReviewClicked;

    public User _user;
    public AccommodationRepository _accommodationRepository;
    public AccommodationReservationRepository _accommodationReservationRepository;
    public AccommodationImageRepository _accommodationImageRepository { get; set; }
    public LocationRepository _locationRepository { get; set; }
    public ObservableCollection<PastReservationsDTO> _pastReservations { get; set; }
    public PastReservations PastReservations { get; set; }
    public PastReservationsViewModel(PastReservations _PastReservations, User user, AccommodationRepository accommodationRepository, AccommodationReservationRepository accommodationReservationRepository)
    {
        PastReservations = _PastReservations;
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

    public void ReviewHandling(object sender, int reservationId)
    {
        ReviewClicked?.Invoke(this, reservationId);
    }
}

