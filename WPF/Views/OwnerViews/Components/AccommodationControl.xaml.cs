using BookingApp.Domain.Models;
using BookingApp.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
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

namespace BookingApp.View.OwnerViews.Components
{
    /// <summary>
    /// Interaction logic for AccommodationControl.xaml
    /// </summary>
    public partial class AccommodationControl : UserControl, INotifyPropertyChanged
    {
        public Accommodation Accommodation { get; set; }
        public Location Location { get; set; }

        public event EventHandler<Accommodation> AccommodationSeeMore;

        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                accommodationName = value;
                OnPropertyChanged("AccommodationName");
            }
        }

        private string accommodationLocation;
        public string AccommodationLocation
        {
            get { return accommodationLocation; }
            set
            {
                accommodationLocation = value;
                OnPropertyChanged("AccommodationLocation");
            }
        }

        private string accommodationType;
        public string AccommodationType
        {
            get { return accommodationType; }
            set
            {
                accommodationType = value;
                OnPropertyChanged("AccommodationType");
            }
        }

        // The event handler for the property changed event
        public event PropertyChangedEventHandler PropertyChanged;

        // The method to invoke the property changed event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationControl(Accommodation accommodation, Location location)
        {
            InitializeComponent();
            Accommodation = accommodation;
            LoadAccommodationImages();
            LoadAccommodationLocation();
            Location = location;
            DataContext = this;
            SetupAccommodation();
        }

        private void LoadAccommodationLocation()
        {
            AccommodationLocation = LocationRepository.GetInstance().GetById(Accommodation.LocationId).ToString();
        }

        private void LoadAccommodationImages()
        {
            Accommodation.Images = AccommodationImageRepository.GetInstance().GetImagesByAccommodationId(Accommodation.Id);
        }

        private void SetupAccommodation()
        {
            AccommodationName = Accommodation.Name;
            AccommodationLocation = LocationRepository.GetInstance().GetById(Accommodation.LocationId).ToString();
            AccommodationType = Accommodation.Type.ToString();
            LoadImageFromFile();
        }

        private void LoadImageFromFile()
        {
            string relativeImagePath = FindRelativeImagePath(Accommodation.Images);
            string absoluteImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeImagePath);
            InsertImage(absoluteImagePath);
        }

        private string FindRelativeImagePath(List<AccommodationImage> images)
        {
            if (Accommodation.Images.Count == 0)
            {
                return "../../../Resources/Assets/default_house.png";
            }
            else
            {
                return Accommodation.Images[0].Path;
            }
        }

        private void InsertImage(string absoluteImagePath)
        {
            using (FileStream stream = new FileStream(absoluteImagePath, FileMode.Open, FileAccess.Read))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                Image.ImageSource = bitmapImage;
            }
        }

        private void AccommodationClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AccommodationSeeMore.Invoke(this, Accommodation);
        }
    }
}
