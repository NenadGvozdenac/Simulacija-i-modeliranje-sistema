    using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using System;
using System.Collections.Generic;
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

namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for DetailedGuestReviewPage.xaml
    /// </summary>
    public partial class DetailedGuestReviewPage : Page
    {
        private Accommodation _accommodation;
        private GuestRating _guestRating;
        private User _guestUser;

        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                accommodationName = value;
                OnPropertyChanged("AccommodationName");
            }
        }

        private string guestUsername;
        public string GuestUsername
        {
            get { return guestUsername; }
            set
            {
                guestUsername = value;
                OnPropertyChanged("GuestUsername");
            }
        }

        private string costPerNight;
        public string CostPerNight
        {
            get { return costPerNight; }
            set
            {
                costPerNight = value;
                OnPropertyChanged("CostPerNight");
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }

        private string typeOfAccommodation;
        public string TypeOfAccommodation
        {
            get { return typeOfAccommodation; }
            set
            {
                typeOfAccommodation = value;
                OnPropertyChanged("TypeOfAccommodation");
            }
        }

        private string reservationDays;
        public string ReservationDays
        {
            get { return reservationDays; }
            set
            {
                reservationDays = value;
                OnPropertyChanged("ReservationDays");
            }
        }

        private string numberOfGuests;
        public string NumberOfGuests
        {
            get { return numberOfGuests; }
            set
            {
                numberOfGuests = value;
                OnPropertyChanged("NumberOfGuests");
            }
        }

        private string currentRating;
        public string CurrentRating
        {
            get { return currentRating; }
            set
            {
                currentRating = value;
                OnPropertyChanged("CurrentRating");
            }
        }

        private string dateTimespan;
        public string DateTimespan
        {
            get { return dateTimespan; }
            set
            {
                dateTimespan = value;
                OnPropertyChanged("NumberOfReviews");
            }
        }

        private string cleaniness;
        public string Cleaniness
        {
            get { return cleaniness; }
            set
            {
                cleaniness = value;
                OnPropertyChanged("Cleaniness");
            }
        }

        private string respectfulness;
        public string Respectfulness
        {
            get { return respectfulness; }
            set
            {
                respectfulness = value;
                OnPropertyChanged("Respectfulness");
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DetailedGuestReviewPage(User guestUser, GuestRating guestRating, Accommodation accommodation)
        {
            InitializeComponent();
            _guestRating = guestRating;
            _accommodation = accommodation;
            _guestUser = guestUser;

            DataContext = this;
            SetupProperties();
        }

        private void SetupProperties()
        {
            AccommodationName = string.Format("{0}", _accommodation.Name);
            GuestUsername = string.Format("{0}", _guestUser.Username);
            CostPerNight = string.Format("${0} per night", _accommodation.Price.ToString());
            Location = string.Format("{0}", _accommodation.Location.ToString());
            TypeOfAccommodation = string.Format("{0}", _accommodation.Type.ToString());
            ReservationDays = string.Format("{0} days", (_guestRating.Reservation.LastDateOfStaying - _guestRating.Reservation.FirstDateOfStaying).Days);
            NumberOfGuests = string.Format("{0}", _guestRating.Reservation.GuestsNumber);
            DateTimespan = string.Format("{0} - {1}", _guestRating.Reservation.FirstDateOfStaying.ToShortDateString(), _guestRating.Reservation.LastDateOfStaying.ToShortDateString());
            CurrentRating = string.Format("{0:F2} / 10.00", _accommodation.AverageReviewScore);
            Cleaniness = string.Format("{0:F2} / 5,00", _guestRating.Cleanliness);
            Respectfulness = string.Format("{0:F2} / 5,00", _guestRating.Respectfulness);
            Comment = string.Format("{0}", _guestRating.Comment);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }

        private void NavigateBack()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Window.GetWindow(this).Close();
        }
        private void ShowRightNavbar()
        {
            RightNavbar.Visibility = Visibility.Visible;
            Navbar.ColumnDefinitions[2].Width = new GridLength(0.6, GridUnitType.Star);
        }

        private void HideRightNavbar()
        {
            RightNavbar.Visibility = Visibility.Collapsed;
            Navbar.ColumnDefinitions[2].Width = new GridLength(0);
        }

        public void ThreeDotsClick(object sender, MouseButtonEventArgs e)
        {
            if (RightNavbar.Visibility == Visibility.Collapsed)
            {
                ShowRightNavbar();
            }
            else
            {
                HideRightNavbar();
            }
        }
    }
}
