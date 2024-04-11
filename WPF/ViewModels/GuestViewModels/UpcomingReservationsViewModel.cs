using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repositories;
using BookingApp.WPF.DTOs.GuestDTOs;
using BookingApp.WPF.Views.GuestViews;
using BookingApp.WPF.Views.GuestViews.Components;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class UpcomingReservationsViewModel
{
    public event EventHandler<int> RescheduleClicked;
    public event EventHandler CancelClicked;

    private User _user;
    public UpcomingReservations UpcomingReservationsView { get; set; }
    public ObservableCollection<UpcomingReservationsDTO> _upcomingReservations { get; set; }

    public UpcomingReservationsViewModel(UpcomingReservations _UpcomingReservations, User user)
    {
        _user = user;
        UpcomingReservationsView = _UpcomingReservations;
        SetUpUpcomingReservations();
        Update();
    }

    private void SetUpUpcomingReservations()
    {
        _upcomingReservations = new ObservableCollection<UpcomingReservationsDTO>();
    }

    public void Update()
    {
        int NumberOfUpcomingReservation = 0;
        _upcomingReservations.Clear();
        foreach (AccommodationReservation reservation in AccommodationReservationService.GetInstance().GetAll())
        {
            NumberOfUpcomingReservation = AddToCollection(reservation, NumberOfUpcomingReservation);
        }
        ReverseCollection();
        UpcomingReservationsView.NumberOfUpcomingRes_TextBlock.Text = "You have " + NumberOfUpcomingReservation + " upcoming reservations";
    }

    public int AddToCollection(AccommodationReservation reservation, int NumberOfUpcomingReservation)
    {
        if (reservation.FirstDateOfStaying > DateTime.Now && reservation.UserId == _user.Id)
        {
            Accommodation acc = AccommodationService.GetInstance().GetById(reservation.AccommodationId);
            AccommodationReservation accRes = AccommodationReservationService.GetInstance().GetById(reservation.Id);
            UpcomingReservationsDTO temp = new UpcomingReservationsDTO(acc, accRes);
            temp.Images = ImageService.GetInstance().GetImagesByAccommodationId(reservation.AccommodationId);
            temp.Location = LocationService.GetInstance().GetById(acc.LocationId);
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
        UpcomingReservationsView.UpcomingReservationsFrame.Content = null;
        AccommodationReservationService.GetInstance().DeleteById(reservationId);
        Update();
    }

    private void NoClicked()
    {
        UpcomingReservationsView.UpcomingReservationsFrame.Content = null;
    }

    public void UpcomingReservationsCard_RescheduleClicked(object sender, int reservationId)
    {
        RescheduleClicked?.Invoke(this, reservationId);
    }

    public void UpcomingReservationsCard_CancelClicked(object sender, int reservationId)
    {
        AccommodationReservation reservation = AccommodationReservationService.GetInstance().GetById(reservationId);
        var a = new CancelReservation(reservation);
        a.YesClicked += (sender, e) => CancelTheReservation(e);
        a.NoClicked += (sender, e) => NoClicked();
        UpcomingReservationsView.UpcomingReservationsFrame.Content = a;
    }
}

