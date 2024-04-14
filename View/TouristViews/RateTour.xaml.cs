using System;
using System.Collections.Generic;
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
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.Repositories;


namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for RateTour.xaml
    /// </summary>
    public partial class RateTour : UserControl
    {
        public Tour selectedTour {  get; set; }
        public User _user {  get; set; }
        public TouristReservation reservation { get; set; }
        public TouristReservationRepository _touristReservationRepository { get; set; }
        public TourRepository _tourRepository { get; set; }
        public TourReviewRepository _tourReviewRepository { get; set; }
        public TourReviewImageRepository _reviewImageRepository { get; set; }
        public List<TourReviewImage> _reviewImages {  get; set; }

        private int guideKnowledgeRating = 0;
        private int guideLanguageRating = 0;
        private int interestingnessRating = 0;
        private string feedbackText = string.Empty;


        public RateTour(User user, TouristReservationRepository touristReservationRepository, TourRepository tourRepository, TourReviewRepository tourReviewRepository, TourReviewImageRepository tourReviewImageRepository, int tourId)
        {
            InitializeComponent();
            _user = user;
            _touristReservationRepository = touristReservationRepository;
            _tourRepository = tourRepository;
            _tourReviewRepository = tourReviewRepository;
            _reviewImageRepository = tourReviewImageRepository;
            //reservation = _touristReservationRepository.GetById(reservationId);
            _tourRepository = new TourRepository();
            _reviewImages = new List<TourReviewImage>();
            selectedTour = _tourRepository.GetById(tourId);
        }


        private void Star_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            }
            else if (starName.StartsWith("guideLanguageStar"))
            {
                guideLanguageRating = starNumber;
                UpdateStarRatingUI("guideLanguageStar", guideLanguageRating);
            }
            // Ocenjivanje Interestigness
            else if (starName.StartsWith("interestingnessStar"))
            {
                interestingnessRating = starNumber;
                UpdateStarRatingUI("interestingnessStar", interestingnessRating);
            }
        }

        private void UpdateStarRatingUI(string starPrefix, int rating)
        {
            for (int i = 1; i <= 5; i++)
            {
                Image star = FindName($"{starPrefix}{i}") as Image;
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
        private void Feedback_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            feedbackText = textBox.Text;
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = ImageService.GetInstance().GetImageFromUser("ReviewImages");
            Image image = ImageService.GetInstance().ReadImage(imagePath);
            TourReviewImage tourReviewImage = new TourReviewImage(selectedTour.Id, imagePath);
            
            image.Width = 185;
            image.Height = 135;

            _reviewImages.Add(tourReviewImage);
            reviewImages_StackPanel.Children.Add(image);
        }
        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            if (guideKnowledgeRating.ToString() != "" || guideLanguageRating.ToString() != "" || _reviewImages.Count != 0)
            {
                TourReview tourReview = new TourReview(_user.Id, selectedTour.Id, guideKnowledgeRating, guideLanguageRating, interestingnessRating, feedbackText);
                _tourReviewRepository.Add(tourReview);

                foreach (TourReviewImage reviewImage in _reviewImages)
                {
                    reviewImage.ReviewId = tourReview.Id;
                    _reviewImageRepository.Add(reviewImage);
                }
                Window parentWindow = Window.GetWindow(this);
                if (parentWindow is TouristMainWindow mainWindow)
                {
                    mainWindow.TouristWindowFrame.Content = mainWindow.ToursVisitedUserControl;
                }
            }
            else
            {
                MessageBox.Show("Popunite sva polja!");
            }
        }

        
    }
}
