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
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using BookingApp.Application.UseCases;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TourRequestsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TourRequest> tourRequests { get; set; }

        private ObservableCollection<TourRequest> _tourRequests;
        public ObservableCollection<TourRequest> MyTourRequests
        {
            get { return _tourRequests; }
            set
            {
                if (_tourRequests != value)
                {
                    _tourRequests = value;
                    OnPropertyChanged();
                }
            }
        }
        public User user { get; set; }
        public TourRequests tourRequestsView {  get; set; }
        public TourRequestsViewModel(User _user, TourRequests _tourRequests)
        {
            tourRequestsView = _tourRequests;
            user = _user;
            tourRequests = new ObservableCollection<TourRequest>();

            //Update();
        }
        public void Update()
        {
            tourRequests.Clear();

            foreach (TourRequest tourRequest in TourRequestService.GetInstance().GetAll())
            {

                TourRequest request = TourRequestService.GetInstance().GetById(tourRequest.Id);
                request.Location = LocationService.GetInstance().GetById(tourRequest.LocationId);
                request.Language = LanguageService.GetInstance().GetById(tourRequest.LanguageId);
                DateTime now = DateTime.Now;
                TimeSpan difference = tourRequest.BeginDate - now;

                if (difference.TotalDays <= 2 && difference.TotalHours < 48)
                {
                    request.Status = "Invalid";
                }
                tourRequests.Add(request);
            }
            MyTourRequests = new ObservableCollection<TourRequest>(tourRequests);
        }

        public void Add_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = Window.GetWindow(tourRequestsView);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.AddRequest(user);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Statistics_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(tourRequestsView);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowTourRequestStatistics();
            }
        }
    }
}
