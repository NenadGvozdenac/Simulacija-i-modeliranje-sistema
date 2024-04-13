﻿using BookingApp.Domain.Models;
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
    public class DailyToursControlViewModel
    {
        public ObservableCollection<Tour> dailyTours { get; set; }

        public TourRepository tourRepository { get; set; }
        public LocationRepository locationRepository { get; set; }
        public TourImageRepository tourImageRepository { get; set; }

        public LanguageRepository languageRepository { get; set; }

        public TourStartTimeRepository tourStartTimeRepository { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedControl { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> EndButtonClickedControl { get; set; }

        private readonly User _user;

        public DailyToursControl dailyToursControl { get; set; }

        public DailyToursControlViewModel(DailyToursControl _dailyToursControl,User user)
        {
            dailyToursControl = _dailyToursControl;
            dailyTours = new ObservableCollection<Tour>();
            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();
            tourImageRepository = new TourImageRepository();
            languageRepository = new LanguageRepository();
            tourStartTimeRepository = new TourStartTimeRepository();

            _user = user;
            Update();

        }

        public void Update()
        {
            foreach (TourStartTime startTime in tourStartTimeRepository.GetAll())
            {
                if (CheckIfPassed(startTime) == 1)
                {
                    continue;
                }
                if (startTime.Time.Date == System.DateTime.Now.Date)
                {
                    Tour toura = tourRepository.GetById(startTime.TourId);
                    if (toura.OwnerId == _user.Id)
                    {
                        Tour tour = new Tour();
                        tour.Capacity = toura.Capacity;
                        tour.CurrentDate = startTime.Time;
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



                        dailyTours.Add(tour);
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
            OnBeginButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            BeginButtonClickedControl?.Invoke(this, e);
        }


        public void DailyTourCard_EndButtonClicked(object sender, BeginButtonClickedEventArgs e)
        {
            OnEndButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnEndButtonClicked(BeginButtonClickedEventArgs e)
        {
            EndButtonClickedControl?.Invoke(this, e);
        }


    }
}