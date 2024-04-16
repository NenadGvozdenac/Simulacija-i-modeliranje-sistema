using BookingApp.Domain.Models;
using BookingApp.View.PathfinderViews;
using BookingApp.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class ReviewsWindowViewModel
    {
        public User _user { get; set; }

        public ReviewsWindow _reviewsWindow { get; set; }
        public ReviewsWindowViewModel(ReviewsWindow reviewsWindow, User user) {
        
            _user = user;
            _reviewsWindow = reviewsWindow;
            var reviewsControl = new ReviewsControl(user);
            _reviewsWindow.Content = reviewsControl;

        }

    }
}
