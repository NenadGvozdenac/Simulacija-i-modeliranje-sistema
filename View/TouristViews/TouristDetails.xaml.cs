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

        public int GuestNumber
        {
            get { return _guestNumber; }
            set
            {
                _guestNumber = value;
                // Provjerite ako je uneseni broj veći od kapaciteta odabranog turističkog putovanja
                if (selectedTour != null && _guestNumber > selectedTour.Capacity)
                {
                    MessageBox.Show("Uneseni broj turista prelazi kapacitet odabrane ture!");
                    // Resetirajte vrijednost polja GuestNumberText na kapacitet odabrane ture
                    GuestNumberText.Text = selectedTour.Capacity.ToString();
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
            //GuestNumber = int.Parse(GuestNumberText.Text);

            tourNameTextBlock.Text = selectedTour.Name;
            tourCountryTextBlock.Text = locationRepository.GetById(selectedTour.LocationId).Country;
            tourCityTextBlock.Text = locationRepository.GetById(selectedTour.LocationId).City;
        }

        private void GuestNumber_TextChanged(object sender, RoutedEventArgs e)
        {
            // Ažurirajte vrijednost polja GuestNumber kada se promijeni tekst u TextBoxu
            if (int.TryParse(GuestNumberText.Text, out int number))
            {
                GuestNumber = number;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {

            //ReturnRequest?.Invoke(this, EventArgs.Empty);
        }

        private void GuestAgeDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GuestAgeUp_Click(object sender, RoutedEventArgs e)
        {

        }


        private void GuestNumberUp_Click(object sender, RoutedEventArgs e)
        {

        }
        private void GuestNumberDown_Click(object sender, RoutedEventArgs e)
        {

        }
        private void GuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddTourist_Click(object sender, RoutedEventArgs e)
        {
            // Dohvatanje vrednosti iz TextBox-ova

            if(GuestNumber < _tourists.Count()+1)
            {
                MessageBox.Show("Povecajte broj turista!");
                return;
            }

            if (GuestName.Text != "" || GuestSurname.Text != "" || GuestAge.Text != "")
            {
                string name = GuestName.Text;
                string surname = GuestSurname.Text;
                int age = int.Parse(GuestAge.Text);


                // Kreiranje novog objekta Tourist
                Tourist tourist = new Tourist
                {
                    Name = name,
                    Surname = surname,
                    Age = age
                };

                // Dodavanje turiste u kolekciju _tourists
                _tourists.Add(tourist);
                // Osvježavanje prikaza u DataGridu
                TouristDataGrid.ItemsSource = null; // Postavite na null prije postavljanja na ObservableCollection
                TouristDataGrid.ItemsSource = _tourists;
                GuestName.Text = "";
                GuestSurname.Text = "";
                GuestAge.Text = "";
            }else
            {
                MessageBox.Show("Popunite sva polja!");
            }

        }

        private void ReserveTour_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }

}
