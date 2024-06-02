using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using BookingApp.Application.UseCases;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for RequestsOnComplexTourRequest.xaml
    /// </summary>
    public partial class RequestsOnComplexTourRequest : UserControl, INotifyPropertyChanged
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
        public User _user {  get; set; }
        public int complexRequestId {  get; set; }
        public RequestsOnComplexTourRequest(User user, int complexRequestId)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            this.complexRequestId = complexRequestId;
            tourRequests = new ObservableCollection<TourRequest>();
            Update();
        }

        public void Update()
        {
            tourRequests.Clear();

            foreach (TourRequest tourRequest in TourRequestService.GetInstance().GetAll())
            {
                if (complexRequestId == tourRequest.ComplexRequestId)
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
            }
            MyTourRequests = new ObservableCollection<TourRequest>(tourRequests);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
