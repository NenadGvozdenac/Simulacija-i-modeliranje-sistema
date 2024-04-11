using BookingApp.Application.Commands;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.View.OwnerViews;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class AddGuestRatingViewModel : INotifyPropertyChanged
{
    public GuestRatingDTO UncheckedGuestRatingDTO { get; set; }

    public ObservableCollection<int> Cleanliness { get; set; }

    public ObservableCollection<int> Respectfulness { get; set; }

    private int _selectedCleanliness;
    public int SelectedCleanliness
    {
        get { return _selectedCleanliness; }
        set
        {
            if (value != _selectedCleanliness)
            {
                _selectedCleanliness = value;
                OnPropertyChanged(nameof(SelectedCleanliness));
            }
        }
    }

    private int _selectedRespectfulness;
    public int SelectedRespectfulness
    {
        get { return _selectedRespectfulness; }
        set
        {
            if (value != _selectedRespectfulness)
            {
                _selectedRespectfulness = value;
                OnPropertyChanged(nameof(SelectedRespectfulness));
            }
        }
    }

    private string _comment;
    public string Comment
    {
        get { return _comment; }
        set
        {
            if (value != _comment)
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
    }
    public AddGuestRatingPage Page { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ICommand AddGuestRatingCommand => new AddGuestRatingCommand(this);
    public ICommand GoBackCommand => new NavigateToPreviousPageCommand(Page);

    public AddGuestRatingViewModel(AddGuestRatingPage page, GuestRating guestRating)
    {
        UncheckedGuestRatingDTO = new(guestRating);
        Page = page;

        Cleanliness = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
        Respectfulness = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
    }

    public bool IsDataValid()
    {
        return SelectedRespectfulness != 0 && SelectedCleanliness != 0;
    }
}
