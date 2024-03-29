using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.Services.Mutual;

public class ImageService
{
    private static Lazy<ImageService> instance = new Lazy<ImageService>(() => new ImageService());

    private ImageService()
    {
    }

    public static ImageService GetInstance()
    {
        return instance.Value;
    }

    public string GetImageFromUser()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|All files (*.*)|*.*";
        if (openFileDialog.ShowDialog() == true)
        {
            string selectedImagePath = openFileDialog.FileName;
            string destinationFolder = "../../../Resources/Images/AccommodationImages/";

            string fileName = System.IO.Path.GetFileName(selectedImagePath);

            string destinationPath = System.IO.Path.Combine(destinationFolder, fileName);

            if (!System.IO.File.Exists(destinationPath))
            {
                System.IO.File.Copy(selectedImagePath, destinationPath, true);
            }

            return destinationPath;
        }
        else return null;
    }

    public Image ReadImage(string absoluteImagePath)
    {
        using (FileStream stream = new FileStream(absoluteImagePath, FileMode.Open, FileAccess.Read))
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            Image image = new Image
            {
                Source = bitmapImage,
                Width = 250,
                Height = 200,
                Margin = new Thickness(5),
                Stretch = Stretch.Fill
            };

            return image;
        }
    }
}
