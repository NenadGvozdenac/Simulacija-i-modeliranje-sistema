using BookingApp.Domain.Models;
using BookingApp.WPF.Views.TouristViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TouristNotificationsViewModel
    {
        public TouristNotifications touristNotifications {  get; set; }
        public TouristNotificationsViewModel(User user, TouristNotifications _touristNotifications) {
            touristNotifications = _touristNotifications;

        }
    }
}
