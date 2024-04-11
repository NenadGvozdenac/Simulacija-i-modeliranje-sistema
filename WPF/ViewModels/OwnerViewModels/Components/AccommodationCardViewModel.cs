using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.OwnerViews.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModels.Components;

public class AccommodationCardViewModel
{
    private AccommodationControl accommodationControl;
    private readonly string DEFAULT_HOUSE_PICTURE = "../../../Resources/Assets/default_house.png";
    public Accommodation Accommodation { get; set; }
    public AccommodationCardViewModel(AccommodationControl accommodationControl, Accommodation accommodation)
    {
        this.accommodationControl = accommodationControl;
        Accommodation = accommodation;
        LoadFirstImage();
    }

    private void LoadFirstImage()
    {
        var path = Accommodation.Images.Count > 0 ? Accommodation.Images[0].Path : DEFAULT_HOUSE_PICTURE;
        Image image = ImageService.GetInstance().ReadImage(path);
        accommodationControl.Image.ImageSource = image.Source;
    }

    public void NavigateToDetails()
    {
        DetailedAccommodationPage detailedAccommodationPage = new(Accommodation);
        detailedAccommodationPage.AccommodationClosed += (s, e) => accommodationControl.AccommodationClosed();
        NavigationService.GetNavigationService(accommodationControl).Navigate(detailedAccommodationPage);
    }
}
