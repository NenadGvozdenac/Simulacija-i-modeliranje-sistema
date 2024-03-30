using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Services.Mutual;
using BookingApp.ViewModel.OwnerViewModels;
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

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for ReservationReview.xaml
    /// </summary>
    public partial class ReservationReview : UserControl
    {
        public User _user;
        public AccommodationReservation reservation;
        public Accommodation accommodation;
        public AccommodationReservationRepository _accommodationReservationRepository;
        public AccommodationRepository _accommodationRepository;
        public AccommodationReviewRepository _accommodationReviewRepository;
        public ReviewImageRepository _reviewImageRepository;

        public List<ReviewImage> _reviewImages;

        public ReservationReview(User user, AccommodationReservationRepository accommodationReservationRepository, AccommodationRepository accommodationRepository, AccommodationReviewRepository accommodationReviewRepository, ReviewImageRepository reviewImageRepository, int reservationId)
        {
            InitializeComponent();
            _user = user;
            _accommodationReservationRepository = accommodationReservationRepository;
            _accommodationRepository = accommodationRepository;    
            _accommodationReviewRepository = accommodationReviewRepository;
            _reviewImageRepository = reviewImageRepository;
            reservation = _accommodationReservationRepository.GetById(reservationId);
            accommodation = _accommodationRepository.GetById(reservation.AccommodationId);
            _reviewImages = new List<ReviewImage>();
            SetUpReviewPage();
        }

        private void SetUpReviewPage()
        {
            accommodationName_TextBlock.Text = accommodation.Name;
            dearUsername_TextBlock.Text = "Dear " + _user.Username + ",";
            username_TextBlock.Text = _user.Username;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).GoBack();
        }

        private void FinishReview_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReview review = new AccommodationReview(_user.Id, reservation.AccommodationId, reservation.Id, (int)cleanliness_Slider.Value, (int)ownersCourtesy_Slider.Value, feedback_TextBox.Text);
            _accommodationReviewRepository.Add(review);
            
            foreach (ReviewImage reviewImage in _reviewImages)
            {
                reviewImage.ReviewId = review.Id;
                _reviewImageRepository.Add(reviewImage);
            }
            NavigationService.GetNavigationService(this).GoBack();
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = ImageService.GetInstance().GetImageFromUser("ReviewImages");
            Image slika = ImageService.GetInstance().ReadImage(imagePath);
            ReviewImage reviewImage = new ReviewImage(reservation.Id, reservation.AccommodationId, imagePath);

            slika.Width = 185;
            slika.Height = 140;

            _reviewImages.Add(reviewImage);
            reviewImages_StackPanel.Children.Add(slika);      
        }
    }
}
