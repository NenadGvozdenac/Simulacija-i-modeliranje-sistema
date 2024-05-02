using BookingApp.Application.Localization;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.Components;

public class RenovationCardViewModel
{
    private RenovationControl renovationControl;
    public AccommodationRenovation Renovation { get; set; }
    public string RenovationStatus
    {
        get => TranslationSource.Instance[Renovation.Status.ToString()];
    }
    public RenovationCardViewModel(RenovationControl renovationControl, AccommodationRenovation renovation)
    {
        this.renovationControl = renovationControl;
        Renovation = renovation;
    }
}
