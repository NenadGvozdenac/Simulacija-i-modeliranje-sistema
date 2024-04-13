using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.GuestViews;
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

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class AccommodationsViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Accommodation> _accommodations { get; set; }
    public Accommodations AccommodationView { get; set; }

    int minvalueGuestNumber = 1,
    maxvalueGuestNumber = 30,
    startvalueGuestNumber = 1;

    int minvalueDaysOfStay = 0,
    maxvalueDaysOfStay = 30,
    startvalueDaysOfStay = 0;

    public AccommodationsViewModel(Accommodations _Accommodations, User user)
    {
        AccommodationView = _Accommodations;


        //ObservableCollections
        _accommodations = new ObservableCollection<Accommodation>();
        _filteredAccommodations = new ObservableCollection<Accommodation>();

        Update();

    }

    public void Update()
    {
        foreach (Accommodation accommodation in AccommodationService.GetInstance().GetAll())
        {
            accommodation.Location = LocationService.GetInstance().GetById(accommodation.LocationId);
            accommodation.Images = ImageService.GetInstance().GetImagesByAccommodationId(accommodation.Id);

            _accommodations.Add(accommodation);
        }

        AccommodationView.DaysOfStay.Text = startvalueDaysOfStay.ToString();
        AccommodationView.GuestNumber.Text = startvalueGuestNumber.ToString();
        LoadCountries();
        FilteredAccommodations = new ObservableCollection<Accommodation>(_accommodations);
    }

    public void ResetFilters_Click()
    {
        AccommodationView.SearchBox_TextBox.Text = string.Empty;
        AccommodationView.CountryComboBox.Text = "";
        AccommodationView.CityComboBox.IsEnabled = false;
        AccommodationView.CityComboBox.Text = "";
        AccommodationView.checkBoxApartment.IsChecked = false;
        AccommodationView.checkBoxHouse.IsChecked = false;
        AccommodationView.checkBoxShack.IsChecked = false;
        AccommodationView.GuestNumber.Text = "1";
        AccommodationView.DaysOfStay.Text = "0";
    }
    //Filtering Accommodations
    public void FilterAccommodations()
    {
        //types
        var checkedTypes = GetCheckedTypes();

        //countries and cities
        string selectedCountry = AccommodationView.CountryComboBox.SelectedItem?.ToString();
        string selectedCity = AccommodationView.CityComboBox.SelectedItem?.ToString();

        //guest number
        int selectedGuestNumber;
        int.TryParse(AccommodationView.GuestNumber.Text, out selectedGuestNumber);

        //days of staying
        int selectedDaysOfStay;
        int.TryParse(AccommodationView.DaysOfStay.Text, out selectedDaysOfStay);

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

        if (AccommodationView.checkBoxApartment.IsChecked == true)
            checkedTypes.Add("Apartment");
        if (AccommodationView.checkBoxHouse.IsChecked == true)
            checkedTypes.Add("House");
        if (AccommodationView.checkBoxShack.IsChecked == true)
            checkedTypes.Add("Shack");

        return checkedTypes;
    }

    //Countries
    private void LoadCountries()
    {
        List<string> listOfCountries = LocationService.GetInstance().GetCountries();
        AccommodationView.CountryComboBox.ItemsSource = listOfCountries;
    }

    public void CountryComboBox_SelectionChanged()
    {
        if (AccommodationView.CountryComboBox.SelectedItem == null)
        {
            FilterAccommodations();
            return;
        }
        List<string> listOfCities = LocationService.GetInstance().GetCitiesByCountry(AccommodationView.CountryComboBox.SelectedItem.ToString());
        AccommodationView.CityComboBox.ItemsSource = listOfCities;
        AccommodationView.CityComboBox.Focus();
        AccommodationView.CityComboBox.IsEnabled = true;
        FilterAccommodations();
    }


    //Accommodation Types CheckBoxes
    public void CheckBox_Checked()
    {
        FilterAccommodations();
    }
    public void CheckBox_Unchecked()
    {
        FilterAccommodations();
    }

    //GuestNumber NumPicker
    public void GuestNumberUp_Click()
    {
        int number;
        if (AccommodationView.GuestNumber.Text != "") number = Convert.ToInt32(AccommodationView.GuestNumber.Text);
        else number = 0;
        if (number < maxvalueGuestNumber)
            AccommodationView.GuestNumber.Text = Convert.ToString(number + 1);
    }
    public void GuestNumberDown_Click()
    {
        int number;
        if (AccommodationView.GuestNumber.Text != "") number = Convert.ToInt32(AccommodationView.GuestNumber.Text);
        else number = 0;
        if (number > minvalueGuestNumber)
            AccommodationView.GuestNumber.Text = Convert.ToString(number - 1);
    }
    public void GuestNumber_TextChanged()
    {
        int number = 0;
        if (AccommodationView.GuestNumber.Text != "")
            if (!int.TryParse(AccommodationView.GuestNumber.Text, out number)) AccommodationView.GuestNumber.Text = startvalueGuestNumber.ToString();
        if (number > maxvalueGuestNumber) AccommodationView.GuestNumber.Text = maxvalueGuestNumber.ToString();
        if (number < minvalueGuestNumber) AccommodationView.GuestNumber.Text = minvalueGuestNumber.ToString();
        AccommodationView.GuestNumber.SelectionStart = AccommodationView.GuestNumber.Text.Length;
        FilterAccommodations();
    }

    //DaysOfStay NumPicker
    public void DaysOfStayUp_Click()
    {
        int number;
        if (AccommodationView.DaysOfStay.Text != "") number = Convert.ToInt32(AccommodationView.DaysOfStay.Text);
        else number = 0;
        if (number < maxvalueDaysOfStay)
            AccommodationView.DaysOfStay.Text = Convert.ToString(number + 1);
    }
    public void DaysOfStayDown_Click()
    {
        int number;
        if (AccommodationView.DaysOfStay.Text != "") number = Convert.ToInt32(AccommodationView.DaysOfStay.Text);
        else number = 0;
        if (number > minvalueDaysOfStay)
            AccommodationView.DaysOfStay.Text = Convert.ToString(number - 1);
    }
    public void DaysOfStay_TextChanged()
    {
        int number = 0;
        if (AccommodationView.DaysOfStay.Text != "")
            if (!int.TryParse(AccommodationView.DaysOfStay.Text, out number)) AccommodationView.DaysOfStay.Text = startvalueDaysOfStay.ToString();
        if (number > maxvalueDaysOfStay) AccommodationView.DaysOfStay.Text = maxvalueDaysOfStay.ToString();
        if (number < minvalueDaysOfStay) AccommodationView.DaysOfStay.Text = minvalueDaysOfStay.ToString();
        AccommodationView.DaysOfStay.SelectionStart = AccommodationView.DaysOfStay.Text.Length;
        FilterAccommodations();
    }


    private string _searchAccommodation;
    public string SearchAccommodation
    {
        get { return _searchAccommodation; }
        set
        {
            if (_searchAccommodation != value)
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
