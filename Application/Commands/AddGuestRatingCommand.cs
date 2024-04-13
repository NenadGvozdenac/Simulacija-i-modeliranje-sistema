using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace BookingApp.Application.Commands;

public class AddGuestRatingCommand : ICommand
{
    private AddGuestRatingViewModel addGuestRatingViewModel;

    public AddGuestRatingCommand(AddGuestRatingViewModel addGuestRatingViewModel)
    {
        this.addGuestRatingViewModel = addGuestRatingViewModel;
        addGuestRatingViewModel.PropertyChanged += (sender, args) => { CanExecuteChanged.Invoke(this, EventArgs.Empty); };
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return addGuestRatingViewModel.IsDataValid();
    }

    public void Execute(object parameter)
    {
        GuestRating guestRating = GuestRatingService.GetInstance().GetById(addGuestRatingViewModel.UncheckedGuestRatingDTO.Id);
        guestRating.Cleanliness = addGuestRatingViewModel.SelectedCleanliness;
        guestRating.Respectfulness = addGuestRatingViewModel.SelectedRespectfulness;
        guestRating.Comment = addGuestRatingViewModel.Comment;
        guestRating.IsChecked = true;

        GuestRatingService.GetInstance().Update(guestRating);
        addGuestRatingViewModel.GoBackCommand.Execute(null);
        addGuestRatingViewModel.Page.OnNavigationCompleted();
    }
}