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

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for Accommodations.xaml
    /// </summary>
    public partial class Accommodations : UserControl, INotifyPropertyChanged
    {

        public ObservableCollection<Accommodation> _accommodations { get; set; }
        
        public AccommodationRepository accomodationrepository { get; set; }
        public LocationRepository locationrepository { get; set; }
        public AccommodationImageRepository accommodationimagerepository { get; set; }

        int minvalueGuestNumber = 1,
        maxvalueGuestNumber = 30,
        startvalueGuestNumber = 1;

        int minvalueDaysOfStay = 0,
        maxvalueDaysOfStay = 30,
        startvalueDaysOfStay = 0;

        public Accommodations()
        {
            InitializeComponent();
            DataContext = this;

            _accommodations = new ObservableCollection<Accommodation>();
            _filteredAccommodations = new ObservableCollection<Accommodation>();

            accomodationrepository = new AccommodationRepository();
            locationrepository = new LocationRepository();
            accommodationimagerepository = new AccommodationImageRepository();


            Update();

            //SearchAccommodation
            DaysOfStay.Text = startvalueDaysOfStay.ToString();
            GuestNumber.Text = startvalueGuestNumber.ToString();
            FilteredAccommodations = new ObservableCollection<Accommodation>(_accommodations);
            //
        }

        public void Update() 
        {                
            foreach (Accommodation accom in accomodationrepository.GetAll())
            {
                accom.Location = locationrepository.GetById(accom.LocationId);
                accom.Images = accommodationimagerepository.GetImagesByAccommodationId(accom.Id);

                _accommodations.Add(accom);
            }
            LoadCountries();
        }


        ////FILTERS FOR SEARCHING ACCOMMODATIONS
        //
        
        private void LoadCountries()
        {
            List<string> listOfCountries = locationrepository.GetCountries();
            CountryComboBox.ItemsSource = listOfCountries;
        }

        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> listOfCities = locationrepository.GetCitiesByCountry(CountryComboBox.SelectedItem.ToString());
            CityComboBox.ItemsSource = listOfCities;
            CityComboBox.Focus();
            CityComboBox.IsEnabled = true;
            FilterAccommodations();
        }

        private void FilterAccommodations()
        {
            //Filtering Types
            var checkedTypes = new List<string>();
            if (checkBoxApartment.IsChecked == true)
                checkedTypes.Add("Apartment");
            if (checkBoxHouse.IsChecked == true)
                checkedTypes.Add("House");
            if (checkBoxShack.IsChecked == true)
                checkedTypes.Add("Shack");

            //Filtering countries and cities
            string selectedCountry = CountryComboBox.SelectedItem?.ToString();
            string selectedCity = CityComboBox.SelectedItem?.ToString();

            int selectedGuestNumber = 0;
            if (!string.IsNullOrEmpty(GuestNumber.Text))
            {
                selectedGuestNumber = Convert.ToInt32(GuestNumber.Text);
            }

            int selectedDaysOfStay = 0;
            if (!string.IsNullOrEmpty(DaysOfStay.Text))
            {
                selectedDaysOfStay = Convert.ToInt32(DaysOfStay.Text);
            }

            //Filtering Logic 
            if ((checkedTypes.Count == 0 && string.IsNullOrWhiteSpace(selectedCountry) && 
                string.IsNullOrWhiteSpace(selectedCity) && 
                string.IsNullOrWhiteSpace(_searchAccommodation)) && 
                selectedGuestNumber == 1 &&
                selectedDaysOfStay == 0)
            {
                FilteredAccommodations = _accommodations;
            }
            else
            {
                FilteredAccommodations = new ObservableCollection<Accommodation>(
                _accommodations.Where(accommodation =>
                //Checks for types
                (checkedTypes.Count == 0 || checkedTypes.Contains(accommodation.Type.ToString())) &&
                //Checks for countries and cities
                (string.IsNullOrWhiteSpace(selectedCountry) || accommodation.Location.Country == selectedCountry) &&
                (string.IsNullOrWhiteSpace(selectedCity) || accommodation.Location.City == selectedCity) &&
                //Checks for name
                (string.IsNullOrWhiteSpace(_searchAccommodation) || accommodation.Name.ToLower().Contains(_searchAccommodation.ToLower())) &&
                //Checks for GuestNumber
                (selectedGuestNumber == 1 || selectedGuestNumber <= accommodation.MaxGuestNumber) &&
                (selectedDaysOfStay == 0 || selectedDaysOfStay >= accommodation.MinReservationDays)));
            }
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FilterAccommodations();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            FilterAccommodations();
        }

        //
        ////END OF FILTERING
        //
        
        //Custom Num Picker

        //GuestNumber
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

        private void GuestNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                GuestNumberUp.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(GuestNumberUp, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                GuestNumberDown.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(GuestNumberDown, new object[] { true });
            }
        }

        private void GuestNumber_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(GuestNumberUp, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(GuestNumberDown, new object[] { false });
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

        //DaysOfStay
        private void DaysOfStay_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                DaysOfStayUp.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(DaysOfStayUp, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                DaysOfStayDown.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(DaysOfStayDown, new object[] { true });
            }
        }

        private void DaysOfStay_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(DaysOfStayUp, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(DaysOfStayDown, new object[] { false });
        }

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
    }
}
