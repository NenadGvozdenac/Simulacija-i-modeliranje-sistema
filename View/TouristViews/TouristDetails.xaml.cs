    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Channels;
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
    using BookingApp.Model.MutualModels;
    using BookingApp.Repository;
    using BookingApp.Repository.MutualRepositories;
    using BookingApp.View.PathfinderViews;

    namespace BookingApp.View.TouristViews
    {
        /// <summary>
        /// Interaction logic for TouristDetails.xaml
        /// </summary>
        public partial class TouristDetails : UserControl
        {
            public Tours ToursUserControl;
            public event EventHandler ReturnRequest;
            public User _user;
            public LocationRepository locationRepository;
            public ObservableCollection<Tourist> _tourists {  get; set; }

            public TouristRepository touristRepository;

            private int _guestNumber;
            private string name;
            private string surname;
            private int _guestAge;
            public int GuestAge
        {
            get { return _guestAge; }
            set
            {
                _guestAge = value;
                if(_guestAge <= 0 || _guestAge > 150)
                {
                    enterValidAgeMessage.Visibility = Visibility.Visible;
                }else
                {
                    enterValidAgeMessage.Visibility = Visibility.Hidden;
                }
            }
        }

            public int GuestNumber
            {
                get { return _guestNumber; }
                set
                {
                    _guestNumber = value;
                if (selectedTour != null && (_guestNumber > selectedTour.Capacity || _guestNumber<=0))
                {
                    numberHigherMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    numberHigherMessage.Visibility = Visibility.Hidden;
                    enterValidGuestNumber.Visibility = Visibility.Hidden;

                }
            }
            }

            public Tour selectedTour { get; set; }
            public TouristDetails(Tour detailedTour, User user)
            {
                InitializeComponent();
                _user = user;
                locationRepository = new LocationRepository();
                selectedTour = detailedTour;
                ToursUserControl = new Tours(user);
                _tourists = new ObservableCollection<Tourist>();
                touristRepository = new TouristRepository();

                tourNameTextBlock.Text = selectedTour.Name;
                tourCountryTextBlock.Text = locationRepository.GetById(selectedTour.LocationId).Country;
                tourCityTextBlock.Text = locationRepository.GetById(selectedTour.LocationId).City;

                HideMessages();


            }

        private void GuestNumber_TextChanged(object sender, RoutedEventArgs e)
            {
                if (!int.TryParse(GuestNumberText.Text, out int number) && GuestNumberText.Text!="")
                {
                enterValidGuestNumber.Visibility = Visibility.Visible;
                }
            else
                {
                    GuestNumber = number;
                    enterValidGuestNumber.Visibility = Visibility.Hidden;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.TouristWindowFrame.Content = mainWindow.ToursUserControl;
            }
        }

        private void GuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(GuestAgeText.Text, out int result) && GuestAgeText.Text!="")
            {
                enterValidAgeMessage.Visibility = Visibility.Visible;

            }
            else
            {
                GuestAge = result;
            }
        }

        private void HideMessages()
        {
            enterAllFieldsMessage.Visibility = Visibility.Hidden;
            enterValidAgeMessage.Visibility = Visibility.Hidden;
            numberHigherMessage.Visibility = Visibility.Hidden;
            increaseNumberText.Visibility = Visibility.Hidden;
            addTouristMessage.Visibility = Visibility.Hidden;
            enterValidAgeMessage.Visibility = Visibility.Hidden;

        }
        private void AddTourist_Click(object sender, RoutedEventArgs e)
        {

            if(increaseNumberText.Visibility==Visibility.Visible || enterValidGuestNumber.Visibility == Visibility.Visible || numberHigherMessage.Visibility==Visibility.Visible || enterValidAgeMessage.Visibility==Visibility.Visible) {
                return;
            }

            HideMessages();

                if (!areFieldsEmpty())
                {
                    name = GuestName.Text;
                    surname = GuestSurname.Text;

                    Tourist tourist = new Tourist
                    {
                        Name = name,
                        Surname = surname,
                        Age = GuestAge
                    };

                    _tourists.Add(tourist);
                    TouristDataGrid.ItemsSource = null;
                    TouristDataGrid.ItemsSource = _tourists;
                    GuestName.Text = "";
                    GuestSurname.Text = "";
                    GuestAgeText.Text = "";
                }else
                {
                    enterAllFieldsMessage.Visibility = Visibility.Visible;
                    return;
                }
                if (GuestNumber < _tourists.Count() + 1)
                {
                    increaseNumberText.Visibility = Visibility.Visible;
                    return;
                }

        }

        private bool areFieldsEmpty()
        {
            return GuestNumberText.Text == "" || GuestName.Text == "" || GuestSurname.Text == "" || GuestAgeText.Text == "";
        }
            private void ReserveTour_Click(object sender, RoutedEventArgs e)
            {
                if(_tourists.Count() == 0) {
                    addTouristMessage.Visibility = Visibility.Visible;
                    return;
                }
                
                if(_tourists.Count() < GuestNumber)
                {
                    addTouristMessage.Visibility = Visibility.Visible;
                    return;
                }
                List<Tourist> tourists = new List<Tourist>();
                foreach(Tourist t in _tourists)
                {
                    tourists.Add(t);
                }

                Window parentWindow = Window.GetWindow(this);

                if (parentWindow is TouristMainWindow mainWindow)
                  {
                      mainWindow.ShowTourDates(selectedTour, GuestNumber, tourists);
                      _tourists.Clear();
                  }
            
            }
        }

    }
