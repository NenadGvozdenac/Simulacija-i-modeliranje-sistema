using BookingApp.Application.Localization;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class ScheduleAccommodationForRenovationControl : UserControl
{
    public Accommodation Accommodation { get; }
    private DateSpan lastRenovationDate;
    private readonly string DEFAULT_HOUSE_PICTURE = "../../../Resources/Assets/default_house.png";

    public string LastRenovationDate
    {
        get => lastRenovationDate != null ? lastRenovationDate.ToString() : TranslationSource.Instance["NeverBeenRenovated"];
    }

    public ScheduleAccommodationForRenovationControl(Accommodation accommodation)
    {
        InitializeComponent();
        DataContext = this;
        Accommodation = accommodation;
        lastRenovationDate = AccommodationRenovationService.GetInstance().GetLastRenovationDate(accommodation.Id);
        LoadFirstImage();
    }

    private void LoadFirstImage()
    {
        var path = Accommodation.Images.Count > 0 ? Accommodation.Images[0].Path : DEFAULT_HOUSE_PICTURE;
        Image image = ImageService.GetInstance().ReadImage(path);
        Image.ImageSource = image.Source;
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DetailedScheduleRenovationPage detailedScheduleRenovationPage = new DetailedScheduleRenovationPage(Accommodation);
        NavigationService.GetNavigationService(this).Navigate(detailedScheduleRenovationPage);
    }
}
