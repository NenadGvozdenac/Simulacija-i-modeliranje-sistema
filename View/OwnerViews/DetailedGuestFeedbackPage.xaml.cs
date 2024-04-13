using BookingApp.Model.MutualModels;
using BookingApp.Services.Mutual;
using BookingApp.ViewModel.OwnerViewModels;
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

namespace BookingApp.View.OwnerViews
{
    public partial class DetailedGuestFeedbackPage : Page
    {
        private ImageService _imageService;
        public DetailedGuestFeedbackPage(DetailedGuestFeedbackViewModel detailedGuestFeedbackViewModel)
        {
            DataContext = detailedGuestFeedbackViewModel;
            _imageService = ImageService.GetInstance();
            InitializeComponent();
            LoadImages(detailedGuestFeedbackViewModel.AccommodationReview);
        }

        private void LoadImages(AccommodationReview accommodationReview)
        {
            if (accommodationReview.ReviewImages.Count > 0)
            {
                foreach (var reviewImage in accommodationReview.ReviewImages)
                {
                    Image image = _imageService.ReadImage(reviewImage.Path);
                    images_StackPanel.Children.Add(image);
                }
            }
        }

        private void BackArrowClick(object sender, MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
