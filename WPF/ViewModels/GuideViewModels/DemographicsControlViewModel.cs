using BookingApp.Application.UseCases;
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
   

        public EventHandler<BeginButtonClickedEventArgs> StatsButtonClickedControl { get; set; }

        DemographicsControl demographicsControl { get; set; }
        public DemographicsControlViewModel(DemographicsControl _demographicsControl)
        {
            
            demographicsControl = _demographicsControl;
            tours = new ObservableCollection<Tour>();
            Update();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            foreach (Tour tour in TourService.GetInstance().GetAll())
            {
                tour.Location = LocationService.GetInstance().GetById(tour.LocationId);
                tour.Language = LanguageService.GetInstance().GetById(tour.LanguageId);
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
