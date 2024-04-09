using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public class AddAccommodationViewModel : INotifyPropertyChanged
{
    private User _user;

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

    public AddAccommodationViewModel(User user)
    {
        _user = user;
        Images = new ObservableCollection<AccommodationImage>();
        LoadTypesOfAccommodations();
        LoadCountries();
    }

    private void LoadTypesOfAccommodations()
    {
        AccommodationTypes = new ObservableCollection<string>(Enum.GetNames(typeof(AccommodationType)));
    }

    private void LoadCountries()
    {
        Countries = new ObservableCollection<string>(LocationService.GetInstance().GetCountries());
    }

    public bool AddAccommodation()
    {
        if (!IsDataValid())
        {
            return false;
        }

        Accommodation accommodation = new();
        accommodation.Name = AccommodationName;
        accommodation.LocationId = LocationService.GetInstance().GetLocationByCityAndCountry(City, Country).Id;
        accommodation.Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), Type);
        accommodation.MaxGuestNumber = MaximumNumberOfGuests;
        accommodation.MinReservationDays = MinimumNumberOfDaysForReservation;
        accommodation.CancellationPeriodDays = DaysBeforeReservationIsFinal;
        accommodation.Price = AccommodationPrice;
        accommodation.OwnerId = _user.Id;
        accommodation.Images = Images.ToList();

        AccommodationService.GetInstance().Add(accommodation);

        return true;
    }
    private bool IsDataValid()
    {
        return AreAllStringsFilled() && AreAllNumbersOK();
    }

    private bool AreAllNumbersOK()
    {
        return MaximumNumberOfGuests > 0
            && MinimumNumberOfDaysForReservation > 0
            && DaysBeforeReservationIsFinal > 0;
    }

    private bool AreAllStringsFilled()
    {
        return !string.IsNullOrEmpty(AccommodationName)
            && !string.IsNullOrEmpty(Country)
            && !string.IsNullOrEmpty(City)
            && !string.IsNullOrEmpty(Type);
    }

    public void AddImage()
    {
        if (!IsImageValid())
        {
            return;
        }

        AccommodationImage image = new();
        image.Path = ImageURL;
        Images.Add(image);
    }

    private bool IsImageValid()
    {
        return !(string.IsNullOrEmpty(ImageURL) || ImageAlreadyExists());
    }

    private bool ImageAlreadyExists()
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
}
