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
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.Application.UseCases;

public class ImageService
{
    public ImageService()
    {
    }

    public static ImageService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<ImageService>();
    }

    public string GetImageFromUser(string folder = "AccommodationImages")
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|All files (*.*)|*.*";
        if (openFileDialog.ShowDialog() == true)
        {
            string selectedImagePath = openFileDialog.FileName;
            string destinationFolder = "../../../Resources/Images/" + folder + "/";

            string fileName = Path.GetFileName(selectedImagePath);

            string destinationPath = Path.Combine(destinationFolder, fileName);

            if (!File.Exists(destinationPath))
            {
                File.Copy(selectedImagePath, destinationPath, true);
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
