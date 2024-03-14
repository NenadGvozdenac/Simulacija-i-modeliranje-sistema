using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.View.OwnerViews
{
    public partial class AddAccommodationPage : Page, INotifyPropertyChanged
    {
        private User _user;

        private LocationRepository _locationRepository;
        private AccommodationRepository _accommodationRepository;
        private AccommodationImageRepository _imageRepository;

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

        public event EventHandler PageClosed;

        private void ClosePage()
        {
            PageClosed?.Invoke(this, EventArgs.Empty);
        }

        public AddAccommodationPage(User user, AccommodationRepository accommodationRepository, AccommodationImageRepository accommodationImageRepository)
        {
            _user = user;
            _locationRepository = new LocationRepository();
            _accommodationRepository = accommodationRepository;
            _imageRepository = accommodationImageRepository;
            Images = new ObservableCollection<AccommodationImage>();

            DataContext = this;

            InitializeComponent();
            LoadCountries();
            LoadTypesOfAccommodations();
        }

        private void LoadTypesOfAccommodations()
        {
            AccommodationTypes = new ObservableCollection<string>(Enum.GetNames(typeof(AccommodationType)));
        }

        private void LoadCountries()
        {
            Countries = new ObservableCollection<string>(_locationRepository.GetCountries());
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (!IsDataValid())
            {
                return;
            }

            AddAccommodation();

            NavigateToPreviousPage();
        }

        private void AddAccommodation()
        {
            Accommodation accommodation = new();
            accommodation.Id = _accommodationRepository.NextId();
            accommodation.Name = AccommodationName;
            accommodation.LocationId = _locationRepository.GetLocationByCityAndCountry(City, Country).Id;
            accommodation.Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), Type);
            accommodation.MaxGuestNumber = MaximumNumberOfGuests;
            accommodation.MinReservationDays = MinimumNumberOfDaysForReservation;
            accommodation.CancellationPeriodDays = DaysBeforeReservationIsFinal;
            accommodation.Price = AccommodationPrice;
            accommodation.OwnerId = _user.Id;
            accommodation.Images = Images.ToList();
            _accommodationRepository.Add(accommodation);

            foreach (AccommodationImage image in Images)
            {
                image.Id = _imageRepository.NextId();
                image.AccommodationId = accommodation.Id;
                _imageRepository.Add(image);
            }

            NavigateToPreviousPage();
        }

        private bool IsDataValid()
        {
            return !string.IsNullOrEmpty(AccommodationName)
                && !string.IsNullOrEmpty(Country)
                && !string.IsNullOrEmpty(City)
                && !string.IsNullOrEmpty(Type)
                && MaximumNumberOfGuests > 0
                && MinimumNumberOfDaysForReservation > 0
                && DaysBeforeReservationIsFinal > 0;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateToPreviousPage();
        }

        private void NavigateToPreviousPage()
        {
            if (NavigationService.CanGoBack)
            {
                ClosePage();
                NavigationService.GoBack();
            }
        }

        private void CountryTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cities = new ObservableCollection<string>(_locationRepository.GetCitiesByCountry(CountryTextBox.SelectedItem.ToString()));
            CityTextBox.IsDropDownOpen = true;
            CityTextBox.IsEnabled = true;
        }

        private void AddURLClick(object sender, RoutedEventArgs e)
        {
            if (!IsImageValid())
            {
                return;
            }

            AddImage();
        }

        private void AddImage()
        {
            AccommodationImage image = new();
            image.Path = ImageURL;
            Images.Add(image);
            ImageURLTextBox.Clear();
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

        private void ImageURLTextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                string destinationFolder = "../../../Resources/Images/AccommodationImages/"; // Update this with your desired destination folder path

                string fileName = System.IO.Path.GetFileName(selectedImagePath);

                string destinationPath = System.IO.Path.Combine(destinationFolder, fileName);

                // Uncomment to add files from other folders.
                // System.IO.File.Copy(selectedImagePath, destinationPath, true);

                ImageURLTextBox.Text = destinationPath;
            }
        }
    }
}