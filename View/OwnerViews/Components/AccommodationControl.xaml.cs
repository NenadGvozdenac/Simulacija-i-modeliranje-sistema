using BookingApp.Model.MutualModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace BookingApp.View.OwnerViews.Components
{
    /// <summary>
    /// Interaction logic for AccommodationControl.xaml
    /// </summary>
    public partial class AccommodationControl : UserControl
    {
        public Accommodation Accommodation { get; set; }
        public Location Location { get; set; }
        public AccommodationControl(Accommodation accommodation, Location location)
        {
            InitializeComponent();
            Accommodation = accommodation;
            Location = location;
            SetupAccommodation();
        }

        private void SetupAccommodation()
        {
            AccommodationName.Content = Accommodation.Name;
            AccommodationLocation.Content = Location;
            AccommodationType.Content = Accommodation.Type;
            LoadImageFromFile();
        }

        private void LoadImageFromFile()
        {
            try
            {
                string relativeImagePath = Accommodation.Images[0].Path;
                string absoluteImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeImagePath);

                using (FileStream stream = new FileStream(absoluteImagePath, FileMode.Open, FileAccess.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad; // Ensure the image is fully loaded into memory
                    bitmapImage.EndInit();

                    Image.Source = bitmapImage;
                }
            }
            catch (Exception ex)
            {
                string relativeImagePath = "../../../Resources/Assets/default_house.png";

                string absoluteImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeImagePath);

                using (FileStream stream = new FileStream(absoluteImagePath, FileMode.Open, FileAccess.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad; // Ensure the image is fully loaded into memory
                    bitmapImage.EndInit();

                    Image.Source = bitmapImage;
                }
            }
        }

        private void EditButton_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
