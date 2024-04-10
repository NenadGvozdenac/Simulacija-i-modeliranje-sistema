using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedAccommodationViewModel
{
    public AccommodationDTO Accommodation { get; set; }
    private Accommodation _accommodation { get; set; }

    public DetailedAccommodationViewModel(Accommodation accommodation)
    {
        Accommodation = new(accommodation);
        _accommodation = accommodation;
    }

    public bool CloseAccommodation()
    {
        return AccommodationService.GetInstance().Delete(_accommodation);
    }
}
