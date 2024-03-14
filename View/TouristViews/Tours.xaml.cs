using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository;
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
using BookingApp.View.GuestViews;

namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for Tours.xaml
    /// </summary>
    public partial class Tours : UserControl
    {
        public ObservableCollection<Tour> tours { get; set; }

        public TourRepository tourRepository { get; set; }
        public LocationRepository locationRepository { get; set; }
        public TourImageRepository tourImageRepository { get; set; }

        public LanguageRepository languageRepository { get; set; }




        public Tours(User user)
        {
            InitializeComponent();
            DataContext = this;

            tours = new ObservableCollection<Tour>();
            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();
            tourImageRepository = new TourImageRepository();
            languageRepository = new LanguageRepository();
            Update();

        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            foreach (Tour tour in tourRepository.GetAll())
            {
                tour.Location = locationRepository.GetById(tour.LocationId);
                tour.Images = tourImageRepository.GetImagesByTourId(tour.Id);
                tour.Language = languageRepository.GetById(tour.LanguageId);


                tours.Add(tour);
            }

        }
    }
}
