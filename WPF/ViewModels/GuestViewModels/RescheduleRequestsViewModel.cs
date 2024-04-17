using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BookingApp.WPF.DTOs.GuestDTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.WPF.Views.GuestViews;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class RescheduleRequestsViewModel
{
    public User _user;

    public ObservableCollection<AccommodationMovingDTO> _pendingRequests { get; set; }
    public ObservableCollection<AccommodationMovingDTO> _reviewedRequests { get; set; }
    public RescheduleRequests RescheduleRequests { get; set; }
    public RescheduleRequestsViewModel(RescheduleRequests _RescheduleRequests, User user)
    {
        RescheduleRequests = _RescheduleRequests;
        _user = user;
        SetUpRescheduleRequests();
        Update();
    }

    private void SetUpRescheduleRequests()
    {
        _pendingRequests = new ObservableCollection<AccommodationMovingDTO>();
        _reviewedRequests = new ObservableCollection<AccommodationMovingDTO>();
    }

    public void Update()
    {
        _pendingRequests.Clear();
        _reviewedRequests.Clear();

        var movingList = AccommodationReservationService.GetInstance().GetAllMoving().ToList();

        foreach (AccommodationReservationMoving moving in movingList)
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
        AccommodationReservationService.GetInstance().UpdateMoving(moving);
    }

    private AccommodationMovingDTO CreateAccommodationMovingDTO(AccommodationReservationMoving moving)
    {
        AccommodationMovingDTO temp = new AccommodationMovingDTO(
            AccommodationService.GetInstance().GetById(moving.AccommodationId), moving);
        temp.Location = LocationService.GetInstance().GetById(AccommodationService.GetInstance().GetById(moving.AccommodationId).LocationId);
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
