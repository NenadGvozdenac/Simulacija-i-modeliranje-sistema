using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BookingApp.View.OwnerViews;

public partial class DetailedAccommodationPage : Page
{
    private DetailedAccommodationViewModel _viewModel;
    public DetailedAccommodationPage(Accommodation accommodation)
    {
        InitializeComponent();

        _viewModel = new DetailedAccommodationViewModel(accommodation);
        DataContext = _viewModel;

        SetupProperties();
    }

    private void SetupProperties()
    {
        ImagesPanel.Children.Clear();
        LoadImages(_viewModel.Accommodation.Images);
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
        ImagesPanel.Children.Add(ImageService.GetInstance().ReadImage(absoluteImagePath));
    }

    private void NavigateToPreviousPage()
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }

    private void Back_Click(object sender, MouseButtonEventArgs e)
    {
        NavigateToPreviousPage();
    }
}