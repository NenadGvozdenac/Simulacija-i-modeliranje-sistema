using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository.OwnerRepositories;
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
using System.Windows.Shapes;

namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for AddGuestRating.xaml
    /// </summary>
    public partial class AddGuestRating : Window
    {
        private GuestRatingRepository _guestRatingRepository;
        private GuestRating _uncheckedGuestRating;
        private string guestUsername;
        public string GuestUsername
        {
            get => guestUsername;
            set
            {
                if(value != guestUsername)
                {
                    guestUsername = value;
                    OnPropertyChanged("GuestUsername");
                }
            }
        }

        private string accommodationName;
        public string AccommodationName
        {
            get => accommodationName;
            set
            {
                if(value != accommodationName)
                {
                    accommodationName = value;
                    OnPropertyChanged("AccommodationName");
                }
            }
        }

        private string accommodationType;
        public string AccommodationType
        {
            get => accommodationType;
            set
            {
                if(value != accommodationType)
                {
                    accommodationType = value;
                    OnPropertyChanged("AccommodationType");
                }
            }
        }

        private string accommodationLocation;
        public string AccommodationLocation
        {
            get => accommodationLocation;
            set
            {
                if(value != accommodationLocation)
                {
                    accommodationLocation = value;
                    OnPropertyChanged("AccommodationLocation");
                }
            }
        }

        private int numberOfGuests;
        public int NumberOfGuests
        {
            get => numberOfGuests;
            set
            {
                if(value != numberOfGuests)
                {
                    numberOfGuests = value;
                    OnPropertyChanged("NumberOfGuests");
                }
            }
        }

        private int reservationDays;
        public int ReservationDays
        {
            get => reservationDays;
            set
            {
                if (value != reservationDays)
                {
                    reservationDays = value;
                    OnPropertyChanged("ReservationDays");
                }
            }
        }

        private string reservationTimespan;
        public string ReservationTimespan
        {
            get => reservationTimespan;
            set
            {
                if(value != reservationTimespan)
                {
                    reservationTimespan = value;
                    OnPropertyChanged("ReservationTimespan");
                }
            }
        }

        private ObservableCollection<int> cleanliness;
        public ObservableCollection<int> Cleanliness
        {
            get => cleanliness;
            set
            {
                if(value != cleanliness)
                {
                    cleanliness = value;
                    OnPropertyChanged("Cleanliness");
                }
            }
        }

        private int selectedCleanliness;
        public int SelectedCleanliness
        {
            get => selectedCleanliness;
            set
            {
                if(value != selectedCleanliness)
                {
                    selectedCleanliness = value;
                    OnPropertyChanged("SelectedCleanliness");
                }
            }
        }

        private ObservableCollection<int> respectfulness;
        public ObservableCollection<int> Respectfulness
        {
            get => respectfulness;
            set
            {
                if(value != respectfulness)
                {
                    respectfulness = value;
                    OnPropertyChanged("Respectfulness");
                }
            }
        }

        private int selectedRespectfulness;
        public int SelectedRespectfulness
        {
            get => selectedRespectfulness;
            set
            {
                if(value != selectedRespectfulness)
                {
                    selectedRespectfulness = value;
                    OnPropertyChanged("SelectedRespectfulness");
                }
            }
        }

        private string comment;
        public string Comment
        {
            get => comment;
            set
            {
                if(value != comment)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        // The event handler for the property changed event
        public event PropertyChangedEventHandler PropertyChanged;

        // The method to invoke the property changed event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // TODO: Add reservation
        public AddGuestRating(Accommodation accommodation, GuestRating uncheckedGuestRating, GuestRatingRepository guestRatingRepository)
        {
            _guestRatingRepository = guestRatingRepository;
            _uncheckedGuestRating = uncheckedGuestRating;
            AccommodationName = accommodation.Name;
            AccommodationLocation = accommodation.Location.ToString();
            AccommodationType = accommodation.Type.ToString();
            Cleanliness = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
            Respectfulness = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
            ReservationTimespan = string.Format("{0} - {0}", uncheckedGuestRating.Reservation.FirstDateOfStaying, uncheckedGuestRating.Reservation.LastDateOfStaying);
            NumberOfGuests = uncheckedGuestRating.Reservation.GuestsNumber;
            ReservationDays = (uncheckedGuestRating.Reservation.LastDateOfStaying - uncheckedGuestRating.Reservation.FirstDateOfStaying).Days;
            DataContext = this;
            InitializeComponent();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if(!IsDataValid())
            {
                return;
            }

            GuestRating guestRating = _guestRatingRepository.GetById(_uncheckedGuestRating.Id);
            guestRating.Cleanliness = SelectedCleanliness;
            guestRating.Respectfulness = SelectedRespectfulness;
            guestRating.Comment = Comment;
            guestRating.IsChecked = true;

            _guestRatingRepository.Update(guestRating);
        }

        private bool IsDataValid()
        {
            return SelectedRespectfulness != 0 && SelectedCleanliness != 0;
        }
    }
}
