using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
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

namespace BookingApp.Application.UseCases
{
    public class TourImageService
    {
        private ITourImageRepository _tourImageRepository;


        public TourImageService(ITourImageRepository imageRepository)
        {
            _tourImageRepository = imageRepository;
        }

        public static TourImageService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourImageService>();
        }

        public void Add(TourImage tour)
        {
            _tourImageRepository.Add(tour);
        }

        public void Remove(TourImage tour)
        {
            _tourImageRepository.Remove(tour);
        }

        public List<TourImage> GetAll()
        {
            return _tourImageRepository.GetAll();
        }

        public int NextId()
        {
            return _tourImageRepository.NextId();
        }

        public List<TourImage> GetByTourId(int tourId)
        {
            return _tourImageRepository.GetByTourId(tourId);
        }

        public void Update(TourImage tour)
        {
            _tourImageRepository.Equals(tour);
        }

        public void RemoveByTourId(int tourId)
        {
            _tourImageRepository.RemoveByTourId(tourId);
        }

        public List<TourImage> GetImagesByTourId(int id)
        {
            return _tourImageRepository.GetImagesByTourId(id);
        }

        public string GetImageFromUser(string folder = "TourImages")
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
}
