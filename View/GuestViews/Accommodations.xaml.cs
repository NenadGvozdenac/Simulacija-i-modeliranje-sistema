using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class Accommodations : UserControl
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

            accomodationrepository = new AccommodationRepository();
            locationrepository = new LocationRepository();
            accommodationimagerepository = new AccommodationImageRepository();

            Update();
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

    }
}
