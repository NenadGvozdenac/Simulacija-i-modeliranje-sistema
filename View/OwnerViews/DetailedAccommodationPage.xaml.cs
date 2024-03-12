using BookingApp.Model.MutualModels;
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
    private Accommodation _accommodation;

    private string accommodationName;
    public string AccommodationName
    {
        get { return accommodationName; }
        set
        {
            accommodationName = value;
            OnPropertyChanged("AccommodationName");
        }
    }
    private string costPerNight;
    public string CostPerNight
    {
        get { return costPerNight; }
        set
        {
            costPerNight = value;
            OnPropertyChanged("CostPerNight");
        }
    }

    private string location;
    public string Location
    {
        get { return location; }
        set
        {
            location = value;
            OnPropertyChanged("Location");
        }
    }

    private string typeOfAccommodation;
    public string TypeOfAccommodation
    {
        get { return typeOfAccommodation; }
        set
        {
            typeOfAccommodation = value;
            OnPropertyChanged("TypeOfAccommodation");
        }
    }

    private string minimumReservationDays;
    public string MinimumReservationDays
    {
        get { return minimumReservationDays; }
        set
        {
            minimumReservationDays = value;
            OnPropertyChanged("MinimumReservationDays");
        }
    }

    private string daysBeforeCancellationIsFinal;
    public string DaysBeforeCancellationIsFinal
    {
        get { return daysBeforeCancellationIsFinal; }
        set
        {
            daysBeforeCancellationIsFinal = value;
            OnPropertyChanged("DaysBeforeCancellationIsFinal");
        }
    }

    private string maximumNumberOfGuests;
    public string MaximumNumberOfGuests
    {
        get { return maximumNumberOfGuests; }
        set
        {
            maximumNumberOfGuests = value;
            OnPropertyChanged("MaximumNumberOfGuests");
        }
    }

    private string currentRating;
    public string CurrentRating
    {
        get { return currentRating; }
        set
        {
            currentRating = value;
            OnPropertyChanged("CurrentRating");
        }
    }

    private string numberOfReviews;
    public string NumberOfReviews
    {
        get { return numberOfReviews; }
        set
        {
            numberOfReviews = value;
            OnPropertyChanged("NumberOfReviews");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    // The method to invoke the property changed event
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public DetailedAccommodationPage(Accommodation accommodation)
    {
        InitializeComponent();
        _accommodation = accommodation;
        DataContext = this;
        SetupProperties();
    }

    private void SetupProperties()
    {
        AccommodationName = string.Format("{0}", _accommodation.Name);
        CostPerNight = string.Format("${0} per night", _accommodation.Price.ToString());
        Location = string.Format("{0}", _accommodation.Location.ToString());
        TypeOfAccommodation = string.Format("{0}", _accommodation.Type.ToString());
        MinimumReservationDays = string.Format("{0} days", _accommodation.MinReservationDays.ToString());
        DaysBeforeCancellationIsFinal = string.Format("{0} days", _accommodation.CancellationPeriodDays.ToString());
        MaximumNumberOfGuests = string.Format("{0} guests", _accommodation.MaxGuestNumber.ToString());
        CurrentRating = string.Format("{0} / 10.00", _accommodation.AverageReviewScore.ToString());
        NumberOfReviews = string.Format("{0} reviews", "0"); // TODO: Implement this

        ImagesPanel.Children.Clear();
        LoadImages(_accommodation.Images);
    }

    private void LoadImages(List<AccommodationImage> images)
    {
        foreach (var image in images)
        {
            string relativeImagePath = image.Path;
            string absoluteImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeImagePath);

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
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}