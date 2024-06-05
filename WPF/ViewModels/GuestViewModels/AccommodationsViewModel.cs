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
using System.Threading;
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
    private bool isDemoModeActive = false;
    private CancellationTokenSource demoCancellationTokenSource;

    public event EventHandler AnywhereAnytimeClicked;

    public User user { get; set; }

    int minvalueGuestNumber = 1,
    maxvalueGuestNumber = 30,
    startvalueGuestNumber = 1;

    int minvalueDaysOfStay = 0,
    maxvalueDaysOfStay = 30,
    startvalueDaysOfStay = 0;

    public AccommodationsViewModel(Accommodations _Accommodations, User _user)
    {
        AccommodationView = _Accommodations;
        user = _user;

        _accommodations = new ObservableCollection<Accommodation>();
        _filteredAccommodations = new ObservableCollection<Accommodation>();
        Update();

    }

    public void AnywhereAnytime_Click()
    {
        AnywhereAnytimeClicked?.Invoke(this, EventArgs.Empty);
    }

    public void Update()
    {

        if (GuestService.GetInstance().GetByGuestId(user.Id).IsSuperGuest)
        {
            AccommodationView.crownImage.Visibility = Visibility.Visible;
        }
        else
        {
            AccommodationView.crownImage.Visibility = Visibility.Hidden;
        }

        foreach (Accommodation accommodation in AccommodationService.GetInstance().GetAll())
        {
            accommodation.Location = LocationService.GetInstance().GetById(accommodation.LocationId);
            accommodation.Images = ImageService.GetInstance().GetImagesByAccommodationId(accommodation.Id);

            _accommodations.Add(accommodation);
        }

        AccommodationView.username.Content = user.Username;
        AccommodationView.DaysOfStay.Text = startvalueDaysOfStay.ToString();
        AccommodationView.GuestNumber.Text = startvalueGuestNumber.ToString();
        LoadCountries();
        FilteredAccommodations = new ObservableCollection<Accommodation>(_accommodations);
        SortBySuperAccommodations();
    }

    public async Task StartDemoMode()
    {
        // Simulate user interactions to demonstrate functionality

        demoCancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = demoCancellationTokenSource.Token;

        isDemoModeActive = true;

        try
        {

            while (isDemoModeActive)
            {
                AccommodationView.Overlay.Visibility = Visibility.Visible;
                AccommodationView.SearchBox_transparent.Visibility = Visibility.Hidden;
                AccommodationView.NavBar_transparent.Visibility = Visibility.Visible;
                AccommodationView.Search_transparent.Visibility = Visibility.Hidden;
                AccommodationView.Sticker_transparent.Visibility = Visibility.Visible;
                AccommodationView.Location_transparent.Visibility = Visibility.Visible;
                AccommodationView.Type_transparent.Visibility = Visibility.Visible;
                AccommodationView.GuestsNumber_transparent.Visibility = Visibility.Visible;
                AccommodationView.DaysOfStay_transparent.Visibility = Visibility.Visible;
                AccommodationView.ResetFilters_transparent.Visibility = Visibility.Visible;


                SearchAccommodation = "K";
                await Task.Delay(1000, cancellationToken); 
                SearchAccommodation = "Ku";
                await Task.Delay(1000, cancellationToken); 
                SearchAccommodation = "Kuc";
                await Task.Delay(1000, cancellationToken); 
                SearchAccommodation = "Kuca";
                await Task.Delay(2000, cancellationToken); // Wait for 3 seconds
                AccommodationView.SearchBox_transparent.Visibility = Visibility.Visible;


                AccommodationView.Location_transparent.Visibility = Visibility.Hidden;
                await Task.Delay(2000, cancellationToken); // Wait for 2 seconds
                AccommodationView.CountryComboBox.SelectedIndex = 0; // Select first country
                await Task.Delay(2000, cancellationToken); // Wait for 2 seconds      
                AccommodationView.CityComboBox.SelectedIndex = 5; // Select Sombor
                await Task.Delay(2000, cancellationToken);
                AccommodationView.Location_transparent.Visibility = Visibility.Visible;


                AccommodationView.Type_transparent.Visibility = Visibility.Hidden;
                await Task.Delay(2000, cancellationToken);
                AccommodationView.checkBoxHouse.IsChecked = true; // Check apartment checkbox
                await Task.Delay(2000, cancellationToken);
                AccommodationView.Type_transparent.Visibility = Visibility.Visible;

                AccommodationView.GuestsNumber_transparent.Visibility = Visibility.Hidden;
                await Task.Delay(2000, cancellationToken);
                AccommodationView.GuestNumber.Text = "3";
                await Task.Delay(2000, cancellationToken);
                AccommodationView.GuestsNumber_transparent.Visibility = Visibility.Visible;


                AccommodationView.DaysOfStay_transparent.Visibility = Visibility.Hidden;
                await Task.Delay(2000, cancellationToken);
                AccommodationView.DaysOfStay.Text = "5";
                await Task.Delay(2000, cancellationToken);
                AccommodationView.DaysOfStay_transparent.Visibility = Visibility.Visible;

                AccommodationView.ResetFilters_transparent.Visibility = Visibility.Hidden;
                await Task.Delay(2000, cancellationToken);
                ResetFilters_Click();
                AccommodationView.ResetFilters_transparent.Visibility = Visibility.Visible;

                cancellationToken.ThrowIfCancellationRequested();

                // Delay before next iteration
                await Task.Delay(1000, cancellationToken); // Wait for 1 second
            }
        }
        catch (OperationCanceledException)
        {
            ResetDemoMode();
            isDemoModeActive = false;
        }
    }

    private void ResetDemoMode()
    {
        ResetFilters_Click();
        // Reset any changes made during demo mode
        AccommodationView.Overlay.Visibility = Visibility.Collapsed;
        // Hide other transparent elements
        AccommodationView.SearchBox_transparent.Visibility = Visibility.Collapsed;
        AccommodationView.NavBar_transparent.Visibility = Visibility.Collapsed;
        AccommodationView.Search_transparent.Visibility = Visibility.Collapsed;
        AccommodationView.Sticker_transparent.Visibility = Visibility.Collapsed;
        AccommodationView.Location_transparent.Visibility = Visibility.Collapsed;
        AccommodationView.Type_transparent.Visibility = Visibility.Collapsed;
        AccommodationView.GuestsNumber_transparent.Visibility = Visibility.Collapsed;
        AccommodationView.DaysOfStay_transparent.Visibility = Visibility.Collapsed;
        AccommodationView.ResetFilters_transparent.Visibility = Visibility.Collapsed;
    }

    public void Overlay_MouseLeftButtonDown()
    {
            StopDemoMode();
    }

    public void StopDemoMode()
    {
        // Reset any changes made during demo mode
        demoCancellationTokenSource?.Cancel();
        
        AccommodationView.Overlay.Visibility = Visibility.Collapsed;
        // Set flag to indicate demo mode is inactive
        isDemoModeActive = false;
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
        SortBySuperAccommodations();
    }

    private void SortBySuperAccommodations()
    {
        var sortedAccommodations = FilteredAccommodations
            .OrderByDescending(accommodation => OwnerService.GetInstance().GetById(accommodation.OwnerId).Item1.IsSuperOwner)
            .ToList();

        FilteredAccommodations.Clear();
        foreach (var accommodation in sortedAccommodations)
        {
            FilteredAccommodations.Add(accommodation);
        }
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
