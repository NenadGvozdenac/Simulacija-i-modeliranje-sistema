using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class SettingsViewModel
{
    public OwnerUserDTO OwnerUserDTO { get; set; }

    public SettingsViewModel(User user)
    {
        OwnerUserDTO = new(user);
    }
}
