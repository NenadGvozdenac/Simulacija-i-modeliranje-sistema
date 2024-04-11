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

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedGuestFeedbackViewModel
{
    public GuestFeedbackDTO GuestFeedbackDTO { get; set; }
    public AccommodationReview AccommodationReview { get; set; }
    public DetailedGuestFeedbackViewModel(AccommodationReview accommodationReview)
    {
        GuestFeedbackDTO = new GuestFeedbackDTO(accommodationReview);
        AccommodationReview = accommodationReview;
    }

    public void LoadImages(StackPanel images_StackPanel)
    {
        if (AccommodationReview.ReviewImages.Count > 0)
        {
            foreach (var reviewImage in AccommodationReview.ReviewImages)
            {
                Image image = ImageService.GetInstance().ReadImage(reviewImage.Path);
                images_StackPanel.Children.Add(image);
            }
        }
    }
}
