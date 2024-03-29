using BookingApp.Miscellaneous;
using BookingApp.Model.MutualModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

public partial class ReservationRescheduling : UserControl, INotifyPropertyChanged
{
    public event EventHandler<AccommodationReservationMoving> ReservationReschedulingDetails;

    private AccommodationReservationMoving accommodationReservationMoving;
    public AccommodationReservationMoving AccommodationReservationMoving
    {
        get => accommodationReservationMoving;
        set
        {
            accommodationReservationMoving = value;
            OnPropertyChanged();
        }
    }
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

    private string guestUsername;

    public string GuestUsername
    {
        get => guestUsername;
        set
        {
            guestUsername = value;
            OnPropertyChanged();
        }
    }

    private DateSpan currentReservationTimespan;
    public DateSpan CurrentReservationTimespan
    {
        get => currentReservationTimespan;
        set
        {
            currentReservationTimespan = value;
            OnPropertyChanged();
        }
    }

    private DateSpan wantedReservationTimespan;
    public DateSpan WantedReservationTimespan
    {
        get => wantedReservationTimespan;
        set
        {
            wantedReservationTimespan = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public ReservationRescheduling(AccommodationReservationMoving accommodationReservationMoving)
    {
        DataContext = this;
        AccommodationReservationMoving = accommodationReservationMoving;
        AccommodationName = AccommodationReservationMoving.Accommodation.Name;
        GuestUsername = AccommodationReservationMoving.Guest.Username;
        CurrentReservationTimespan = AccommodationReservationMoving.CurrentReservationTimespan;
        WantedReservationTimespan = AccommodationReservationMoving.WantedReservationTimespan;
        InitializeComponent();
    }

    private void ReservationRescheduling_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        LoadReschedulingDetails();
    }

    private void LoadReschedulingDetails()
    {
        ReservationReschedulingDetails.Invoke(this, AccommodationReservationMoving);
    }
}
