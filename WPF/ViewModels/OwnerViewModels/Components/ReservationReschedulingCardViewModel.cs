using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.Components;

public class ReservationReschedulingCardViewModel
{
    public AccommodationReservationMoving AccommodationReservationMoving { get; set; }

    public ReservationReschedulingCardViewModel(AccommodationReservationMoving accommodationReservationMoving)
    {
        this.AccommodationReservationMoving = accommodationReservationMoving;
    }
}
