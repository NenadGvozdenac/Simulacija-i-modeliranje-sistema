using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.View.PathfinderViews;
using BookingApp.View.PathfinderViews.Componentss;
using BookingApp.WPF.Views.TouristViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class DailyToursControlViewModel
    {
        public ObservableCollection<Tour> dailyTours { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedControl { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> EndButtonClickedControl { get; set; }

        


        private readonly User _user;

        public DailyToursControl dailyToursControl { get; set; }

        public DailyToursControlViewModel(DailyToursControl _dailyToursControl,User user)
        {
            dailyToursControl = _dailyToursControl;
            dailyTours = new ObservableCollection<Tour>();
            
           _user = user;
            Update();

        }

        public void Update()
        {
            dailyTours.Clear();

            foreach (TourStartTime startTime in TourStartTimeService.GetInstance().GetAll())
            {
                if (CheckIfPassed(startTime) != 1)
                {
                   if (startTime.Time.Date == System.DateTime.Now.Date)
                    {
                        Tour toura = TourService.GetInstance().GetById(startTime.TourId);
                        if (toura.OwnerId == _user.Id)
                        {
                            Tour tour = new Tour();
                            tour.Capacity = toura.Capacity;
                            tour.CurrentDate = startTime.Time;
                            tour.Location = LocationService.GetInstance().GetById(toura.LocationId);
                            tour.Id = toura.Id;
                            tour.Images = new(TourImageService.GetInstance().GetImagesByTourId(tour.Id));
                            tour.Language = LanguageService.GetInstance().GetById(toura.LanguageId);  //fix                          
                            tour.LocationId = toura.LocationId;
                            tour.LanguageId = toura.LanguageId;
                            tour.Duration = toura.Duration;
                            tour.Checkpoints = toura.Checkpoints;
                            tour.Dates = toura.Dates;
                            tour.Description = toura.Description;
                            tour.Name = toura.Name;                       
                            if(TourStartTimeService.GetInstance().GetAll().Find(a => a.Status == "ongoing") != null)
                            {
                                tour.Ongoing = false;
                            }
                            else
                            {
                                tour.Ongoing = true;
                            }

                            
                            dailyTours.Add(tour);
                            
                        }
                    }
                }
            }
        }

        public int CheckIfPassed(TourStartTime startTime)
        {
            if (startTime.Status == "passed")
                return 1;
            return 0;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        public void DailyTourCard_BeginButtonClicked(object sender, BeginButtonClickedEventArgs e)
        {
            TourStartTime startTime = new TourStartTime();
            startTime = TourStartTimeService.GetInstance().GetByTourStartTimeAndId(e.StartTime, e.TourId);
            startTime.Status = "ongoing";
            TourStartTimeService.GetInstance().Update(startTime);
            foreach (var tour in dailyTours) { tour.Ongoing = false;}
            //OnBeginButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            BeginButtonClickedControl?.Invoke(this, e);
        }


        public void DailyTourCard_EndButtonClicked(object sender, BeginButtonClickedEventArgs e)
        {
            TourStartTime startTime = new TourStartTime();
            startTime = TourStartTimeService.GetInstance().GetByTourStartTimeAndId(e.StartTime, e.TourId);
            startTime.Status = "passed";
            TourStartTimeService.GetInstance().Update(startTime);
            Tour tour = dailyTours.FirstOrDefault(a => a.Id == e.TourId && a.CurrentDate == e.StartTime);
            dailyTours.Remove(tour);
            foreach (var _tour in dailyTours) { _tour.Ongoing = true;}
            // OnEndButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnEndButtonClicked(BeginButtonClickedEventArgs e)
        {
            EndButtonClickedControl?.Invoke(this, e);
        }

        public void SearchTourByName(string name){
            if (!string.IsNullOrWhiteSpace(name))
            {                
                var toursToRemove = dailyTours.Where(tour => !tour.Name.ToLower().Contains(name.ToLower())).ToList();
                foreach (var tour in toursToRemove)
                {
                    dailyTours.Remove(tour);
                }
            }
        }

        public void SearchByCountry(string country)
        {
            if (!string.IsNullOrWhiteSpace(country))
            {
                var toursToRemove = dailyTours.Where(tour => tour.Location.Country != country).ToList();
                foreach (var tour in toursToRemove)
                {
                    dailyTours.Remove(tour);
                }
            }
        }
    
        public void SearchByCity(string city){
            if (!String.IsNullOrWhiteSpace(city))
            {

                var toursToRemove = dailyTours.Where(tour => tour.Location.City != city).ToList();
                foreach (var tour in toursToRemove)
                {
                    dailyTours.Remove(tour);
                }

            }
        }
    
    
        public void SearchByLanguage(string language) {

            if (!String.IsNullOrWhiteSpace(language))
            {
                var toursToRemove = dailyTours.Where(tour => tour.Language.Name != language).ToList();
                foreach (var tour in toursToRemove)
                {
                    dailyTours.Remove(tour);
                }
            }

        }
    
        public void SearchByCapacity(int capacity)
        {
            if (capacity != 0)
            {
                var toursToRemove = dailyTours.Where(tour => tour.Capacity != capacity).ToList();
                foreach (var tour in toursToRemove)
                {
                    dailyTours.Remove(tour);
                }
            }
        }
    
        public void SearchByDuration(int duration)
        {
            if (duration != 0)
            {
                var toursToRemove = dailyTours.Where(tour => tour.Duration != duration).ToList();
                foreach (var tour in toursToRemove)
                {
                    dailyTours.Remove(tour);
                }
            }
        }

        public void SearchByCriteria(string country, string city, string language, int capacity, int duration, string searchText) {
        
            Update();
            SearchByCountry(country);
            SearchByCity(city);
            SearchByLanguage(language);
            SearchByCapacity(capacity);
            SearchByDuration(duration);
            SearchTourByName(searchText);
        
        }
    
    
    }
}
