using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.View.PathfinderViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class DemographicsControlViewModel
    {
        public ObservableCollection<Tour> tours { get; set; }

        public TourRepository tourRepository { get; set; }

        public LocationRepository locationRepository { get; set; }
        public TourImageRepository tourImageRepository { get; set; }

        public TourStartTimeRepository tourStartTimeRepository { get; set; }

        public LanguageRepository languageRepository { get; set; }

        public TouristReservationRepository touristReservationRepository { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> StatsButtonClickedControl { get; set; }

        DemographicsControl demographicsControl { get; set; }
        public DemographicsControlViewModel(DemographicsControl _demographicsControl)
        {
            
            demographicsControl = _demographicsControl;
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

        public void demographicscard_TourStatsClicked(object sender, BeginButtonClickedEventArgs e)
        {
            onStatsButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void onStatsButtonClicked(BeginButtonClickedEventArgs e)
        {
            StatsButtonClickedControl?.Invoke(this, e);
        }


    }
}
