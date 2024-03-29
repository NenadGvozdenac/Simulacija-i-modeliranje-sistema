using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
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

namespace BookingApp.View.OwnerViews.Components;

public partial class ReservationControl : UserControl, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private AccommodationRepository _accommodationRepository;
    private UserRepository _userRepository;

    public event EventHandler<AccommodationReservation> ReservationSeeMore;

    private AccommodationReservation _accommodationReservation;

    private string accommodationName;
    public string AccommodationName
    {
        get => accommodationName;
        set
        {
            accommodationName = value;
            OnPropertyChanged();
        }
    }

    private string guestName;
    public string GuestName
    {
        get => guestName;
        set
        {
            guestName = value;
            OnPropertyChanged();
        }
    }

    private string reservationTimespan;
    public string ReservationTimespan
    {
        get => reservationTimespan;
        set
        {
            reservationTimespan = value;
            OnPropertyChanged();
        }
    }

    private ReservationType reservationType;
    public ReservationType ReservationType
    {
        get => reservationType;
        set
        {
            reservationType = value;
            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ReservationControl(AccommodationReservation accommodationReservation, ReservationType reservationType)
    {
        DataContext = this;
        
        _accommodationRepository = AccommodationRepository.GetInstance();
        _userRepository = UserRepository.GetInstance();
        
        AccommodationName = _accommodationRepository.GetById(accommodationReservation.AccommodationId).Name;
        GuestName = _userRepository.GetById(accommodationReservation.UserId).Username;
        ReservationTimespan = accommodationReservation.FirstDateOfStaying.ToString("dd.MM.yyyy") + " - " + accommodationReservation.LastDateOfStaying.ToString("dd.MM.yyyy");
        ReservationType = reservationType;

        _accommodationReservation = accommodationReservation;

        InitializeComponent();
    }

    private void AccommodationReservationClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ReservationSeeMore?.Invoke(this, _accommodationReservation);
    }
}
