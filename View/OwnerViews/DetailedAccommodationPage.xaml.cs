using BookingApp.Model.MutualModels;
using BookingApp.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.OwnerViews;

/// <summary>
/// Interaction logic for DetailedAccommodationPage.xaml
/// </summary>
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
        using (FileStream stream = new FileStream(absoluteImagePath, FileMode.Open, FileAccess.Read))
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            ImagesPanel.Children.Add(new Image
            {
                Source = bitmapImage,
                Width = 250,
                Height = 200,
                Margin = new Thickness(5),
                Stretch = Stretch.Fill
            });
        }
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