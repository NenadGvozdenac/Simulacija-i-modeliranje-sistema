﻿using BookingApp.Domain.Models;
using BookingApp.Model.PathfinderModels;
using BookingApp.Repositories;
using BookingApp.View.GuestViews;
using BookingApp.View.OwnerViews.Components;
using BookingApp.View.PathfinderViews.Componentss;
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
using System.Windows.Shapes;

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for GuideMainWindow.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {
        private readonly User _user;
        private TourRepository _tourRepository;
        private TourImageRepository _tourImageRepository;
        private TourStartTimeRepository _timeRepository;
        private DailyTourCard dailyTourCard;

        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedMain { get; set; }
        public GuideMainWindow(User user)
        {
            InitializeComponent();
            _user = user;
            _tourImageRepository = new TourImageRepository();
            _tourRepository = new TourRepository();
            _timeRepository = new TourStartTimeRepository();
            Update();
        }

        public void ScheduleTourClick(object sender, RoutedEventArgs e)
        {
            AddTourWindow tourWindow = new AddTourWindow(_user, _tourRepository, _tourImageRepository);
            tourWindow.ShowDialog();

        }

        public void DailyToursClick(object sender, RoutedEventArgs e) {

           
            DailyToursWindow dailyWindow = new DailyToursWindow(_user);

            if (ongoingTourCheck() == 0){
                
                dailyWindow.BeginButtonClickedWindow += (s, e) => DailyToursWindow_SomeEventHandler(s, e);
                dailyWindow.EndButtonClickedWindow += (s, e) => DailyToursWindow_EndEventHandlerDaily(s, e);
                dailyWindow.ShowDialog();
            }
        }

        public int ongoingTourCheck() {
            foreach (TourStartTime time in _timeRepository.GetAll())
            {
                if (time.Status == "ongoing")
                {
                    CheckpointsView checkpointsWindow = new CheckpointsView(time.TourId, time.Time);
                    checkpointsWindow.EndButtonClickedMain += (s, e) => DailyToursWindow_EndEventHandlerMain(s, e);

                    checkpointsWindow.ShowDialog();
                    return 1;
                }
            }
            return 0;
        }


        public void Update()
        {
            
        }



       /* private void DailyTourCard_BeginButtonClicked(object sender, BeginButtonClickedEventArgs e)
        {
            ChangeTourStatus(e.TourId, e.StartTime);
        }*/

        public void ChangeTourStatusOngoing(int tourId, DateTime dateTime)
        {
            TourStartTime startTime = new TourStartTime();
            startTime = _timeRepository.GetByTourStartTimeAndId(dateTime, tourId);
            startTime.Status = "ongoing";
            _timeRepository.Update(startTime);
        }

        public void ChangeTourStatusPassed(int tourId, DateTime dateTime)
        {
            TourStartTime startTime = new TourStartTime();
            startTime = _timeRepository.GetByTourStartTimeAndId(dateTime, tourId);
            startTime.Status = "passed";
            _timeRepository.Update(startTime);
        }


        private void DailyToursWindow_SomeEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            OnBeginButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            ChangeTourStatusOngoing(e.TourId,e.StartTime);
        }

       
        
        private void DailyToursWindow_EndEventHandlerDaily(object sender, BeginButtonClickedEventArgs e)
        {
            OnEndButtonClickedDaily(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnEndButtonClickedDaily(BeginButtonClickedEventArgs e)
        {
            ChangeTourStatusPassed(e.TourId, e.StartTime);
        }

        private void DailyToursWindow_EndEventHandlerMain(object sender, BeginButtonClickedEventArgs e)
        {
            OnEndButtonClickedMain(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnEndButtonClickedMain(BeginButtonClickedEventArgs e)
        {
            ChangeTourStatusPassed(e.TourId, e.StartTime);
        }

    }
}
