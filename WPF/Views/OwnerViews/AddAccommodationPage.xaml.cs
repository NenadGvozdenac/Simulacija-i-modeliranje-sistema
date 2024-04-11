using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;

namespace BookingApp.View.OwnerViews;

public partial class AddAccommodationPage : Page
{
    public event EventHandler PageClosed;

    private AddAccommodationViewModel _addAccommodationViewModel;

    public AddAccommodationPage(User user)
    {
        _addAccommodationViewModel = new AddAccommodationViewModel(this, user);
        DataContext = _addAccommodationViewModel;

        InitializeComponent();
    }

    private void CountryTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var Cities = LocationService.GetInstance().GetCitiesByCountry(CountryTextBox.SelectedItem.ToString());
        _addAccommodationViewModel.Cities = new ObservableCollection<string>(Cities);

        CityTextBox.IsDropDownOpen = true;
        CityTextBox.IsEnabled = true;
    }

    private void ClosePage()
    {
        PageClosed?.Invoke(this, EventArgs.Empty);
    }
    private void BackButton_Click(object sender, MouseButtonEventArgs e)
    {
        ClosePage();
        _addAccommodationViewModel.CancelCommand.Execute(null);
    }

    private void ImageURLTextBox_MouseDown(object sender, MouseButtonEventArgs e)
    {
        string imagePath = ImageService.GetInstance().GetImageFromUser();
        _addAccommodationViewModel.ImageURL = imagePath;
    }
}