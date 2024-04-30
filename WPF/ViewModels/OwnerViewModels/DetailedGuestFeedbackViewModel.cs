using BookingApp.Domain.Models;
using BookingApp.Domain.Miscellaneous;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BookingApp.Application.UseCases;
using BookingApp.WPF.Views.OwnerViews.Components;
using BookingApp.WPF.Views.OwnerViews;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingApp.WPF.Views.GuestViews;
using System.Windows.Media;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class DetailedGuestFeedbackViewModel : ObservableObject
{
    public GuestFeedbackDTO GuestFeedbackDTO { get; set; }
    public AccommodationReview AccommodationReview { get; set; }

    public DetailedGuestFeedbackViewModel(AccommodationReview accommodationReview)
    {
        GuestFeedbackDTO = new GuestFeedbackDTO(accommodationReview);
        AccommodationReview = accommodationReview;
    }

    public void CardClicked(GuestFeedbackControl guestFeedbackControl)
    {
        DetailedGuestFeedbackPage detailedGuestFeedbackPage = new DetailedGuestFeedbackPage(this);
        NavigationService.GetNavigationService(guestFeedbackControl).Navigate(detailedGuestFeedbackPage);
    }

    public void LeftArrowClick()
    {
        var currentImage = GuestFeedbackDTO.Images.FirstOrDefault(image => image.Path == GuestFeedbackDTO.ImageURL);
        var currentIndex = GuestFeedbackDTO.Images.IndexOf(currentImage);

        if (currentIndex == -1) { return; }

        if (currentIndex == 0)
        {
            GuestFeedbackDTO.ImageURL = GuestFeedbackDTO.Images.Last().Path;
        }
        else
        {
            GuestFeedbackDTO.ImageURL = GuestFeedbackDTO.Images[currentIndex - 1].Path;
        }
    }

    public void RightArrowClick()
    {
        var currentImage = GuestFeedbackDTO.Images.FirstOrDefault(image => image.Path == GuestFeedbackDTO.ImageURL);
        var currentIndex = GuestFeedbackDTO.Images.IndexOf(currentImage);
        if (currentIndex == -1) { return; }

        if (currentIndex == GuestFeedbackDTO.Images.Count - 1)
        {
            GuestFeedbackDTO.ImageURL = GuestFeedbackDTO.Images.First().Path;
        }
        else
        {
            GuestFeedbackDTO.ImageURL = GuestFeedbackDTO.Images[currentIndex + 1].Path;
        }
    }
}
