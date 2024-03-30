using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.Repository;
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

namespace BookingApp.View.OwnerViews
{
    public partial class GuestReviewPage : Page
    {
        private User _user;

        private ReviewedGuestReviews _reviewedGuestReviews;
        private PendingGuestReviews _pendingGuestReviews;

        public GuestReviewPage(User user)
        {
            _user = user;

            _reviewedGuestReviews = new ReviewedGuestReviews(_user);
            _pendingGuestReviews = new PendingGuestReviews(_user);

            InitializeComponent();

            MainPanel.Content = _reviewedGuestReviews;
        }

        private void ShowRightNavbar()
        {
            RightNavbar.Visibility = Visibility.Visible;
            Navbar.ColumnDefinitions[2].Width = new GridLength(0.6, GridUnitType.Star);
        }

        private void HideRightNavbar()
        {
            RightNavbar.Visibility = Visibility.Collapsed;
            Navbar.ColumnDefinitions[2].Width = new GridLength(0);
        }

        public void ThreeDotsClick(object sender, MouseButtonEventArgs e)
        {
            if (RightNavbar.Visibility == Visibility.Collapsed)
            {
                ShowRightNavbar();
            }
            else
            {
                HideRightNavbar();
            }
        }

        private void PendingButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = _pendingGuestReviews;
            PendingButton.IsEnabled = false;
            ReviewedButton.IsEnabled = true;
        }

        private void ReviewedButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = _reviewedGuestReviews;
            PendingButton.IsEnabled = true;
            ReviewedButton.IsEnabled = false;
        }

        private void PendingButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _reviewedGuestReviews.Update();
            _pendingGuestReviews.Update();
        }

        private void ReviewedButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _reviewedGuestReviews.Update();
            _pendingGuestReviews.Update();
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

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Window.GetWindow(this).Close();
        }
    }
}
