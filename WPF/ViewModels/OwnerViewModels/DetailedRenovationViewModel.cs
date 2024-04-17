using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedRenovationViewModel
{
    public AccommodationDTO AccommodationDTO { get; set; }
    public RenovationDTO RenovationDTO { get; set; }

    public DetailedRenovationViewModel(AccommodationRenovation renovation)
    {
        AccommodationDTO = new AccommodationDTO(renovation.Accommodation);
        RenovationDTO = new RenovationDTO(renovation);
    }
}
