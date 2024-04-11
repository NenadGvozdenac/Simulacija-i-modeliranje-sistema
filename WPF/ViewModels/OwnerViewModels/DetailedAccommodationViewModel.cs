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

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class DetailedAccommodationViewModel
{
    public AccommodationDTO Accommodation { get; set; }
    public Accommodation _accommodation { get; set; }
    public DetailedAccommodationPage Page { get; set; }
    public ICommand CloseAccommodationCommand => new CloseAccommodationCommand(this);

    public DetailedAccommodationViewModel(DetailedAccommodationPage page, Accommodation accommodation)
    {
        Page = page;
        Accommodation = new(accommodation);
        _accommodation = accommodation;

        SetupProperties();
    }

    public void SetupProperties()
    {
        Page.ImagesPanel.Children.Clear();
        LoadImages(Accommodation.Images);
    }

    private void LoadImages(List<AccommodationImage> images)
    {
        foreach (AccommodationImage image in images)
        {
            AddImageToImagePanel(image);
        }
    }

    private void AddImageToImagePanel(AccommodationImage image)
    {
        string relativeImagePath = image.Path;
        string absoluteImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeImagePath);

        AddToPanel(absoluteImagePath);
    }

    private void AddToPanel(string absoluteImagePath)
    {
        Page.ImagesPanel.Children.Add(ImageService.GetInstance().ReadImage(absoluteImagePath));
    }
}
