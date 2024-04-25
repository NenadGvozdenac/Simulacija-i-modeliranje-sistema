using BookingApp.Application.Commands;
using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using BookingApp.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedRenovationViewModel
{
    private readonly DetailedRenovationView page;
    public AccommodationDTO AccommodationDTO { get; set; }
    public RenovationDTO RenovationDTO { get; set; }
    public AccommodationRenovation Renovation { get; set; }

    public ICommand CancelRenovationCommand => new CancelRenovationCommand(page);
    public DetailedRenovationViewModel(DetailedRenovationView page, AccommodationRenovation renovation)
    {
        AccommodationDTO = new AccommodationDTO(renovation.Accommodation);
        RenovationDTO = new RenovationDTO(renovation);
        this.page = page;
        Renovation = renovation;
    }
}
