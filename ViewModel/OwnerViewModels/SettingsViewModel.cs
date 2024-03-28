using BookingApp.DTOs.OwnerDTOs;
using BookingApp.Model.MutualModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.OwnerViewModels;

public class SettingsViewModel
{
    public OwnerUserDTO OwnerUserDTO { get; set; }

    public SettingsViewModel(User user)
    {
        OwnerUserDTO = new(user);
    }
}
