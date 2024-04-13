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

        public EventHandler<BeginButtonClickedEventArgs> StatsButtonClickedControl { get; set; }

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
            foreach (Tour tour in tourRepository.GetAll())
            {
                tours.Add(tour);
            }
        }

        private void demographicscard_TourStatsClicked(object sender, BeginButtonClickedEventArgs e) 
        {
            onStatsButtonClicked(new BeginButtonClickedEventArgs(e.TourId,e.StartTime));
        }

        private void onStatsButtonClicked(BeginButtonClickedEventArgs e)
        {
            StatsButtonClickedControl?.Invoke(this, e);
        }

    }
}
