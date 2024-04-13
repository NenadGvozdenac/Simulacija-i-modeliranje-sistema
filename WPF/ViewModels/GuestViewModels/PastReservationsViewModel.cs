using BookingApp.Domain.Models;
using BookingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.WPF.Views.GuestViews;
using BookingApp.WPF.DTOs.GuestDTOs;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.GuestViewModels;
public class PastReservationsViewModel
{
    public event EventHandler<int> ReviewClicked;

    public User _user;
    public ObservableCollection<PastReservationsDTO> _pastReservations { get; set; }
    public PastReservations PastReservations { get; set; }
    public PastReservationsViewModel(PastReservations _PastReservations, User user)
    {
        PastReservations = _PastReservations;
        _user = user;
        SetUpPastReservations();
        Update();
    }

    public void SetUpPastReservations()
    {
        _pastReservations = new ObservableCollection<PastReservationsDTO>();
    }

    public void Update()
    {
        _pastReservations.Clear();
        foreach (AccommodationReservation reservation in   AccommodationReservationService.GetInstance().GetAll())
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
            Accommodation acc = AccommodationService.GetInstance().GetById(reservation.AccommodationId);
            AccommodationReservation accRes = AccommodationReservationService.GetInstance().GetById(reservation.Id);
            PastReservationsDTO temp = new PastReservationsDTO(acc, accRes);
            temp.Images = ImageService.GetInstance().GetImagesByAccommodationId(reservation.AccommodationId);
            temp.Location = LocationService.GetInstance().GetById(acc.LocationId);
            _pastReservations.Add(temp);
        }
    }

    public void ReviewHandling(object sender, int reservationId)
    {
        ReviewClicked?.Invoke(this, reservationId);
    }
}

