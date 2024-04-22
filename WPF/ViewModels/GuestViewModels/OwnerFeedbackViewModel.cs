using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.GuestViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class OwnerFeedbackViewModel
{
    public OwnerFeedback OwnerFeedbackWindow { get; set; }
    public ObservableCollection<GuestRating> _ownerFeedbacks { get; set; }
    public OwnerFeedbackViewModel(OwnerFeedback ownerFeedback,User _user)
    {
        OwnerFeedbackWindow = ownerFeedback;
        _ownerFeedbacks =  new ObservableCollection<GuestRating>();

        SetUpOwnerFeedback(_user);
    }

    private void SetUpOwnerFeedback(User user)
    {
        var ratings = GuestRatingService.GetInstance().GetAll();
        foreach(GuestRating rating in ratings)
        {
            if (rating.GuestId == user.Id)
                _ownerFeedbacks.Add(rating);
        }
    }
}
