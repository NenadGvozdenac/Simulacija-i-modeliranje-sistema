using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
using BookingApp.Model.GuestModels;

namespace BookingApp.View.GuestViews.Components
{
    /// <summary>
    /// Interaction logic for CancelReservation.xaml
    /// </summary>
    public partial class CancelReservation : UserControl
    {
        public AccommodationReservation selectedReservation;
        public AccommodationRepository _accommodationRepository;
        public AccommodationReservationMovingRepository _accommodationMovingRepository;

        public EventHandler<int> YesClicked;
        public EventHandler NoClicked;
        public CancelReservation(AccommodationReservation _selectedReservation, AccommodationRepository accommodationRepository)
        {
            InitializeComponent();
            selectedReservation = _selectedReservation;
            _accommodationRepository = accommodationRepository;
            _accommodationMovingRepository = new AccommodationReservationMovingRepository();
            SetUpCancelReservation();
        }

        private void SetUpCancelReservation()
        {
            Accommodation accommodation = _accommodationRepository.GetById(selectedReservation.AccommodationId);
            NameOfTheAccommodation_TextBlock.Text = accommodation.Name;
            AvailableDates temp = new AvailableDates(selectedReservation.FirstDateOfStaying, selectedReservation.LastDateOfStaying);
            OriginalCheckInDate_TextBlock.Text = temp.ToString();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            YesClicked?.Invoke(this, selectedReservation.Id);
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
           NoClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
