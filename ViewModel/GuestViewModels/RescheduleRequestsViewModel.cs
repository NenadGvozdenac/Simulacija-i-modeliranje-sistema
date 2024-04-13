using BookingApp.Model.GuestModels;
using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.View.GuestViews;

namespace BookingApp.ViewModel.GuestViewModels;

public class RescheduleRequestsViewModel
{
    public User _user;
    public AccommodationRepository _accommodationRepository;
    public AccommodationReservationMovingRepository _accommodationReservationMovingRepository;
    public LocationRepository _locationRepository;

    public ObservableCollection<AccommodationMovingDTO> _pendingRequests { get; set; }
    public ObservableCollection<AccommodationMovingDTO> _reviewedRequests { get; set; }
    public RescheduleRequests RescheduleRequests { get; set; }
    public RescheduleRequestsViewModel(RescheduleRequests _RescheduleRequests, User user, AccommodationRepository accommodationRepository, AccommodationReservationMovingRepository accommodationReservationMovingRepository)
    {
        RescheduleRequests = _RescheduleRequests;
        _user = user;
        _accommodationRepository = accommodationRepository;
        _accommodationReservationMovingRepository = accommodationReservationMovingRepository;
        SetUpRescheduleRequests();
        Update();
    }

    private void SetUpRescheduleRequests()
    {
        _locationRepository = new LocationRepository();
        _pendingRequests = new ObservableCollection<AccommodationMovingDTO>();
        _reviewedRequests = new ObservableCollection<AccommodationMovingDTO>();
    }

    public void Update()
    {
        _pendingRequests.Clear();
        _reviewedRequests.Clear();

        foreach (AccommodationReservationMoving moving in _accommodationReservationMovingRepository.GetAll())
        {
            if (IsPendingRequest(moving))
            {
                AddPendingRequest(moving);
            }
            else if (IsReviewedRequest(moving))
            {
                AddReviewedRequest(moving);
            }
            else if (IsTimedOutRequest(moving))
            {
                MarkAsTimedOut(moving);
                AddReviewedRequest(moving);
            }
        }
        ReverseCollections();
    }

    private bool IsPendingRequest(AccommodationReservationMoving moving)
    {
        return moving.Status == ReschedulingStatus.Pending &&
               moving.GuestId == _user.Id &&
               moving.CurrentReservationTimespan.Start > DateTime.Now;
    }

    private bool IsReviewedRequest(AccommodationReservationMoving moving)
    {
        return moving.GuestId == _user.Id &&
               (moving.Status == ReschedulingStatus.Accepted ||
                moving.Status == ReschedulingStatus.Rejected ||
                moving.Status == ReschedulingStatus.TimedOut);
    }

    private bool IsTimedOutRequest(AccommodationReservationMoving moving)
    {
        return moving.GuestId == _user.Id &&
               moving.CurrentReservationTimespan.Start <= DateTime.Now;
    }

    private void AddPendingRequest(AccommodationReservationMoving moving)
    {
        AccommodationMovingDTO temp = CreateAccommodationMovingDTO(moving);
        _pendingRequests.Add(temp);
    }

    private void AddReviewedRequest(AccommodationReservationMoving moving)
    {
        AccommodationMovingDTO temp = CreateAccommodationMovingDTO(moving);
        _reviewedRequests.Add(temp);
    }

    private void MarkAsTimedOut(AccommodationReservationMoving moving)
    {
        moving.Status = ReschedulingStatus.TimedOut;
        _accommodationReservationMovingRepository.Update(moving);
    }

    private AccommodationMovingDTO CreateAccommodationMovingDTO(AccommodationReservationMoving moving)
    {
        AccommodationMovingDTO temp = new AccommodationMovingDTO(
            _accommodationRepository.GetById(moving.AccommodationId), moving);
        temp.Location = _locationRepository.GetById(_accommodationRepository.GetById(moving.AccommodationId).LocationId);
        return temp;
    }
    private void ReverseCollections()
    {
        List<AccommodationMovingDTO> tempList = new List<AccommodationMovingDTO>(_pendingRequests);
        tempList.Reverse();

        _pendingRequests.Clear();
        foreach (AccommodationMovingDTO item in tempList)
        {
            _pendingRequests.Add(item);
        }

        tempList = new List<AccommodationMovingDTO>(_reviewedRequests);
        tempList.Reverse();

        _reviewedRequests.Clear();
        foreach (AccommodationMovingDTO item in tempList)
        {
            _reviewedRequests.Add(item);
        }
    }
}
