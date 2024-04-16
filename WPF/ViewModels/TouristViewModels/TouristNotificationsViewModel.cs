using BookingApp.Domain.Models;
using BookingApp.WPF.Views.TouristViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TouristNotificationsViewModel
    {
        public ObservableCollection<Tourist> tourists { get; set; }

        private ObservableCollection<Tourist> _myTourists;
        public ObservableCollection<Tourist> MyTourists
        {
            get { return _myTourists; }
            set
            {
                if (_myTourists != value)
                {
                    _myTourists = value;
                    OnPropertyChanged();
                }
            }
        }
        public TouristNotifications touristNotifications {  get; set; }
        public User user {  get; set; }
        public TouristNotificationsViewModel(User _user, TouristNotifications _touristNotifications) {
            touristNotifications = _touristNotifications;
            tourists = new ObservableCollection<Tourist>();
            user = _user;
        }

        public void Update()
        {
            foreach (TouristReservation touristReservation in TourReservationService.GetInstance().GetAll())
            {
                if (touristReservation.CheckpointId != -1)
                {
                    tourists.Add(TouristService.GetInstance().GetById(touristReservation.Id_Tourist));
                }
            }
            MyTourists = new ObservableCollection<Tourist>(tourists);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
