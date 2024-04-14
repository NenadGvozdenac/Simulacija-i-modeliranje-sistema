using BookingApp.Application.Commands;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class AddAccommodationViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<string> _accommodationTypes;

    [ObservableProperty]
    private ObservableCollection<string> _countries;

    [ObservableProperty]
    public ObservableCollection<AccommodationImage> _images;

    [ObservableProperty]
    private User _user;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    public ObservableCollection<string> _cities;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private string _accommodationName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private string _country;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private string _city;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private string _type;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private int _accommodationPrice;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private int _maximumNumberOfGuests;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private int _minimumNumberOfDaysForReservation;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private int daysBeforeReservationIsFinal;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddImageCommand))]
    private string imageURL;

    public AddAccommodationPage Page;
    public ICommand AddCommand => new AddAccommodationCommand(this);
    public ICommand CancelCommand => new NavigateToPreviousPageCommand(Page);
    public ICommand AddImageCommand => new AddImageCommand(this);

    public AddAccommodationViewModel(AddAccommodationPage page, User user)
    {
        User = user;
        Page = page;

        AccommodationTypes = new ObservableCollection<string>(Enum.GetNames(typeof(AccommodationType)));
        Countries = new ObservableCollection<string>(LocationService.GetInstance().GetCountries());
        Images = new ObservableCollection<AccommodationImage>();
    }

    public void CountryChanged()
    {
        Cities = new(LocationService.GetInstance().GetCitiesByCountry(Page.CountryTextBox.SelectedItem.ToString()));

        Page.CityTextBox.IsDropDownOpen = true;
        Page.CityTextBox.IsEnabled = true;
    }

    public void ImageSelected()
    {
        ImageURL = ImageService.GetInstance().GetImageFromUser();
    }

    public void ClearPage()
    {
        AccommodationName = "";
        Page.CountryTextBox.SelectedIndex = 0;
        Page.CityTextBox.SelectedIndex = 0;
        Page.AccommodationType.SelectedIndex = 0;
        AccommodationPrice = 0;
        MaximumNumberOfGuests = 0;
        MinimumNumberOfDaysForReservation = 0;
        DaysBeforeReservationIsFinal = 0;
        ImageURL = "";
        Images.Clear();

        Page.ClosePage();
    }
}