using BookingApp.WPF.DTOs.OwnerDTOs;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedReservationMovingViewModel
{
    public AccommodationReservationMovingDTO AccommodationReservationMovingDTO { get; set; }

    public DetailedReservationMovingViewModel(AccommodationReservationMovingDTO accommodationReservationMovingDTO)
    {
        AccommodationReservationMovingDTO = accommodationReservationMovingDTO;
    }
}
