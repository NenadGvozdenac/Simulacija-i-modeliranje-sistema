using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.View.GuestViews.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
            FilteredAccommodations = new ObservableCollection<Accommodation>(_accommodations);
            _searchAccommodation = string.Empty;
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
        }


        ////FILTERS FOR SEARCHING ACCOMMODATIONS
        //
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



            if (checkedTypes.Count == 0 && string.IsNullOrWhiteSpace(_searchAccommodation))
            {
                FilteredAccommodations = _accommodations;
            }
            else
            {
                FilteredAccommodations = new ObservableCollection<Accommodation>(
                    _accommodations.Where(accommodation =>
                    (checkedTypes.Count == 0 || checkedTypes.Contains(accommodation.Type.ToString())) && (string.IsNullOrWhiteSpace(_searchAccommodation) || accommodation.Name.ToLower().Contains(_searchAccommodation.ToLower()))));
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
    }
}
