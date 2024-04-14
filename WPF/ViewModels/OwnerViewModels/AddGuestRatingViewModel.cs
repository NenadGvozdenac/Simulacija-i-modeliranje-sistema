using BookingApp.Application.Commands;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class AddGuestRatingViewModel : ObservableObject, INotifyPropertyChanged
{
    [ObservableProperty]
    private ObservableCollection<int> _cleanliness;

    [ObservableProperty]
    public ObservableCollection<int> _respectfulness;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddGuestRatingCommand))]
    private int _selectedCleanliness;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddGuestRatingCommand))]
    private int _selectedRespectfulness;

    [ObservableProperty]
    private string _comment;

    public GuestRatingDTO UncheckedGuestRatingDTO { get; set; }
    public AddGuestRatingPage Page { get; set; }

    public ICommand AddGuestRatingCommand => new AddGuestRatingCommand(this);
    public ICommand GoBackCommand => new NavigateToPreviousPageCommand(Page);

    public AddGuestRatingViewModel(AddGuestRatingPage page, GuestRating guestRating)
    {
        UncheckedGuestRatingDTO = new(guestRating);
        Page = page;

        Cleanliness = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
        Respectfulness = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
    }
}
