using BookingApp.DTOs.OwnerDTOs;

namespace BookingApp.ViewModel.OwnerViewModels;

public class DetailedReservationMovingViewModel
{
    public AccommodationReservationMovingDTO AccommodationReservationMovingDTO { get; set; }

    public DetailedReservationMovingViewModel(AccommodationReservationMovingDTO accommodationReservationMovingDTO)
    {
        AccommodationReservationMovingDTO = accommodationReservationMovingDTO;
    }
}
