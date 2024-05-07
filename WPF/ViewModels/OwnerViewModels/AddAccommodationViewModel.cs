using BookingApp.Application.Commands;
using BookingApp.Application.Localization;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.OwnerViews.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Controls;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class AddAccommodationViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<string> _accommodationTypes;

    [ObservableProperty]
    private ObservableCollection<AccommodationImage> _images;

    [ObservableProperty]
    private User _user;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private string _accommodationName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private string _location;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private string _type;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private int _accommodationPrice;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private int _maximumNumberOfGuests;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private int _minimumNumberOfDaysForReservation;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AddCommand))]
    private int daysBeforeReservationIsFinal;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(RemoveImageCommand))]
    private string imageURL;

    public AddAccommodationPage Page;
    public ICommand AddCommand => new AddAccommodationCommand(this);
    public ICommand CancelCommand => new NavigateToPreviousPageCommand(Page);
    public ICommand AddImageCommand => new AddImageCommand(this);
    public ICommand RemoveImageCommand => new RemoveImageCommand(this);

    public AddAccommodationViewModel(AddAccommodationPage page, User user)
    {
        User = user;
        Page = page;

        AccommodationTypes = new ObservableCollection<string>((Enum.GetNames(typeof(AccommodationType))).Select(a => TranslationSource.Instance[a]));
        Images = new ObservableCollection<AccommodationImage>();
    }

    public void LeftArrowClick()
    {
        var currentImage = Images.FirstOrDefault(image => image.Path == ImageURL);
        var currentIndex = Images.IndexOf(currentImage);

        if(currentIndex == -1) { return; }

        if (currentIndex == 0)
        {
            ImageURL = Images.Last().Path;
        }
        else
        {
            ImageURL = Images[currentIndex - 1].Path;
        }
    }

    public void RightArrowClick()
    {
        var currentImage = Images.FirstOrDefault(image => image.Path == ImageURL);
        var currentIndex = Images.IndexOf(currentImage);

        if (currentIndex == -1) { return; }

        if (currentIndex == Images.Count - 1)
        {
            ImageURL = Images.First().Path;
        }
        else
        {
            ImageURL = Images[currentIndex + 1].Path;
        }
    }

    public void LocationTextBox_TextChanged()
    {
        string searchText = Page.LocationTextBox.Text.ToLower();

        if (searchText.Length == 0)
        {
            Location = "";
            return;
        }

        UpdateLocation(searchText);
    }

    public void LocationTextBox_PreviewKeyDown(Key key)
    {
        if (key == Key.Back)
        {
            Location = "";
            return;
        }

        if (key == Key.Enter)
        {
            this.DisableAll();

            EnterNewLocationModal enterNewLocationModal = new EnterNewLocationModal(this);

            Page.AddLocationModalPanel.Children.Add(enterNewLocationModal);
            Page.AddLocationModalPanel.Visibility = Visibility.Visible;

            this.Page.MainGrid.Opacity = 0.5;

            enterNewLocationModal.IsVisibleChanged += (s, e) => EnableAll(e);
        }

        if (key == Key.Tab)
        {
            string searchText = Page.LocationTextBox.Text.ToLower();

            UpdateLocation(searchText);
        }
    }

    private void EnableAll(DependencyPropertyChangedEventArgs e)
    {
        if(e.NewValue is Visibility visibility && visibility == Visibility.Collapsed) { return; }

        this.Page.MainGrid.Opacity = 1;

        foreach (TextBox t in Page.FindLogicalChildren<TextBox>())
        {
            t.IsEnabled = true;
        }

        foreach (Button button in Page.FindLogicalChildren<Button>())
        {
            button.IsEnabled = true;
        }

        foreach (ComboBox comboBox in Page.FindLogicalChildren<ComboBox>())
        {
            comboBox.IsEnabled = true;
        }
    }

    public void DisableAll()
    {
        foreach (TextBox t in Page.FindLogicalChildren<TextBox>())
        {
            t.IsEnabled = false;
        }

        foreach (Button button in Page.FindLogicalChildren<Button>())
        {
            button.IsEnabled = false;
        }

        foreach (ComboBox comboBox in Page.FindLogicalChildren<ComboBox>())
        {
            comboBox.IsEnabled = false;
        }
    }

    private void UpdateLocation(string searchText)
    {
        string matchedLocation = LocationService.GetInstance().GetLocationsFormatted().FirstOrDefault(loc => loc.ToLower().StartsWith(searchText));

        if (!string.IsNullOrEmpty(matchedLocation))
        {
            Location = matchedLocation;
            Page.LocationTextBox.Select(searchText.Length, matchedLocation.Length - searchText.Length);
        }
    }
}