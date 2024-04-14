using BookingApp.Application.Commands;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.DTOs.OwnerDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class DetailedAccommodationViewModel : ObservableObject
{
    public AccommodationDTO Accommodation { get; set; }
    public Accommodation _accommodation { get; set; }
    public DetailedAccommodationPage Page { get; set; }
    [ObservableProperty]
    private string _imageURL;
    public ICommand CloseAccommodationCommand => new CloseAccommodationCommand(this);

    public DetailedAccommodationViewModel(DetailedAccommodationPage page, Accommodation accommodation)
    {
        Page = page;
        Accommodation = new(accommodation);
        _accommodation = accommodation;
        _imageURL = Accommodation.Images.Count > 0 ? Accommodation.Images[0].Path : "";
    }

    public void LeftArrowClick()
    {
        var currentImage = _accommodation.Images.FirstOrDefault(image => image.Path == ImageURL);
        var currentIndex = _accommodation.Images.IndexOf(currentImage);

        if (currentIndex == -1) { return; }

        if (currentIndex == 0)
        {
            ImageURL = _accommodation.Images.Last().Path;
        }
        else
        {
            ImageURL = _accommodation.Images[currentIndex - 1].Path;
        }
    }

    public void RightArrowClick()
    {
        var currentImage = _accommodation.Images.FirstOrDefault(image => image.Path == ImageURL);
        var currentIndex = _accommodation.Images.IndexOf(currentImage);

        if (currentIndex == -1) { return; }

        if (currentIndex == _accommodation.Images.Count - 1)
        {
            ImageURL = _accommodation.Images.First().Path;
        }
        else
        {
            ImageURL = _accommodation.Images[currentIndex + 1].Path;
        }
    }
}
