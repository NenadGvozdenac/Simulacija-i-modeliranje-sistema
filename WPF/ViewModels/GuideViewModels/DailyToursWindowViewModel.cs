using BookingApp.Domain.Models;
using BookingApp.View.PathfinderViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class DailyToursWindowViewModel
    {
        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedWindow { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> EndButtonClickedWindow { get; set; }

        public User _user { get; set; }

        public DailyToursWindow dailyToursWindow { get; set; }

        public DailyToursWindowViewModel(DailyToursWindow _dailyToursWindow,User user)
        {
            
            _user = user;
            dailyToursWindow = _dailyToursWindow;
            var dailyToursControl = new DailyToursControl(user);
            dailyToursWindow.Content = dailyToursControl;
            dailyToursControl.BeginButtonClickedControl += (s, e) => DailyTours_SomeEventHandler(s, e);
            dailyToursControl.EndButtonClickedControl += (s, e) => DailyTours_EndEventHandler(s, e);
        }

        public void DailyTours_SomeEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            OnBeginButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            BeginButtonClickedWindow?.Invoke(this, e);
            dailyToursWindow.Close();
        }



        public void DailyTours_EndEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            OnEndButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
        }


        public void OnEndButtonClicked(BeginButtonClickedEventArgs e)
        {
            EndButtonClickedWindow?.Invoke(this, e);
            dailyToursWindow.Close();
        }

    }
}
