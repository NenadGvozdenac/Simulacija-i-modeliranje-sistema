using BookingApp.View.OwnerViews.GuestReviewControls;
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
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;

namespace BookingApp.View.OwnerViews
{
    public partial class GuestReviewPage : Page
    {
        public GuestReviewsViewModel ViewModel { get; set; }
        public GuestReviewPage(User user)
        {
            ReviewedGuestReviews _reviewedGuestReviews = new(user);
            PendingGuestReviews _pendingGuestReviews = new(user);

            ViewModel = new GuestReviewsViewModel(this, _reviewedGuestReviews, _pendingGuestReviews);

            InitializeComponent();

            MainPanel.Content = _reviewedGuestReviews;
        }

        private void PendingButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = ViewModel.PendingGuestReviews;
            PendingButton.IsEnabled = false;
            ReviewedButton.IsEnabled = true;
        }

        private void ReviewedButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = ViewModel.ReviewedGuestReviews;
            PendingButton.IsEnabled = true;
            ReviewedButton.IsEnabled = false;
        }

        private void PendingButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel.ReviewedGuestReviews.Update();
            ViewModel.PendingGuestReviews.Update();
        }

        private void ReviewedButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel.ReviewedGuestReviews.Update();
            ViewModel.PendingGuestReviews.Update();
        }

        private void BackArrowClick(object sender, MouseButtonEventArgs e)
        {
            NavigateToPreviousPage();
        }

        private void NavigateToPreviousPage()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
