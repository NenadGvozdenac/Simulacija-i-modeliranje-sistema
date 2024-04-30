using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.View.PathfinderViews;
using BookingApp.View.PathfinderViews.Componentss;
using BookingApp.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class GuideMainWindowViewModel
    {

        private readonly User _user;
        private DailyTourCard dailyTourCard;

        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedMain { get; set; }

        public GuideMainWindow mainWindow { get; set; }

        public DailyToursControl tours {  get; set; }
        public GuideMainWindowViewModel(GuideMainWindow _mainWindow,User user)
        {
            mainWindow = _mainWindow;
            _user = user;
            tours = new DailyToursControl(_user);
            mainWindow.TourContainer.Children.Add(tours);
            Update();
        }

        public void ScheduleTourClick(object sender, RoutedEventArgs e)
        {
            AddTourWindow tourWindow = new AddTourWindow(_user);
            tourWindow.ShowDialog();

        }

        public void DailyToursClick(object sender, RoutedEventArgs e)
        {


            DailyToursWindow dailyWindow = new DailyToursWindow(_user);

            if (ongoingTourCheck() == 0)
            {

                dailyWindow.dailyToursWindowViewModel.BeginButtonClickedWindow += (s, e) => DailyToursWindow_SomeEventHandler(s, e);
                dailyWindow.dailyToursWindowViewModel.EndButtonClickedWindow += (s, e) => DailyToursWindow_EndEventHandlerDaily(s, e);
                dailyWindow.ShowDialog();
            }
        }

        public int ongoingTourCheck()
        {
            foreach (TourStartTime time in TourStartTimeService.GetInstance().GetAll())
            {
                if (time.Status == "ongoing")
                {
                    CheckpointsView checkpointsWindow = new CheckpointsView(time.TourId, time.Time);
                    checkpointsWindow.checkpointsViewModel.EndButtonClickedMain += (s, e) => DailyToursWindow_EndEventHandlerMain(s, e);

                    checkpointsWindow.ShowDialog();
                    return 1;
                }
            }
            return 0;
        }

        public void OngoingTour_Click(object sender, RoutedEventArgs e)
        {
            if(ongoingTourCheck() == 0)
            {
                MessageBox.Show("There are no ongoing tours currently");
            }
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
            startTime = TourStartTimeService.GetInstance().GetByTourStartTimeAndId(dateTime, tourId);
            startTime.Status = "ongoing";
            TourStartTimeService.GetInstance().Update(startTime);
        }

        public void ChangeTourStatusPassed(int tourId, DateTime dateTime)
        {
            TourStartTime startTime = new TourStartTime();
            startTime = TourStartTimeService.GetInstance().GetByTourStartTimeAndId(dateTime, tourId);
            startTime.Status = "passed";
            TourStartTimeService.GetInstance().Update(startTime);
        }

        public void Demographics_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TourDemographics tourDemographics = new TourDemographics();
            tourDemographics.Show();
        }

        public void Reviews_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReviewsWindow reviewsWindow = new ReviewsWindow(_user);
            reviewsWindow.Show();
        }

        

        // HANDLERS
        public  void DailyToursWindow_SomeEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            OnBeginButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            ChangeTourStatusOngoing(e.TourId, e.StartTime);
        }



        public void DailyToursWindow_EndEventHandlerDaily(object sender, BeginButtonClickedEventArgs e)
        {
            OnEndButtonClickedDaily(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnEndButtonClickedDaily(BeginButtonClickedEventArgs e)
        {
            ChangeTourStatusPassed(e.TourId, e.StartTime);
        }

        public void DailyToursWindow_EndEventHandlerMain(object sender, BeginButtonClickedEventArgs e)
        {
            ChangeTourStatusPassed(e.TourId, e.StartTime);
            tours.dailyToursControlViewModel.DailyTourCard_EndButtonClicked(sender, e);
        }

        public void OnEndButtonClickedMain(BeginButtonClickedEventArgs e)
        {
           
        }




    }
}
