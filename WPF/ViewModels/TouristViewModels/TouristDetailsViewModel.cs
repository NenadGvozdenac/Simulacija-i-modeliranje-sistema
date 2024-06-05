using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BookingApp.WPF.Views.GuestViews;
using BookingApp.Domain.RepositoryInterfaces;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class TouristDetailsViewModel
    {
        public Tours ToursUserControl;
        public event EventHandler ReturnRequest;
        public User _user;
        public ObservableCollection<Tourist> _tourists { get; set; }

        public TouristDetails TouristDetailsView {  get; set; }

        public Frame TouristWindowFrame;

        public Tour selectedTour { get; set; }
        public TourVoucher Voucher {  get; set; }
        public ObservableCollection<TourVoucher> vouchers { get; set; }
        private ObservableCollection<TourVoucher> _vouchers;
        public ObservableCollection<TourVoucher> Vouchers
        {
            get { return _vouchers; }
            set
            {
                if (_vouchers != value)
                {
                    _vouchers = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<TourImage> tourImages {  get; set; }
        public TourImage tourImage {  get; set; }
        public TouristDetailsViewModel(Tour detailedTour, User user, TouristDetails touristDetails, Frame touristWindowFrame)
        {
            _user = user;
            vouchers = new ObservableCollection<TourVoucher>();
            TouristDetailsView = touristDetails;
            TouristWindowFrame = touristWindowFrame;
            selectedTour = detailedTour;
            ToursUserControl = new Tours(user);
            _tourists = new ObservableCollection<Tourist>();
            TouristDetailsView.tourNameTextBlock.Text = selectedTour.Name;
            TouristDetailsView.tourCountryTextBlock.Text = LocationService.GetInstance().GetById(selectedTour.LocationId).Country + ", " + LocationService.GetInstance().GetById(selectedTour.LocationId).City;
            //TouristDetailsView.tourCityTextBlock.Text = LocationService.GetInstance().GetById(selectedTour.LocationId).City;
            TouristDetailsView.DescriptionTextBlock.Text = selectedTour.Description;
            TouristDetailsView.LanguageText.Text = LanguageService.GetInstance().GetById(selectedTour.LanguageId).ToString();
            TouristDetailsView.CapacityText.Text = selectedTour.Capacity.ToString();
            TouristDetailsView.DurationText.Text = selectedTour.Duration.ToString();
            tourImages = TourImageService.GetInstance().GetImagesByTourId(selectedTour.Id);
            foreach(TourImage image in tourImages)
            {
                tourImage = image;
                break;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Reserve_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(TouristDetailsView);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.TourReservation(selectedTour, _user);
            }
        }
    }
}
