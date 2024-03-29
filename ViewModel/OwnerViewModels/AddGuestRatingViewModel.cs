using BookingApp.DTOs.OwnerDTOs;
using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.Services.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.OwnerViewModels;

public class AddGuestRatingViewModel
{
    public GuestRatingDTO UncheckedGuestRatingDTO { get; set; }

    public ObservableCollection<int> Cleanliness { get; set; }

    public ObservableCollection<int> Respectfulness { get; set; }

    public int SelectedCleanliness { get; set; }
    public int SelectedRespectfulness { get; set; }
    public string Comment { get; set; }

    public AddGuestRatingViewModel(GuestRatingDTO guestRatingDTO)
    {
        UncheckedGuestRatingDTO = guestRatingDTO;

        Cleanliness = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
        Respectfulness = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
    }

    public bool UpdateGuestRating()
    {
        if(!IsDataValid())
        {
            return false;
        }

        GuestRating guestRating = GuestRatingService.GetInstance().GetById(UncheckedGuestRatingDTO.Id);
        guestRating.Cleanliness = SelectedCleanliness;
        guestRating.Respectfulness = SelectedRespectfulness;
        guestRating.Comment = Comment;
        guestRating.IsChecked = true;

        GuestRatingService.GetInstance().Update(guestRating);

        return true;
    }

    private bool IsDataValid()
    {
        return SelectedRespectfulness != 0 && SelectedCleanliness != 0;
    }
}
