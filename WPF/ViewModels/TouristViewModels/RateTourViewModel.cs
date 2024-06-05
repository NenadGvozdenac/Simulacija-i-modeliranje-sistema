using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;
using BookingApp.WPF.Views.GuestViews;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class RateTourViewModel
    {
        public Tour selectedTour { get; set; }
        public User _user { get; set; }
        public TouristReservation reservation { get; set; }
        public List<TourReviewImage> _reviewImages { get; set; }

        private int guideKnowledgeRating = 0;
        private int guideLanguageRating = 0;
        private int interestingnessRating = 0;
        private string feedbackText = string.Empty;
        public RateTour rateTour {  get; set; }
        public RateTourViewModel(User user, int tourId, RateTour rateTour)
        {
            _user = user;
            //reservation = _touristReservationRepository.GetById(reservationId);
            _reviewImages = new List<TourReviewImage>();
            selectedTour = TourService.GetInstance().GetById(tourId);
            this.rateTour = rateTour;
        }


        public void Star_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image star = sender as Image;
            string starName = star.Name;

            // Ova vrednost ce biti 1 ako je prva zvezdica kliknuta, 2 ako je druga kliknuta, i tako dalje.
            int starNumber = int.Parse(starName.Substring(starName.Length - 1));

            // Ocenjivanje Guide's knowledge
            if (starName.StartsWith("guideKnowledgeStar"))
            {
                guideKnowledgeRating = starNumber;
                UpdateStarRatingUI("guideKnowledgeStar", guideKnowledgeRating);
                rateTour.knowledgeMessage.Visibility = Visibility.Hidden;
            }
            else if (starName.StartsWith("guideLanguageStar"))
            {
                guideLanguageRating = starNumber;
                UpdateStarRatingUI("guideLanguageStar", guideLanguageRating);
                rateTour.languageMessage.Visibility = Visibility.Hidden;
            }
            // Ocenjivanje Interestigness
            else if (starName.StartsWith("interestingnessStar"))
            {
                interestingnessRating = starNumber;
                UpdateStarRatingUI("interestingnessStar", interestingnessRating);
                rateTour.interestMessage.Visibility = Visibility.Hidden;
            }
        }

        public void UpdateStarRatingUI(string starPrefix, int rating)
        {
            for (int i = 1; i <= 5; i++)
            {
                Image star = rateTour.FindName($"{starPrefix}{i}") as Image;
                if (i <= rating)
                {
                    star.Source = new BitmapImage(new Uri("/Resources/Assets/star-filled-rate-rating-bookmark-favourite-save-priority-important.256x243.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    star.Source = new BitmapImage(new Uri("/Resources/Assets/star-outline.256x238.png", UriKind.RelativeOrAbsolute));
                }
            }
        }
        public void Feedback_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            feedbackText = textBox.Text;
            rateTour.feedbackMessage.Visibility = Visibility.Hidden;
        }

        public void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = ImageService.GetInstance().GetImageFromUser("ReviewImages");
            Image image = ImageService.GetInstance().ReadImage(imagePath);
            TourReviewImage tourReviewImage = new TourReviewImage(selectedTour.Id, imagePath);

            image.Width = 185;
            image.Height = 135;

            _reviewImages.Add(tourReviewImage);
            rateTour.reviewImages_StackPanel.Children.Add(image);
            rateTour.imagesMessage.Visibility = Visibility.Hidden;
        }

        public bool CheckLanguage()
        {
            if(guideLanguageRating < 1 || guideLanguageRating > 5)
            {
                rateTour.languageMessage.Visibility = Visibility.Visible;
                return true;
            }
            return false;
        }
        public void CheckKnowledge()
        {
            if(guideKnowledgeRating < 1 || guideKnowledgeRating > 5)
            {
                rateTour.knowledgeMessage.Visibility = Visibility.Visible;
            }
        }
        public void CheckInterestingness()
        {
            if(interestingnessRating < 1 || interestingnessRating > 5)
            {
                rateTour.interestMessage.Visibility = Visibility.Visible;
            }
        }

        public void CheckFeedback()
        {
            if(feedbackText == "")
            {
                rateTour.feedbackMessage.Visibility = Visibility.Visible;
            }
        }
        public void CheckImages()
        {
            if (_reviewImages.Count == 0)
            {
                rateTour.imagesMessage.Visibility = Visibility.Visible;
            }
        }
        public bool CheckVisibility()
        {
            if (rateTour.languageMessage.Visibility == Visibility.Visible|| rateTour.feedbackMessage.Visibility == Visibility.Visible || rateTour.imagesMessage.Visibility==Visibility.Visible || rateTour.knowledgeMessage.Visibility == Visibility.Visible || rateTour.interestMessage.Visibility == Visibility.Visible)
            {
                return true;
            }
            return false;
        }
        public void Finish_Click(object sender, RoutedEventArgs e)
        {
            CheckInterestingness();
            CheckKnowledge();
            CheckLanguage();
            CheckImages();
            CheckFeedback();

            if (CheckVisibility()) {
                return;
            }

            //if (guideKnowledgeRating.ToString() != "" || guideLanguageRating.ToString() != "" || _reviewImages.Count != 0)
            if(!CheckVisibility())
            {
                TourReview tourReview = new TourReview(_user.Id, selectedTour.Id, guideKnowledgeRating, guideLanguageRating, interestingnessRating, feedbackText, "Invalid");
                TourReviewService.GetInstance().Add(tourReview);

                foreach (TourReviewImage reviewImage in _reviewImages)
                {
                    reviewImage.ReviewId = tourReview.Id;
                    TourReviewImageService.GetInstance().Add(reviewImage);
                }
                /*Window parentWindow = Window.GetWindow(rateTour);
                if (parentWindow is TouristMainWindow mainWindow)
                {
                    mainWindow.TouristWindowFrame.Content = mainWindow.ToursVisitedUserControl;
                }*/
                NavigationService.GetNavigationService(rateTour).GoBack();

            }
            else
            {
                //MessageBox.Show("Popunite sva polja!");
            }
        }
    }
}
