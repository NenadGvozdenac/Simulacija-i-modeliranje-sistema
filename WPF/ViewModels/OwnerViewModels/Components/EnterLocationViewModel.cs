using BookingApp.Application.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.Components;

public partial class EnterLocationViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddLocationCommand))]
    private string _city;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddLocationCommand))]
    private string _country;

    public ICommand AddLocationCommand => new AddLocationCommand(this);
    public ICommand CancelAddingLocationCommand => new CancelAddingNewLocationCommand(this);

    public AddAccommodationViewModel AddAccommodationViewModel { get; }

    public EnterLocationViewModel(AddAccommodationViewModel addAccommodationViewModel)
    {
        AddAccommodationViewModel = addAccommodationViewModel;
    }
}
