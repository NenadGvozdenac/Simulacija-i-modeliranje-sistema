using BookingApp.Application.Commands;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class AddAccommodationViewModel : INotifyPropertyChanged
{
    private User _user;
    public User User { get => _user; set => _user = value; }

    private ObservableCollection<string> _accommodationTypes;
    public ObservableCollection<string> AccommodationTypes
    {
        get { return _accommodationTypes; }
        set
        {
            if (value != _accommodationTypes)
            {
                _accommodationTypes = value;
                OnPropertyChanged(nameof(AccommodationTypes));
            }
        }
    }

    private ObservableCollection<string> countries;
    public ObservableCollection<string> Countries
    {
        get { return countries; }
        set
        {
            if (value != countries)
            {
                countries = value;
                OnPropertyChanged(nameof(Countries));
            }
        }
    }

    private ObservableCollection<string> cities;
    public ObservableCollection<string> Cities
    {
        get { return cities; }
        set
        {
            if (value != cities)
            {
                cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }
    }

    private string name;
    public string AccommodationName
    {
        get => name;
        set
        {
            if (value != name)
            {
                name = value;
                OnPropertyChanged(nameof(AccommodationName));
            }
        }
    }

    private string country;
    public string Country
    {
        get => country;
        set
        {
            if (value != country)
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
    }

    private string city;
    public string City
    {
        get => city;
        set
        {
            if (value != city)
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }
    }

    private string type;
    public string Type
    {
        get => type;
        set
        {
            if (value != type)
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
    }

    private int accommodationPrice;
    public int AccommodationPrice
    {
        get => accommodationPrice;
        set
        {
            if (value != accommodationPrice)
            {
                accommodationPrice = value;
                OnPropertyChanged(nameof(AccommodationPrice));
            }
        }
    }

    private int maximumNumberOfGuests;
    public int MaximumNumberOfGuests
    {
        get => maximumNumberOfGuests;
        set
        {
            if (value != maximumNumberOfGuests)
            {
                maximumNumberOfGuests = value;
                OnPropertyChanged(nameof(MaximumNumberOfGuests));
            }
        }
    }

    private int minimumNumberOfDaysForReservation;
    public int MinimumNumberOfDaysForReservation
    {
        get => minimumNumberOfDaysForReservation;
        set
        {
            if (value != minimumNumberOfDaysForReservation)
            {
                minimumNumberOfDaysForReservation = value;
                OnPropertyChanged(nameof(MinimumNumberOfDaysForReservation));
            }
        }
    }

    private int daysBeforeReservationIsFinal;
    public int DaysBeforeReservationIsFinal
    {
        get => daysBeforeReservationIsFinal;
        set
        {
            if (value != daysBeforeReservationIsFinal)
            {
                daysBeforeReservationIsFinal = value;
                OnPropertyChanged(nameof(DaysBeforeReservationIsFinal));
            }
        }
    }

    private string imageURL;
    public string ImageURL
    {
        get => imageURL;
        set
        {
            if (value != imageURL)
            {
                imageURL = value;
                OnPropertyChanged(nameof(ImageURL));
            }
        }
    }

    public ObservableCollection<AccommodationImage> Images { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public AddAccommodationPage Page;
    public ICommand AddCommand => new AddAccommodationCommand(this);
    public ICommand CancelCommand => new NavigateToPreviousPageCommand(Page);
    public ICommand AddImageCommand => new AddImageCommand(this);

    public AddAccommodationViewModel(AddAccommodationPage page, User user)
    {
        User = user;
        Page = page;

        Images = new ObservableCollection<AccommodationImage>();

        LoadTypesOfAccommodations();
        LoadCountries();
    }

    public void LoadTypesOfAccommodations()
    {
        AccommodationTypes = new ObservableCollection<string>(Enum.GetNames(typeof(AccommodationType)));
    }

    public void LoadCountries()
    {
        Countries = new ObservableCollection<string>(LocationService.GetInstance().GetCountries());
    }

    public bool IsDataValid()
    {
        return AreAllStringsFilled() && AreAllNumbersOK();
    }

    public bool AreAllNumbersOK()
    {
        return MaximumNumberOfGuests > 0
            && MinimumNumberOfDaysForReservation > 0
            && DaysBeforeReservationIsFinal > 0;
    }

    public bool AreAllStringsFilled()
    {
        return !string.IsNullOrEmpty(AccommodationName)
            && !string.IsNullOrEmpty(Country)
            && !string.IsNullOrEmpty(City)
            && !string.IsNullOrEmpty(Type);
    }
    public bool IsImageValid()
    {
        return !(string.IsNullOrEmpty(ImageURL) || ImageAlreadyExists());
    }

    public bool ImageAlreadyExists()
    {
        foreach (AccommodationImage image in Images)
        {
            if (image.Path.Equals(ImageURL))
            {
                return true;
            }
        }

        return false;
    }

    public void CountryChanged()
    {
        var cities = LocationService.GetInstance().GetCitiesByCountry(Page.CountryTextBox.SelectedItem.ToString());
        Cities = new ObservableCollection<string>(cities);

        Page.CityTextBox.IsDropDownOpen = true;
        Page.CityTextBox.IsEnabled = true;
    }

    public void ImageSelected()
    {
        string imagePath = ImageService.GetInstance().GetImageFromUser();
        ImageURL = imagePath;
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
