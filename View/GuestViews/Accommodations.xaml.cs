using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.View.GuestViews.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

namespace BookingApp.View.GuestViews;

public partial class Accommodations : UserControl, INotifyPropertyChanged
{

    public ObservableCollection<Accommodation> _accommodations { get; set; }  
    public AccommodationRepository accomodationRepository { get; set; }
    public LocationRepository locationRepository { get; set; }
    public AccommodationImageRepository accommodationImageRepository { get; set; }

    int minvalueGuestNumber = 1,
    maxvalueGuestNumber = 30,
    startvalueGuestNumber = 1;

    int minvalueDaysOfStay = 0,
    maxvalueDaysOfStay = 30,
    startvalueDaysOfStay = 0;

    public Accommodations(User user)
    {
        InitializeComponent();
        DataContext = this;
        username.Content = user.Username;

        //ObservableCollections
        _accommodations = new ObservableCollection<Accommodation>();
        _filteredAccommodations = new ObservableCollection<Accommodation>();

        //Repositories
        accomodationRepository = new AccommodationRepository();
        locationRepository = new LocationRepository();
        accommodationImageRepository = new AccommodationImageRepository();
        Update();

    }

    public void Update() 
    {  
        foreach (Accommodation accommodation in accomodationRepository.GetAll())
        {
            accommodation.Location = locationRepository.GetById(accommodation.LocationId);
            accommodation.Images = accommodationImageRepository.GetImagesByAccommodationId(accommodation.Id);

            _accommodations.Add(accommodation);
        }
        
        DaysOfStay.Text = startvalueDaysOfStay.ToString();
        GuestNumber.Text = startvalueGuestNumber.ToString();
        LoadCountries();       
        FilteredAccommodations = new ObservableCollection<Accommodation>(_accommodations);
    }


    //Filtering Accommodations
    private void FilterAccommodations()
    {
        //types
        var checkedTypes = GetCheckedTypes();

        //countries and cities
        string selectedCountry = CountryComboBox.SelectedItem?.ToString();
        string selectedCity = CityComboBox.SelectedItem?.ToString();

        //guest number
        int selectedGuestNumber = 0;
        int.TryParse(GuestNumber.Text, out selectedGuestNumber);

        //days of staying
        int selectedDaysOfStay = 0;
        int.TryParse(DaysOfStay.Text, out selectedDaysOfStay);

        //Filtering Logic 

        FilteringLogic(checkedTypes, selectedCountry, selectedCity, selectedGuestNumber, selectedDaysOfStay);

    }

    private void FilteringLogic(List<string> checkedTypes, string selectedCountry, string selectedCity, int selectedGuestNumber, int selectedDaysOfStay)
    {
        FilteredAccommodations = new ObservableCollection<Accommodation>(
                _accommodations.Where(accommodation =>
                    IsAccommodationTypeValid(accommodation, checkedTypes) &&
                    IsLocationValid(accommodation, selectedCountry, selectedCity) &&
                    IsNameValid(accommodation, _searchAccommodation) &&
                    IsGuestNumberValid(accommodation, selectedGuestNumber) &&
                    IsDaysOfStayValid(accommodation, selectedDaysOfStay)
                )
            );
    }

    private bool IsAccommodationTypeValid(Accommodation accommodation, List<string> checkedTypes)
    {
        return checkedTypes.Count == 0 || checkedTypes.Contains(accommodation.Type.ToString());
    }

    private bool IsLocationValid(Accommodation accommodation, string selectedCountry, string selectedCity)
    {
        return string.IsNullOrWhiteSpace(selectedCountry) || accommodation.Location.Country == selectedCountry &&
               string.IsNullOrWhiteSpace(selectedCity) || accommodation.Location.City == selectedCity;
    }

    private bool IsNameValid(Accommodation accommodation, string searchAccommodation)
    {
        return string.IsNullOrWhiteSpace(searchAccommodation) ||
               accommodation.Name.ToLower().Contains(searchAccommodation.ToLower());
    }

    private bool IsGuestNumberValid(Accommodation accommodation, int selectedGuestNumber)
    {
        return selectedGuestNumber == 1 || selectedGuestNumber <= accommodation.MaxGuestNumber;
    }

    private bool IsDaysOfStayValid(Accommodation accommodation, int selectedDaysOfStay)
    {
        return selectedDaysOfStay == 0 || selectedDaysOfStay >= accommodation.MinReservationDays;
    }

    private List<string> GetCheckedTypes()
    {
        var checkedTypes = new List<string>();

        if (checkBoxApartment.IsChecked == true)
            checkedTypes.Add("Apartment");
        if (checkBoxHouse.IsChecked == true)
            checkedTypes.Add("House");
        if (checkBoxShack.IsChecked == true)
            checkedTypes.Add("Shack");

        return checkedTypes;
    }

    //Countries
    private void LoadCountries()
    {
        List<string> listOfCountries = locationRepository.GetCountries();
        CountryComboBox.ItemsSource = listOfCountries;
    }

    private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        List<string> listOfCities = locationRepository.GetCitiesByCountry(CountryComboBox.SelectedItem.ToString());
        CityComboBox.ItemsSource = listOfCities;
        CityComboBox.Focus();
        CityComboBox.IsEnabled = true;
        FilterAccommodations();
    }
    

    //Accommodation Types CheckBoxes
    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        FilterAccommodations();
    }
    private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        FilterAccommodations();
    }

    //GuestNumber NumPicker
    private void GuestNumberUp_Click(object sender, RoutedEventArgs e)
    {
        int number;
        if (GuestNumber.Text != "") number = Convert.ToInt32(GuestNumber.Text);
        else number = 0;
        if (number < maxvalueGuestNumber)
            GuestNumber.Text = Convert.ToString(number + 1);
    }
    private void GuestNumberDown_Click(object sender, RoutedEventArgs e)
    {
        int number;
        if (GuestNumber.Text != "") number = Convert.ToInt32(GuestNumber.Text);
        else number = 0;
        if (number > minvalueGuestNumber)
            GuestNumber.Text = Convert.ToString(number - 1);
    }
    private void GuestNumber_TextChanged(object sender, TextChangedEventArgs e)
    {
        int number = 0;
        if (GuestNumber.Text != "")
            if (!int.TryParse(GuestNumber.Text, out number)) GuestNumber.Text = startvalueGuestNumber.ToString();
        if (number > maxvalueGuestNumber) GuestNumber.Text = maxvalueGuestNumber.ToString();
        if (number < minvalueGuestNumber) GuestNumber.Text = minvalueGuestNumber.ToString();
        GuestNumber.SelectionStart = GuestNumber.Text.Length;
        FilterAccommodations();
    }

    //DaysOfStay NumPicker
    private void DaysOfStayUp_Click(object sender, RoutedEventArgs e)
    {
        int number;
        if (DaysOfStay.Text != "") number = Convert.ToInt32(DaysOfStay.Text);
        else number = 0;
        if (number < maxvalueDaysOfStay)
            DaysOfStay.Text = Convert.ToString(number + 1);
    }
    private void DaysOfStayDown_Click(object sender, RoutedEventArgs e)
    {
        int number;
        if (DaysOfStay.Text != "") number = Convert.ToInt32(DaysOfStay.Text);
        else number = 0;
        if (number > minvalueDaysOfStay)
            DaysOfStay.Text = Convert.ToString(number - 1);
    }
    private void DaysOfStay_TextChanged(object sender, TextChangedEventArgs e)
    {
        int number = 0;
        if (DaysOfStay.Text != "")
            if (!int.TryParse(DaysOfStay.Text, out number)) DaysOfStay.Text = startvalueDaysOfStay.ToString();
        if (number > maxvalueDaysOfStay) DaysOfStay.Text = maxvalueDaysOfStay.ToString();
        if (number < minvalueDaysOfStay) DaysOfStay.Text = minvalueDaysOfStay.ToString();
        DaysOfStay.SelectionStart = DaysOfStay.Text.Length;
        FilterAccommodations();
    }
    
    
    private string _searchAccommodation;
    public string SearchAccommodation
    {
        get { return _searchAccommodation; }
        set
        {
            if(_searchAccommodation != value)
            {
                _searchAccommodation = value;
                FilterAccommodations();
                OnPropertyChanged();
            }
        }
    }

    private ObservableCollection<Accommodation> _filteredAccommodations;
    public ObservableCollection<Accommodation> FilteredAccommodations
    {
        get { return _filteredAccommodations; }
        set
        {
            if (_filteredAccommodations != value)
            {
                _filteredAccommodations = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
