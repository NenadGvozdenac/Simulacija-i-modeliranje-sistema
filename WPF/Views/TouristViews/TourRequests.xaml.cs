using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.TouristViews.Components;
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

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for TourRequests.xaml
    /// </summary>
    public partial class TourRequests : UserControl
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
        public User user {  get; set; }
        public TourRequests(User _user)
        {
            InitializeComponent();
            DataContext = this;
            user = _user;
            tourRequests = new ObservableCollection<TourRequest>();
            
            Update();
        }
        public void Update()
        {
            tourRequests.Clear();
            
            foreach(TourRequest tourRequest in TourRequestService.GetInstance().GetAll())
            {
                DateTime now = DateTime.Now;
                TimeSpan difference = now - tourRequest.BeginDate;
                if (difference.TotalHours < 48)
                {
                    tourRequest.Status = "Invalid";
                }
                TourRequest request = TourRequestService.GetInstance().GetById(tourRequest.Id);
                request.Location = LocationService.GetInstance().GetById(tourRequest.LocationId);
                request.Language = LanguageService.GetInstance().GetById(tourRequest.LanguageId);
                tourRequests.Add(request);   
            }
            MyTourRequests = new ObservableCollection<TourRequest>(tourRequests);
        }

        private void Add_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

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
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowTourRequestStatistics();
            }
        }
    }
}
