using BookingApp.Model.MutualModels;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BookingApp.ViewModel.OwnerViewModels;
using BookingApp.Services.Owner;
using BookingApp.Services.Mutual;

namespace BookingApp.View.OwnerViews
{
    public partial class AddAccommodationPage : Page
    {
        public event EventHandler PageClosed;

        private AddAccommodationViewModel _addAccommodationViewModel;

        public AddAccommodationPage(User user)
        {
            _addAccommodationViewModel = new AddAccommodationViewModel(user);
            DataContext = _addAccommodationViewModel;

            InitializeComponent();
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            bool successfullyAddedAccommodation = _addAccommodationViewModel.AddAccommodation();

            if (!successfullyAddedAccommodation)
            {
                return;
            }

            NavigateToPreviousPage();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateToPreviousPage();
        }

        private void NavigateToPreviousPage()
        {
            if (NavigationService.CanGoBack)
            {
                ClosePage();
                NavigationService.GoBack();
            }
        }

        private void CountryTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Cities = LocationService.GetInstance().GetCitiesByCountry(CountryTextBox.SelectedItem.ToString());
            _addAccommodationViewModel.Cities = new ObservableCollection<string>(Cities);

            CityTextBox.IsDropDownOpen = true;
            CityTextBox.IsEnabled = true;
        }

        private void AddURLClick(object sender, RoutedEventArgs e)
        {
            _addAccommodationViewModel.AddImage();
            ImageURLTextBox.Clear();
        }

        private void ClosePage()
        {
            PageClosed?.Invoke(this, EventArgs.Empty);
        }
        private void BackButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                ClosePage();
                NavigationService.GoBack();
            }
        }

        private void ImageURLTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string imagePath = ImageService.GetInstance().GetImageFromUser();
            _addAccommodationViewModel.ImageURL = imagePath;
        }
    }
}