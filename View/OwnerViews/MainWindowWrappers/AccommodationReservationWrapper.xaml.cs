using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.Repository;
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
using BookingApp.View.OwnerViews.Components;
using BookingApp.Resources.Types;

namespace BookingApp.View.OwnerViews.MainWindowWrappers;

public partial class AccommodationReservationWrapper : UserControl
{
    private AccommodationRepository _accommodationRepository;
    private AccommodationReservationRepository _accommodationReservationRepository;
    private ObservableCollection<AccommodationReservation> _reservations;

    private User _user;
    public AccommodationReservationWrapper(User user)
    {
        InitializeComponent();

        _user = user;
        _accommodationRepository = AccommodationRepository.GetInstance();
        _accommodationReservationRepository = AccommodationReservationRepository.GetInstance();
        _reservations = new ObservableCollection<AccommodationReservation>();

        CheckReservations();

        Update();
    }

    private void CheckReservations()
    {
        List<AccommodationReservation> reservations = _accommodationReservationRepository.GetReservationsByAccommodations(_accommodationRepository.GetAccommodationsByOwnerId(_user.Id));

        foreach (AccommodationReservation reservation in reservations)
        {
            _accommodationReservationRepository.CheckDate(reservation);
        }
    }

    public void Update()
    {
        _reservations.Clear();

        List<AccommodationReservation> reservations = _accommodationReservationRepository.GetReservationsByAccommodations(_accommodationRepository.GetAccommodationsByOwnerId(_user.Id));

        foreach(AccommodationReservation reservation in reservations)
        {
            _reservations.Add(reservation);
        }

        AddReservations();
    }

    private void AddReservations()
    {
        ClearReservations();
        AddOngoingReservations();
        AddUpcomingReservations();
        AddFinishedReservations();
    }

    private void ClearReservations()
    {
        Reservations.Children.Clear();
    }

    private void AddOngoingReservations()
    {
        foreach(AccommodationReservation reservation in _reservations)
        {
            if (reservation.FirstDateOfStaying <= DateTime.Now && reservation.LastDateOfStaying >= DateTime.Now)
            {
                ReservationControl component = new ReservationControl(reservation, ReservationType.Ongoing);
                component.Margin = new Thickness(15);

                component.ReservationSeeMore += (sender, e) => InvokeSeeMore(e);

                Reservations.Children.Add(component);
            }
        }
    }

    private void InvokeSeeMore(AccommodationReservation e)
    {
        DetailedReservationView detailedReservationView = new DetailedReservationView(e, _accommodationRepository.GetById(e.AccommodationId));
        NavigationService.GetNavigationService(this).Navigate(detailedReservationView);
    }

    private void AddUpcomingReservations()
    {
        foreach (AccommodationReservation reservation in _reservations)
        {
            if (reservation.FirstDateOfStaying > DateTime.Now)
            {
                ReservationControl component = new ReservationControl(reservation, ReservationType.Upcoming);
                component.Margin = new Thickness(15);

                component.ReservationSeeMore += (sender, e) => InvokeSeeMore(e);

                Reservations.Children.Add(component);
            }
        }
    }

    private void AddFinishedReservations()
    {
        foreach (AccommodationReservation reservation in _reservations)
        {
            if (reservation.LastDateOfStaying < DateTime.Now)
            {
                ReservationControl component = new ReservationControl(reservation, ReservationType.Finished);
                component.Margin = new Thickness(15);

                component.ReservationSeeMore += (sender, e) => InvokeSeeMore(e);

                Reservations.Children.Add(component);
            }
        }
    }
}
