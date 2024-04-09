using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class MainPageViewModel
{
    private List<Accommodation> _accommodations;
    public List<Accommodation> Accommodations
    {
        get => _accommodations;
        set => _accommodations = value;
    }

    private List<AccommodationReservation> _accommodationReservations;

    public List<AccommodationReservation> AccommodationReservations
    {
        get => _accommodationReservations;
        set => _accommodationReservations = value;
    }

    private User _user;
    public User User
    {
        get => _user;
        set => _user = value;
    }

    public MainPageViewModel(User user)
    {
        User = user;
        Accommodations = new List<Accommodation>(AccommodationService.GetInstance().GetAll());
        AccommodationReservations = new List<AccommodationReservation>(AccommodationReservationService.GetInstance().GetReservationsByOwnerId(User.Id));
    }

    public void Refresh()
    {
        Accommodations = new List<Accommodation>(AccommodationRepository.GetInstance().GetAll());
        AccommodationReservations = new List<AccommodationReservation>(AccommodationReservationService.GetInstance().GetReservationsByOwnerId(User.Id));
    }
}
