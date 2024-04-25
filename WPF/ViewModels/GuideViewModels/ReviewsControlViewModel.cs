using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.Repositories;
using BookingApp.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class ReviewsControlViewModel
    {
        public ObservableCollection<TourReview> tourReviews { get; set; }

        public User _user {  get; set; }

        public ReviewsControl _reviewsControl { get; set; }

        public TourReviewRepository tourReviewRepository { get; set; }

        public ReviewsControlViewModel(ReviewsControl reviewsWindow, User user)
        {
            _reviewsControl = reviewsWindow;
            _user = user;
            tourReviews = new ObservableCollection<TourReview>();
            tourReviewRepository = new TourReviewRepository();
            Update();
        }

        private void Update()
        {
            foreach(TourReview review in tourReviewRepository.GetAll())
            {
                TourReview review_temp = tourReviewRepository.GetById(review.Id);
                if(TourService.GetInstance().GetByOwnerId(_user.Id).Contains(TourService.GetInstance().GetById(review_temp.TourId))) 
                {
                    TourReview review_copy = new TourReview();
                    review_copy.TourId = review_temp.Id;
                    review_copy.UserId = review_temp.UserId;
                    review_copy.Id = review_temp.Id;
                    review_copy.ReservationId = review_temp.ReservationId;
                    review_copy.ReviewImages = review_temp.ReviewImages;
                    review_copy.GuideKnowledge = review_temp.GuideKnowledge;
                    review_copy.GuideLanguage = review_temp.GuideLanguage;
                    review_copy.TourInterestingness = review_temp.TourInterestingness;
                    review_copy.Feedback = review_temp.Feedback;
                    review_copy.UserName = UserService.GetInstance().GetById(review_temp.UserId).Username;
                    review_copy.Status = review_temp.Status;
                    review_copy.ReservationId = 14;
                    review_copy.Checkpoint = "Katedrala";

                    tourReviews.Add(review_copy);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
