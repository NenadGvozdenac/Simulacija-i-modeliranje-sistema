using BookingApp.Model.MutualModels;
using BookingApp.Model.PathfinderModels;
using BookingApp.Repository.MutualRepositories;
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

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for DemographicsControl.xaml
    /// </summary>
    public partial class DemographicsControl : UserControl
    {
        public ObservableCollection<Tour> tours { get; set; }

        public TourRepository tourRepository { get; set; }

        public LocationRepository locationRepository { get; set; }
        public TourImageRepository tourImageRepository { get; set; }

        public TourStartTimeRepository tourStartTimeRepository { get; set; }

        public LanguageRepository languageRepository { get; set; }

        public TouristReservationRepository touristReservationRepository { get; set; }

        public DemographicsControl()
        {
            InitializeComponent();
            DataContext = this;

            tours = new ObservableCollection<Tour>();
            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();
            tourImageRepository = new TourImageRepository();
            languageRepository = new LanguageRepository();
            tourStartTimeRepository = new TourStartTimeRepository();
            touristReservationRepository = new TouristReservationRepository();
            Update();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            foreach (TourStartTime time in tourStartTimeRepository.GetAll())
            {
                Tour toura = tourRepository.GetById(time.TourId);
                if (time.Status == "passed")
                {
                    Tour tour = new Tour();
                    tour.Name = toura.Name;
                    tour.Capacity = toura.Capacity;
                    tour.CurrentDate = time.Time;
                    tour.Location = locationRepository.GetById(toura.LocationId);
                    tour.Images = tourImageRepository.GetImagesByTourId(tour.Id);
                    tour.Language = languageRepository.GetById(toura.LanguageId);
                    tour.Id = toura.Id;
                    tour.LocationId = toura.LocationId;
                    tour.LanguageId = toura.LanguageId;
                    tour.Duration = toura.Duration;
                    tour.Checkpoints = toura.Checkpoints;
                    tour.Dates = toura.Dates;
                    tour.Description = toura.Description;



                    tours.Add(tour);
                }
            }
        }



    }
}
